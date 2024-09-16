using EntityLayer.Concrete;
using EntityLayer.DTOs.ProfileDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaff.Controllers
{
	public class ProfileController : BaseController
	{
		private readonly UserManager<Staff> _userManager;

		public ProfileController(UserManager<Staff> userManager) :base(userManager)
		{
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> UpdateProfile() 
		{
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			GetProfileDto getProfileDto = new GetProfileDto();
			getProfileDto.Name = values.Name;
			getProfileDto.Surname = values.Surname;
			getProfileDto.PhoneNumber = values.PhoneNumber;
			getProfileDto.Mail = values.Email;
			return View(getProfileDto);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProfile(GetProfileDto getProfileDto)
		{
			var user = await _userManager.FindByNameAsync(User.Identity?.Name);
			user.Name = getProfileDto.Name;
			user.Surname = getProfileDto.Surname;
			user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, getProfileDto.Password);
			if (getProfileDto.Password == getProfileDto.ConfirmPassword)
			{
				var result = await _userManager.UpdateAsync(user);

				if (result.Succeeded)
				{
					return RedirectToAction("SignIn", "Login");
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
			return View();
		}
	}
}
