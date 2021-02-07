
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitecture.Infrastructure.Hubs
{
	public class SampleHub : Hub
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

	}

}