using Adros.Application.DTOs.Payment;
using Adros.Application.Interfaces.IService;
using Adros.Core.Entities.Subscription;
using Adros.Core.Entities.Users;
using Adros.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adros.Apis.Controllers.Subscriptions
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _hmacSecret;

        public PaymentsController(IPaymentService paymentService, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _hmacSecret = configuration["Paymob:HmacSecret"];
        }

        // =========================
        // 1) Student clicks Subscribe
        // =========================
        [HttpPost("subscribe/{subscriptionId}")]
        public async Task<IActionResult> Subscribe(Guid subscriptionId)
        {
            try
            {
                var result = await _paymentService.InitiateSubscriptionPaymentAsync(subscriptionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // =========================
        // 2) Unsubscribe
        // =========================
        [HttpDelete("unsubscribe/{studentId}")]
        public async Task<IActionResult> DeleteSubscriptionByStudent(Guid studentId)
        {
            var result = await _paymentService.DeleteSubscriptionsByStudentAsync(studentId);
            if (!result)
                return NotFound(new { message = "No subscriptions found for this student" });

            return Ok(new { message = "All subscriptions for the student deleted successfully" });
        }

        // =========================
        // 3) Redirect after payment (frontend)
        // =========================
        [HttpGet("redirect")]
        [AllowAnonymous]
        public async Task<IActionResult> PaymentRedirect()
        {
            var query = Request.Query;

            bool success = string.Equals(query["success"].FirstOrDefault(), "true", StringComparison.OrdinalIgnoreCase);
            string merchantOrderId = query["merchant_order_id"].FirstOrDefault();

            // 🔹 optional: تحقق من السجل قبل redirect
            var pendingPayment = await _unitOfWork.Repository<PendingPayment>()
                .GetAsync(x => x.MerchantOrderId == merchantOrderId);

            var redirectUrl = success
                ? "https://adrossweb.vercel.app/app/payment-success"
                : "https://adrossweb.vercel.app/app/payment-fail";

            return Redirect(redirectUrl);
        }

        // =========================
        // TEST ONLY - Fake Paymob Webhook
        // =========================
        [HttpGet("paymob/test-webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> TestPaymobWebhook(
            string merchant_order_id,
            bool success
        )
        {
            var dto = new PaymobWebhookDto
            {
                MerchantOrderId = merchant_order_id,
                OrderId = "TEST_ORDER",
                TransactionId = Guid.NewGuid().ToString(),
                Success = success,
                AmountCents = 10000,
                Message = "TEST WEBHOOK"
            };

            await _paymentService.HandlePaymobWebhookAsync(dto);

            return Ok(new
            {
                message = "Fake webhook sent successfully",
                merchant_order_id,
                success
            });
        }


        // =========================
        // 4) Paymob Webhook
        // =========================
        [HttpPost("paymob/webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> PaymobWebhook()
        {
            var query = Request.Query;

            // 🔹 تحقق من HMAC (مهم لل production)
            //if (!ValidateHmac(query, _hmacSecret))
            //    return BadRequest("Invalid HMAC");

            var dto = new PaymobWebhookDto
            {
                OrderId = query["merchant_order_id"].FirstOrDefault() ?? "",
                TransactionId = query["id"].FirstOrDefault() ?? "",
                Success = string.Equals(query["success"].FirstOrDefault(), "true", StringComparison.OrdinalIgnoreCase),
                AmountCents = int.TryParse(query["amount_cents"].FirstOrDefault(), out var amount) ? amount : 0,
                MerchantOrderId = query["merchant_order_id"].FirstOrDefault(),
                Message = (query.ContainsKey("error_occured") && query["error_occured"].FirstOrDefault() == "true")
                    ? query["pending"].FirstOrDefault()
                    : null
            };

            // 🔹 سجل الدفع في DB
            await _paymentService.HandlePaymobWebhookAsync(dto);

            // 🔹 Webhook لا يحتاج redirect
            return Ok();
        }

        // =========================
        // 5) HMAC Validation
        // =========================
        private bool ValidateHmac(Microsoft.AspNetCore.Http.IQueryCollection query, string secret)
        {
            if (!query.ContainsKey("hmac"))
                return false;

            var receivedHmac = query["hmac"].ToString();

            // ترتيب البيانات حسب Paymob documentation
            var keysToInclude = query.Keys.Where(k => k != "hmac").OrderBy(k => k);
            var dataString = string.Join("", keysToInclude.Select(k => $"{query[k]}"));

            using var hmacSha512 = new System.Security.Cryptography.HMACSHA512(System.Text.Encoding.UTF8.GetBytes(secret));
            var hashBytes = hmacSha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataString));
            var computedHmac = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return computedHmac == receivedHmac.ToLower();
        }
    }
}
