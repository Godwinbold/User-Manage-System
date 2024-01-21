using MailKit.Net.Smtp;
using MimeKit;
using UserManagement_CodeWithSL.Services.Iinterface;

namespace UserManagement_CodeWithSL.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _config;
		public EmailService( IConfiguration configuration) 
		{ 
			_config = configuration;

		}

		public async Task<string> sendEmailAsync(string recipientEmail, string subject, string body)
		{
			try
			{
				var senderemail = _config.GetSection("EmailSettings:sender").Value;
				var port = Convert.ToInt32(_config.GetSection("EmailSettings:port").Value);
				var host = _config.GetSection("EmailSettings:host").Value;
				var appPassword = _config.GetSection("EmailSettings:appPassword").Value;


				var email = new MimeMessage();
				email.Sender = (MailboxAddress.Parse(recipientEmail));
				email.To.Add(MailboxAddress.Parse(recipientEmail));
				email.Subject = subject;

				var builder = new BodyBuilder();
				builder.HtmlBody = body;
				email.Body = builder.ToMessageBody();

				using (var smtp = new SmtpClient())
				{
					smtp.CheckCertificateRevocation = true;
					await smtp.ConnectAsync(host, port);
					await smtp.AuthenticateAsync(senderemail, appPassword);
					await smtp.SendAsync(email);
					await smtp.DisconnectAsync(true);

				}
			} catch (Exception ex)
			{
				return ex.Message;
			}
			return "";
		}

	}
}
