

using Adros.Core.DomainServices.IDomainService;
using Adros.Core.Entities.Subscription;
using Adros.Core.Entities.Users;
using Adros.Shared.Interfaces;
using Adros.Application.DTOs.Payment;
using Adros.Application.Interfaces.IService;
using Microsoft.EntityFrameworkCore;

namespace Adros.Application.Services.UsersServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymobService _paymobService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStudentService _studentService;

        public PaymentService(
            IUnitOfWork unitOfWork,
            IPaymobService paymobService,
            ICurrentUserService currentUserService,
            IStudentService studentService)
        {
            _unitOfWork = unitOfWork;
            _paymobService = paymobService;
            _currentUserService = currentUserService;
            _studentService = studentService;
        }




        public async Task<PaymobInitResponse> InitiateSubscriptionPaymentAsync(Guid subscriptionId)
        {
            var userId = _currentUserService.UserId;
            if (userId == Guid.Empty)
                throw new Exception("User is not authenticated");

            var studentId = await _studentService.GetStudentIdByUserIdAsync(userId);
            if (studentId == Guid.Empty)
                throw new Exception("Student not found for this user");

            var subscription = await _unitOfWork.Repository<Subscription>()
                .GetByIdAsync(subscriptionId);

            if (subscription == null)
                throw new Exception("Subscription not found");

            if (!decimal.TryParse(subscription.Price, out var price) || price <= 0)
                throw new Exception("Invalid subscription price");

            // 🔹 إنشاء MerchantOrderId جديد
            var merchantOrderId = Guid.NewGuid().ToString();

            // 🔹 حفظ محاولة الدفع فقط في PendingPayment
            var pendingPayment = new PendingPayment
            {
                StudentId = studentId,
                SubscriptionId = subscriptionId,
                MerchantOrderId = merchantOrderId,
                Amount = price
            };

            await _unitOfWork.Repository<PendingPayment>().AddAsync(pendingPayment);
            await _unitOfWork.CompleteAsync();

            // 🔹 إنشاء عملية الدفع على Paymob
            var paymentResult = await _paymobService.CreatePaymentAsync(
                amount: price,
                merchantOrderId: merchantOrderId
            );

            if (string.IsNullOrWhiteSpace(paymentResult.paymentUrl))
                throw new Exception("Failed to generate Paymob payment URL");

            return new PaymobInitResponse
            {
                PaymentUrl = paymentResult.paymentUrl,
                MerchantOrderId = merchantOrderId
            };
        }


        public async Task<bool> DeleteSubscriptionsByStudentAsync(Guid studentId)
        {
            // جلب كل الاشتراكات للطالب مباشرة من قاعدة البيانات
            var subscriptions = await _unitOfWork.Repository<StudentSubscription>()
                .GetAllAsync(filter: q => q.Where(x => x.StudentId == studentId));
            
            
            if (!subscriptions.Any())
                return false; // مفيش اشتراكات للطالب

            // حذف كل الاشتراكات
            foreach (var sub in subscriptions)
                _unitOfWork.Repository<StudentSubscription>().Delete(sub);

            // تحديث حالة الطالب
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(studentId);
            if (student != null)
                student.IsSubscriped = false;

            await _unitOfWork.CompleteAsync();
            return true;
        }

        // =========================
        // Webhook Handler
        // =========================
        public async Task HandlePaymobWebhookAsync(PaymobWebhookDto dto)
        {
            if (dto == null)
                return;

            // 🔹 جلب محاولة الدفع من PendingPayment
            var pendingPayment = await _unitOfWork.Repository<PendingPayment>()
                .GetAsync(x => x.MerchantOrderId == dto.MerchantOrderId);

            if (pendingPayment == null)
                return; // لو ما لقيتش payment، ما نعملش حاجة

            // 🔹 إنشاء StudentSubscription بعد الدفع
            var studentSubscription = new StudentSubscription
            {
                StudentId = pendingPayment.StudentId,
                SubscriptionId = pendingPayment.SubscriptionId,
                IsPaid = dto.Success, // true لو الدفع ناجح، false لو فشل
                PaymobOrderId = dto.OrderId,
                PaymentTransactionId = dto.TransactionId,
                StartDate = DateTime.UtcNow
            };
    
            var subscription = await _unitOfWork.Repository<Subscription>()
                .GetByIdAsync(pendingPayment.SubscriptionId);

            studentSubscription.EndDate = subscription.Duration == "شهري"
                ? DateTime.UtcNow.AddMonths(1)
                : DateTime.UtcNow.AddYears(1);

            // 🔹 تفعيل الطالب
            var student = await _unitOfWork.Repository<Student>()
                .GetByIdAsync(pendingPayment.StudentId);

            if (student != null)
                student.IsSubscriped = true;

            // 🔹 إضافة StudentSubscription لقاعدة البيانات
            await _unitOfWork.Repository<StudentSubscription>().AddAsync(studentSubscription);

            // 🔹 حذف السجل المؤقت بعد محاولة الدفع
            _unitOfWork.Repository<PendingPayment>().Delete(pendingPayment);

            await _unitOfWork.CompleteAsync();
        }






    }
}


