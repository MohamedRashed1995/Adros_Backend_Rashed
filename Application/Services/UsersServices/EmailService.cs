
using Adros.Application.Interfaces.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Adros.Application.Services.UsersServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendPasswordResetEmailAsync(string email, string userName, string resetUrl)
        {
            var subject = "إعادة تعيين كلمة المرور - منصة ادرس";

            var body = $"""
            <div dir="rtl" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
                     max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;">
                
                <div style="text-align: center; margin-bottom: 30px;">
                    <h2 style="color: #4CAF50;">إعادة تعيين كلمة المرور</h2>
                </div>
                
                <div style="margin-bottom: 25px;">
                    <p>مرحباً <strong>{userName}</strong>,</p>
                    <p>لقد تلقينا طلباً لإعادة تعيين كلمة المرور لحسابك في منصة <strong>ادرس</strong>.</p>
                    <p>انقر على الزر أدناه لإعادة تعيين كلمة المرور:</p>
                </div>
                
                <div style="text-align: center; margin: 30px 0;">
                    <a href="{resetUrl}" 
                       style="background-color: #4CAF50; color: white; padding: 12px 30px; 
                              text-decoration: none; border-radius: 5px; font-weight: bold;
                              display: inline-block;">
                       إعادة تعيين كلمة المرور
                    </a>
                </div>
                
                <div style="margin-top: 30px; padding-top: 20px; border-top: 1px solid #eee; 
                     color: #666; font-size: 14px;">
                    <p><strong>ملاحظة:</strong></p>
                    <ul>
                        <li>إذا لم تطلب إعادة تعيين كلمة المرور، يمكنك تجاهل هذا الإيميل.</li>
                        <li>ينتهي صلاحية هذا الرابط خلال <strong>24 ساعة</strong>.</li>
                        <li>لن يتم تغيير كلمة المرور إلا إذا قمت بالضغط على الرابط أعلاه.</li>
                    </ul>
                </div>
                
                <div style="margin-top: 30px; text-align: center; color: #888; font-size: 12px;">
                    <p>مع تحيات فريق منصة <strong>ادرس</strong> التعليمية</p>
                    <p>هذا إيميل تلقائي، يرجى عدم الرد عليه</p>
                </div>
                
            </div>
            """;

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendPasswordChangedConfirmationAsync(string email, string userName)
        {
            var subject = "تم تغيير كلمة المرور بنجاح - منصة ادرس";

            var body = $"""
            <div dir="rtl" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
                     max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;">
                
                <div style="text-align: center; margin-bottom: 30px;">
                    <h2 style="color: #4CAF50;">تم تغيير كلمة المرور بنجاح</h2>
                </div>
                
                <div style="margin-bottom: 25px;">
                    <p>مرحباً <strong>{userName}</strong>,</p>
                    <p>تم تغيير كلمة المرور لحسابك في منصة <strong>ادرس</strong> بنجاح.</p>
                    <p>يمكنك الآن تسجيل الدخول باستخدام كلمة المرور الجديدة.</p>
                </div>
                
                <div style="margin-top: 30px; padding-top: 20px; border-top: 1px solid #eee; 
                     color: #666; font-size: 14px;">
                    <p><strong>ملاحظة أمنية:</strong></p>
                    <ul>
                        <li>إذا لم تقم بهذا التغيير، يرجى <strong>التواصل مع الدعم الفوري</strong>.</li>
                        <li>ننصح بعدم مشاركة كلمة المرور مع أي شخص.</li>
                        <li>استخدم كلمة مرور قوية تحتوي على أحرف كبيرة وصغيرة وأرقام ورموز.</li>
                    </ul>
                </div>
                
                <div style="margin-top: 30px; text-align: center; color: #888; font-size: 12px;">
                    <p>مع تحيات فريق منصة <strong>ادرس</strong> التعليمية</p>
                    <p>هذا إيميل تلقائي، يرجى عدم الرد عليه</p>
                </div>
                
            </div>
            """;

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");

                var fromEmail = emailSettings["SenderEmail"] ?? "noreply@adros.com";
                var fromName = emailSettings["SenderName"] ?? "منصة ادرس";

                var message = new MailMessage
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                message.To.Add(to);

                using var smtpClient = new SmtpClient(emailSettings["Host"])
                {
                    Port = int.Parse(emailSettings["Port"] ?? "587"),
                    Credentials = new NetworkCredential(
                        emailSettings["Username"],
                        emailSettings["Password"]),
                    EnableSsl = bool.Parse(emailSettings["EnableSsl"] ?? "true")
                };

                await smtpClient.SendMailAsync(message);

                _logger.LogInformation($"Email sent successfully to {to}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {to}");
                // لا نرمي Exception هنا حتى لا نكشف للمستخدم مشاكل الإيميل
            }
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml)
        {
            // Mock implementation - just log the email
            _logger.LogInformation("Mock Email Sent: To: {ToEmail}, Subject: {Subject}", toEmail, subject);
            _logger.LogDebug("Email Body: {Body}", body);

            await Task.CompletedTask;
        }
    }
}