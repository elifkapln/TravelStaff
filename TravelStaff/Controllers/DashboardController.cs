using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaff.Controllers
{

	public class DashboardController : BaseController
	{
        private readonly UserManager<Staff> _userManager;
        private readonly IStaffService _staffService;
        private readonly ITravelService _travelService;

		public DashboardController(UserManager<Staff> userManager, IStaffService staffService, ITravelService travelService) : base(userManager)
		{
			_userManager = userManager;
			_staffService = staffService;
			_travelService = travelService;
		}
		public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity?.Name);
            ViewBag.userName = values.UserName;
            ViewBag.email = values.Email;

			ViewBag.staffCount = _staffService.TGetAllAdminsStaffs(values.Id).Count;
            var travellist = await _travelService.TGetAllTravelByStaffid(values.Id);
			ViewBag.travelCount = travellist.Count;
            ViewBag.waitingTravelCount = travellist.Count(x => x.StatusID == 1);

			return View();
        }
    }
}
