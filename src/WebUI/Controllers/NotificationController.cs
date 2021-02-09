using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Notification.Queries.GetNotification;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
	public class NotificationController : ApiController
	{

		[HttpPost("/api/notification")]
		public async Task<ActionResult<Result>> Get(GetNotificationQuery query)
		{
			return await Mediator.Send(query);
		}

	}
}