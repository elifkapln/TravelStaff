using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<Staff> _userManager;
		private readonly SignInManager<Staff> _signInManager;

		public LoginController(UserManager<Staff> userManager, SignInManager<Staff> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("signup")]
        public async Task<IActionResult> SignUp(CreateStaffDto p)
        {
			p.Active = true;
			if(ModelState.IsValid)
			{
				Staff appUser = new Staff()
				{
					Name = p.Name,
					Surname = p.Surname,
					Email = p.Mail,
					UserName = p.Username,
					IsAdmin = p.IsAdmin,
				};
				if (p.Password == p.ConfirmPassword)
				{
					var result = await _userManager.CreateAsync(appUser, p.Password);

					if (result.Succeeded)
					{
						return StatusCode(StatusCodes.Status201Created);
					}
					else
					{
						foreach (var item in result.Errors)
						{
							ModelState.AddModelError("", item.Description);
						}
					}
				}
			}
			return BadRequest(ModelState);
		}

		//[HttpPost("signin")]
		//public async Task<IActionResult> SignIn(CreateStaffDto p)
	}
}