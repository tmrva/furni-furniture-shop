namespace furni1
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string main, string email, string subject, string message);
	}
}
