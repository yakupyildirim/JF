using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Notification.Queries.GetNotification
{
	public class GetNotificationQuery : IRequest
	{
	}

	public class GetNotficationQueryHandler : IRequestHandler<GetNotificationQuery>
	{
		private readonly INotificationService _notificationService;

		public GetNotficationQueryHandler(INotificationService notificationService, IMapper mapper)
		{
			_notificationService = notificationService;
		}

		public async Task<Unit> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
		{
			await _notificationService.GetSampleMessages();

			return  Unit.Value;

		}
	}
}
