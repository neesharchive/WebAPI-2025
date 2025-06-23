using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using WebAPI_2025.Data;

namespace WebAPI_2025.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public void SendEmailInBackground(string toEmail, string subject, string body)
        {
            // Fire-and-forget using Task.Run
            _ = Task.Run(async () =>
            {
                try
                {
                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
                    email.To.Add(MailboxAddress.Parse(toEmail));
                    email.Subject = subject;
                    email.Body = new TextPart(TextFormat.Html) { Text = body };

                    using var smtp = new SmtpClient();
                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);

                    Console.WriteLine("Email sent (background).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Email Error - Background] {ex.Message}");
                    // Optional: log to DB or file
                }
            });
        }
    }
}
