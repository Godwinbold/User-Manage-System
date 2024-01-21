namespace UserManagement_CodeWithSL.Services.Iinterface
{
	public interface IEmailService
	{
		Task<string> sendEmailAsync(string recipientEmail, string subject, string body);
	}
}
