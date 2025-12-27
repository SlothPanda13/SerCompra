using System.Net;
using System.Net.Mail;

namespace SerCompra.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            string emailOrigen = emailSettings["Email"];
            string password = emailSettings["Password"];
            string host = emailSettings["Host"];
            int port = int.Parse(emailSettings["Port"]);

            var mailMessage = new MailMessage(emailOrigen, to, subject, body)
            {
                IsBodyHtml = true
            };

            using (var smtpClient = new SmtpClient(host, port))
            {
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailOrigen, password);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}