namespace WebAPI_2025.Services
{
        public interface IEmailService
        {
            void SendEmailInBackground(string toEmail, string subject, string body);
        }

    
}
