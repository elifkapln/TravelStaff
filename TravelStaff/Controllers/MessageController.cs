using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaff.Controllers
{
	public class MessageController : BaseController
	{
		public MessageController(UserManager<Staff> userManager) : base(userManager)
		{
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			return View();
		}
	}
}