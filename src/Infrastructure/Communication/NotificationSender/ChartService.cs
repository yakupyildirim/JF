using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Communication.NotificationSender
{
	public class ChartService : ICommunication
	{
		private readonly IHubContext<ChartHub> _chartHub;

		public ChartService(IHubContext<ChartHub> chartHub)
		{
			_chartHub = chartHub;
		}

		public override async Task<Result> Send()
		{
			var chart = new Notification<List<Chart>>()
			{
				Payload = DataManager.GetData()
			};

			var timerManager = new TimerManager(() =>
								 _chartHub.Clients.All.SendAsync("transferchartdata", chart));
      
			await Task.Delay(1);
			return Result.Success();

		}

	}
}
