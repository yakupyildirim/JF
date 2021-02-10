using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CleanArchitecture.Infrastructure.Communication.SmsSender
{
	public class SmsService : ICommunication
	{
		private readonly SmsConfiguration _smsConfig;

		public SmsService(SmsConfiguration smsConfig)
		{
			_smsConfig = smsConfig;
		}

		public override async Task<Result> Send(IMessage message)
		{
			var sms = (Sms)message;

			TwilioClient.Init(_smsConfig.AccountSid, _smsConfig.AuthToken);

			var sendMessage =  MessageResource.Create(
					body: sms.Message,
					from: new Twilio.Types.PhoneNumber(sms.From),
					to: new Twilio.Types.PhoneNumber(sms.To)
			);
			
			await Task.Delay(1);
			return Result.Success();

		}
	}
}