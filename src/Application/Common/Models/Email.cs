using System.Collections.Generic;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Common.Models
{
	public class Email : IMessage
	{
		public IEnumerable<string> To { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }

		public IFormFileCollection Attachments { get; set; }

		public Email(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
		{
			To = to;
			Subject = subject;
			Content = content;
			Attachments = attachments;
		}
	}
}