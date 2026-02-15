//using Adros.Application.Interfaces.IService;
//using Microsoft.Extensions.Logging;
//using System.Threading.Tasks;

//namespace Adros.Application.Services
//{
//    //public class MockEmailService : IEmailService
//    //{
//    //    private readonly ILogger<MockEmailService> _logger;

//    //    public MockEmailService(ILogger<MockEmailService> logger)
//    //    {
//    //        _logger = logger;
//    //    }

//    //    public async Task SendEmailAsync(string toEmail, string subject, string body)
//    //    {
//    //        await SendEmailAsync(toEmail, subject, body, true);
//    //    }

//    //    public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml)
//    //    {
//    //        // Mock implementation - just log the email
//    //        _logger.LogInformation("Mock Email Sent: To: {ToEmail}, Subject: {Subject}", toEmail, subject);
//    //        _logger.LogDebug("Email Body: {Body}", body);

//    //        await Task.CompletedTask;
//    //    }
//    }
//}


// في Application/Services/
using Adros.Application.Interfaces.IService;
using Microsoft.Extensions.Logging;

namespace Adros.Application.Services
{
    public class MockEmailService : IEmailService
    {
        private readonly ILogger<MockEmailService> _logger;

        public MockEmailService(ILogger<MockEmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            await SendEmailAsync(toEmail, subject, body, true);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml)
        {
            // Mock implementation - just log the email
            _logger.LogInformation("📧 Mock Email Sent: To: {ToEmail}, Subject: {Subject}", toEmail, subject);
            _logger.LogDebug("Email Body: {Body}", body);

            await Task.CompletedTask;
        }

        public async Task SendPasswordResetEmailAsync(string email, string userName, string resetUrl)
        {
            _logger.LogInformation("📧 Password Reset Email: {Email}, Reset URL: {ResetUrl}", email, resetUrl);
            await Task.CompletedTask;
        }

        public async Task SendPasswordChangedConfirmationAsync(string email, string userName)
        {
            _logger.LogInformation("📧 Password Changed Confirmation: {Email}", email);
            await Task.CompletedTask;
        }
    }
}