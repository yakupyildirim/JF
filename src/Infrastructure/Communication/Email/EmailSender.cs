using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace CleanArchitecture.Infrastructure.Communication.Email
{
	public class EmailSender : ICommunication
	{
		private readonly EmailConfiguration _emailConfig;

		public EmailSender(EmailConfiguration emailConfig)
		{
			_emailConfig = emailConfig;
		}
		public override async Task<Result> Send(IMessage message)
		{
			var email = (Email)message;
			var mailMessage = CreateEmailMessage(email);
			return await SendAsync(mailMessage);

		}

		private MimeMessage CreateEmailMessage(Email message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(string.Empty, _emailConfig.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;

			var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

			if (message.Attachments != null && message.Attachments.Any())
			{
				byte[] fileBytes;
				foreach (var attachment in message.Attachments)
				{
					using (var ms = new MemoryStream())
					{
						attachment.CopyTo(ms);
						fileBytes = ms.ToArray();
					}

					bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
				}
			}

			emailMessage.Body = bodyBuilder.ToMessageBody();
			return emailMessage;
		}

		private async Task<Result> SendAsync(MimeMessage mailMessage)
		{
			using (var client = new SmtpClient())
			{
				try
				{
					await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

					await client.SendAsync(mailMessage);
				}
				catch
				{
				  throw;
				}
				finally
				{
					await client.DisconnectAsync(true);
					client.Dispose();
				}
				 return Result.Success();
			}
		}
	}
}