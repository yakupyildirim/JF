
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitecture.Infrastructure.Communication.NotificationSender
{
	public class ChartHub : Hub
	{
		public override Task OnConnectedAsync()
		{
			System.Console.WriteLine("Yeni bir bağlantı: " + Context.ConnectionId);
			Clients.All.SendAsync("YeniBaglanti", "Yeni bir giriş algılandı.", Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(System.Exception exception)
		{
			System.Console.WriteLine("Kapatılan bağlantı: " + Context.ConnectionId);
			Clients.All.SendAsync("KapatilanBaglanti", "Bağlantı kapatıldı.", Context.ConnectionId);
			return base.OnDisconnectedAsync(exception);
		}


		public async Task BroadcastChartData(List<Chart> data) =>
							 await Clients.All.SendAsync("broadcastchartdata", data);


	}

}