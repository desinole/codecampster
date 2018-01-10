using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace Codecamp2018.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string server, string username, string password,
            string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Orlando Codecamp", username));
            mimeMessage.To.Add(new MailboxAddress(email, email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                client.Connect(server, 587, false);

                // We don't have an OAuth2 token, so we've disabled it
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(username, password);

                client.Send(mimeMessage);
                client.Disconnect(true);
            }

            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

}
