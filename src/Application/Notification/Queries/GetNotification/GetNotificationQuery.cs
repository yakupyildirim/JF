using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Notification.Queries.GetNotification
{
	public class GetNotificationQuery : IRequest<Result>
	{
	}

	public class GetNotficationQueryHandler : IRequestHandler<GetNotificationQuery, Result>
	{
		private readonly ICommunication _communication;

		public GetNotficationQueryHandler(ICommunication communication, IMapper mapper)
		{
			_communication = communication;
		}

		public async Task<Result> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
		{
		   Sms sms = new Sms();
			return await _communication.Send(sms);
		}
	}
}
