using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Hubs
{
	public class NotificationService : INotificationService
	{
		private readonly IHubContext<SampleHub> _sampleHub;

		public NotificationService(IHubContext<SampleHub> sampleHub)
		{
			_sampleHub = sampleHub;
		}

		public async Task GetSampleMessages()
		{

			var messagePayload = new MessagePayload()
			{
				Message = "Hi",
				Title = "Agreement"
			};

			var notificaition = new Notification<MessagePayload>()
			{
				NotificationType = Domain.Enums.NotificationType.Like,
				Payload = messagePayload
			};
		

			await _sampleHub.Clients.All.SendAsync("sampleHub", notificaition);

		}

	}
}
