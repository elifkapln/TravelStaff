using Microsoft.AspNetCore.SignalR;

namespace TravelStaff.Controllers
{
	public class MessageHub:Hub
	{
		// Bu metot mesaj gönderildiğinde çağrılacak
		public async Task SendMessage(string message)
		{
			// Tüm bağlı istemcilere (users) mesaj gönder
			await Clients.All.SendAsync("ReceiveMessage", message);
		}
	}
}
