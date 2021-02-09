using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using MimeKit;

namespace CleanArchitecture.Infrastructure.Communication.Email
{
	public class Email : IMessage
	{
		public List<MailboxAddress> To { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }

		public IFormFileCollection Attachments { get; set; }

		public Email(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
		{
			To = new List<MailboxAddress>();

			To.AddRange(to.Select(x => new MailboxAddress(string.Empty, x)));
			Subject = subject;
			Content = content;
			Attachments = attachments;
		}
	}
}