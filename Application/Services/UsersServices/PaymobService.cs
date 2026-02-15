//using Adros.Application.DTOs;
//using Adros.Application.Interfaces.IService;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Adros.Application.Services.UsersServices
//{
//    public class PaymobService : IPaymobService
//    {
//        private readonly PaymobSettings _paymobSettings;
//        public PaymobService(IOptions<PaymobSettings> options)
//        {
//            _paymobSettings = options.Value;
//        }

//        public async Task<string> CreatePaymentAsync(string amount, string merchantOrderId)
//        {
//            // 1) Auth Token
//            // 2) Create Order
//            // 3) Payment Key
//            // 4) Return iframe url

//            return $"https://accept.paymob.com/api/acceptance/iframes/XXXX?payment_token=YYYY";
//        }
//    }

//}

using Adros.Application.DTOs;
using Adros.Application.Interfaces.IService;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Adros.Application.Services.UsersServices
{
    public class PaymobService : IPaymobService
    {
        private readonly HttpClient _httpClient;
        private readonly PaymobSettings _settings;

        public PaymobService(HttpClient httpClient, IOptions<PaymobSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }

        public async Task<(string paymentUrl, string paymobOrderId)> CreatePaymentAsync(decimal amount, string merchantOrderId)
        {
            var authToken = await GetAuthTokenAsync();
            var orderId = await CreateOrderAsync(authToken, amount, merchantOrderId);
            var paymentToken = await CreatePaymentKeyAsync(authToken, orderId, amount);

            var url =
                $"https://accept.paymob.com/api/acceptance/iframes/{_settings.IframeId}?payment_token={paymentToken}";

            return (url, orderId.ToString());
        }

        private async Task<string> GetAuthTokenAsync()
        {
            var payload = new { api_key = _settings.ApiKey };
            var response = await _httpClient.PostAsync(
                "https://accept.paymob.com/api/auth/tokens",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("token").GetString();
        }

        private async Task<int> CreateOrderAsync(string authToken, decimal amount, string merchantOrderId)
        {
            var payload = new
            {
                delivery_needed = false,
                amount_cents = (int)(amount * 100),
                currency = "EGP",
                merchant_order_id = merchantOrderId
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://accept.paymob.com/api/ecommerce/orders"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", authToken);

            request.Content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Paymob CreateOrder failed: {body}");

            var doc = JsonDocument.Parse(body);
            return doc.RootElement.GetProperty("id").GetInt32();
        }


        //        private async Task<string> CreatePaymentKeyAsync(string authToken, int orderId, decimal amount)
        //        {
        //            var payload = new
        //            {
        //                auth_token = authToken,
        //                amount_cents = (int)(amount * 100),
        //                expiration = 3600,
        //                order_id = orderId,
        //                billing_data = new
        //                {
        //                    first_name = "Student",
        //                    last_name = "Name",
        //                    email = "student@test.com",
        //                    phone_number = "01000000000",
        //                    apartment = "NA",
        //                    floor = "NA",
        //                    street = "NA",
        //                    building = "NA",
        //                    shipping_method = "NA",
        //                    city = "Cairo",
        //                    country = "EG",
        //                    state = "Cairo",
        //                    zip_code = "00000",

        //                }
        //,
        //                currency = "EGP",
        //                integration_id = _settings.IntegrationId
        //            };

        //            var response = await _httpClient.PostAsync(
        //                "https://accept.paymob.com/api/acceptance/payment_keys",
        //                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        //            );
        //            response.EnsureSuccessStatusCode();

        //            var json = await response.Content.ReadAsStringAsync();
        //            var doc = JsonDocument.Parse(json);
        //            return doc.RootElement.GetProperty("token").GetString();
        //        }
        private async Task<string> CreatePaymentKeyAsync(string authToken, int orderId, decimal amount)
        {
            var payload = new
            {
                auth_token = authToken,
                amount_cents = (int)(amount * 100),
                expiration = 3600,
                order_id = orderId,
                billing_data = new
                {
                    first_name = "Student",
                    last_name = "Name",
                    email = "student@test.com",
                    phone_number = "01000000000",
                    apartment = "NA",
                    floor = "NA",
                    street = "NA",
                    building = "NA",
                    shipping_method = "NA",
                    city = "Cairo",
                    country = "EG",
                    state = "Cairo",
                    zip_code = "00000",
                },
                currency = "EGP",
                integration_id = _settings.IntegrationId,

                // 🔹 هنا بنضيف return_url ديناميكي endpoint redirect
                return_url = "https://adros-backend/api/payments/redirect"
            };

            var response = await _httpClient.PostAsync(
                "https://accept.paymob.com/api/acceptance/payment_keys",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("token").GetString();
        }


    }
}

