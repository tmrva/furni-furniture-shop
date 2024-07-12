using System.Net.Mail;
using System.Net;

namespace furni1
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string main, string email, string subject, string message)
		{
			string password = "your-16-symbol-password";
			
			var client = new SmtpClient("smtp.gmail.com", 587)
			{
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(main, password)
			};
			return client.SendMailAsync(new MailMessage(from: main, to: email, subject, message));
		}
	}
}
