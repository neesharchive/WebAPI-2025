namespace WebAPI_2025.Services
{
        public interface IEmailService
        {
            Task SendEmailAsync(string toEmail, string subject, string body);
        }

    
}
