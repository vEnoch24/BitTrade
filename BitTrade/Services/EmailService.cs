using BitTrade.Configurations;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using BitTrade.Dto;

namespace BitTrade.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }

        public async Task<bool> SendEmailAsync(EmailDto request)
        {
            try
            {
                using (var smtp = new SmtpClient())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                    var email = new MimeMessage();
                    email.From.Add(emailFrom);
                    email.To.Add(MailboxAddress.Parse(request.To));
                    email.Subject = request.Subject;
                    email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                    await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                    await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
