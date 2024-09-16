using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;

namespace TravelStaff.Controllers
{
	public class BaseController : Controller
	{
		private readonly UserManager<Staff> _userManager;

		public BaseController(UserManager<Staff> userManager)
		{
			_userManager = userManager;
		}

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var user = await _userManager.FindByNameAsync(User.Identity?.Name);
			if (user != null)
			{
				ViewBag.isAdmin = user.IsAdmin;
				ViewBag.nameSurname = user.Name + " " + user.Surname;
			}

			await next();
		}
	}
}