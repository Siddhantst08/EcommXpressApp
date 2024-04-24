using System.Net;
using System.Net.Mail;

namespace EcommApi.api.Services
{
    public class MailService
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            var mail = "ecommservice@outlook.com";
            var pass = "Test12345678";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                
                Credentials = new NetworkCredential(mail, pass)
            };
           // client.UseDefaultCredentials = false;

            return client.SendMailAsync(
                new MailMessage(from: mail,
                to: email, subject: subject, body: body)

                );
        }
    }
}
