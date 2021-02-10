using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Common.Models
{
	public class Sms : IMessage
	{
		public string To { get; set; }
		public string From { get; set; }
		public string Message { get; set; }
	}
}