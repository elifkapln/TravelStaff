using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaff.Controllers
{
	public class TravelStatusUpdateController : BackgroundService
	{
		private readonly ITravelService _travelService;

		public TravelStatusUpdateController(ITravelService travelService)
		{
			_travelService = travelService;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				await UpdateTravelStatusAsync();
				await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Her 24 saatte bir çalışacak
			}
		}

		private async Task UpdateTravelStatusAsync()
		{
			var currentDate = DateTime.Now.Date;

			// Bitiş tarihi geçmiş seyahatleri bul ve güncelle
			var travels = await _travelService.TGetAllActiveTravels();
			var expiredTravels = travels.Where(t => t.EndDate < currentDate).ToList();

			if (expiredTravels.Any())
			{
				foreach (var travel in expiredTravels)
				{
					travel.Active = false;
					_travelService.TUpdate(travel);
				}

				// İsteğe bağlı: Güncellenen seyahatlerin logunu tutabilirsiniz
				Console.WriteLine($"{expiredTravels.Count} seyahat güncellendi ve pasif yapıldı.");
			}
			else
			{
				// İsteğe bağlı: Boş liste olduğunda log veya aksiyon alabilirsiniz
				Console.WriteLine("Güncellenecek seyahat yok.");
			}
		}
	}
}