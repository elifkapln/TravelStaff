using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaff.Controllers
{
	[AllowAnonymous]
    public class LoginController : Controller
    {
		private readonly UserManager<Staff> _userManager;
		private readonly SignInManager<Staff> _signInManager;
		private readonly IStaffService _staffService;

		public LoginController(UserManager<Staff> userManager, SignInManager<Staff> signInManager, IStaffService staffService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_staffService = staffService;
		}

		[HttpGet]
		public IActionResult SignUp()
		{
			//tüm adminleri al
			var users = _staffService.TGetAll();
			var admins = _staffService.TGetAllAdmins(users);

			//var admins = _userManager.Users
			//	.Where(x => x.IsAdmin == true)
			//	.Select(x => new
			//	{
			//		x.Id,
			//		x.Name,
			//		x.Surname
			//	})
			//	.DefaultIfEmpty().ToList();

			ViewBag.Admins = admins;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(CreateStaffDto p)
		{
			if (!ModelState.IsValid)
			{
				var message = ModelState.ToList();
				return View(p);
			}
			Staff appUser = new Staff()
			{
				Name = p.Name,
				Surname = p.Surname,
				Email = p.Mail,
				UserName = p.Username,
				AdminID = p.AdminID,
				IsAdmin = p.IsAdmin,
				Active = true

			};
			if (p.Password == p.ConfirmPassword)
			{
				var result = await _userManager.CreateAsync(appUser, p.Password);

				if (result.Succeeded)
				{
					return RedirectToAction("SignIn");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			else
			{
				ModelState.AddModelError("", "Şifre ve Şifre Tekrar uyuşmamaktadır.");
			}

			var users = _staffService.TGetAll();
			var adminsList = _staffService.TGetAllAdmins(users);

			//var adminsList = _userManager.Users
			//	.Where(x => x.IsAdmin == true)
			//	.Select(x => new
			//	{
			//		x.Id,
			//		x.Name,
			//		x.Surname
			//	})
			//	.DefaultIfEmpty().ToList();

			ViewBag.Admins = adminsList;
			return View(p);
		}

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(StaffLoginDto p)
		{
			if (ModelState.IsValid)
			{
				//var user = await _userManager.FindByNameAsync(p.Username);
				var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, true);
				if (result.Succeeded)
				{
					//await _signInManager.SignInAsync(user,false);
					return RedirectToAction("Index", "Dashboard");
				}
				else
				{
					ModelState.AddModelError("", "E-posta veya şifre yanlış.");
					//RedirectToAction("SignIn", "Login");
				}
			}
			return View();
		}

		//[HttpPost]
		//[ValidateAntiForgeryToken] // CSRF saldırılarına karşı koruma sağlar
		//public new async Task<IActionResult> SignOut()
		//{
		//	await _signInManager.SignOutAsync();
		//	return RedirectToAction("SignIn", "Login");
		//}
	}
}