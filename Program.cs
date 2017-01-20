using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ConsoleApplication
{
	class SmtpOptions
	{
		public string Server { get; set; }
		public int Port { get; set; }
		public SecureSocketOptions UseSsl { get; set; }
	}
    
    public class Program
    {
        public static void Main(string[] args)
        {
            Send().Wait();
        }

        private static async Task Send()
        {
            var smtpOptions = new SmtpOptions
			{
				Server = "172.20.0.133",
				Port = 25,
				UseSsl = SecureSocketOptions.None
			};

			var message = new MimeMessage();
			
			message.From.Add(new MailboxAddress("Prueba", "from@gmail.com.com"));
			message.ReplyTo.Add(new MailboxAddress("Reply To", "reply-to@gmail.com"));
			message.To.Add(new MailboxAddress("Rodrigo Rivera", "rrivem@gmail.com"));

			message.Subject = "Prueba";

			var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Texto plano " + DateTime.Now.ToString();
			bodyBuilder.HtmlBody = "<h1>Esto es HTML</h1><p>Hello</p>";
			message.Body = bodyBuilder.ToMessageBody();
			
			using (var client = new SmtpClient())
			{
				await client
					.ConnectAsync(smtpOptions.Server, smtpOptions.Port, smtpOptions.UseSsl);
				
				await client.SendAsync(message);

				await client.DisconnectAsync(true);
			}
        }
    }
}
