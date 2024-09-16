using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.DTOs.RoleDTOs;
using EntityLayer.DTOs.StaffDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TravelStaff.Controllers
{
	[Authorize(Roles = "Manager")]
	public class RoleController : BaseController
	{
		private readonly RoleManager<AppRole> _roleManager;
		private readonly UserManager<Staff> _userManager;
		private readonly IMapper _mapper;

		public RoleController(RoleManager<AppRole> roleManager, UserManager<Staff> userManager, IMapper mapper) : base(userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			var values = _roleManager.Roles.ToList();
			return View(values);
		}

		[HttpGet]
		public IActionResult AddRole()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddRole(AddRoleDto roleDto)
		{
			if(ModelState.IsValid)
			{
				AppRole role = new AppRole
				{
					Name = roleDto.Name
				};

				var result = await _roleManager.CreateAsync(role);

				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				foreach (var item  in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}
			return View();
		}

        public async Task<IActionResult> DeleteRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var result = await _roleManager.DeleteAsync(values);
			if (result.Succeeded)
			{
                TempData["SuccessMessage"] = "Rol başarıyla silindi.";
                return RedirectToAction("Index");
            }
			return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(int id)
        {
			var values = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
			UpdateRoleDto roleDto = new UpdateRoleDto
			{
				Id = values.Id,
				Name = values.Name
			};
            return View(roleDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto roleDto)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == roleDto.Id);
            values.Name = roleDto.Name;
            var result = await _roleManager.UpdateAsync(values);
			if(result.Succeeded)
			{
                return RedirectToAction("Index");

            }
			return View(roleDto);
        }

		public IActionResult UserList()
		{
			var values = _mapper.Map<List<StaffListDto>>(_userManager.Users.ToList());
			return View(values);
		}

		[HttpGet]
		public async Task<IActionResult> AssignRole(int id)
		{
			//kullanıcı tablosu
			var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
			//rol tablosu
			var roles = _roleManager.Roles.ToList();
			//kullancıyı view'a gönderme
			TempData["UserId"] = user.Id;
			//kullanıcının rolleri
			var userRoles = await _userManager.GetRolesAsync(user);
			

			List<AssignRoleDto> assignRoleDto = new List<AssignRoleDto>();
			foreach (var item in roles)
			{
				AssignRoleDto dto = new AssignRoleDto();
				dto.Id = item.Id;
				dto.Name = item.Name;
				dto.Exist = userRoles.Contains(item.Name);
				assignRoleDto.Add(dto);
			}
			return View(assignRoleDto);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRole(List<AssignRoleDto> model)
		{
			var userid = (int)TempData["UserId"];

			var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
			foreach (var item in model)
			{
				if (item.Exist)
				{
					await _userManager.AddToRoleAsync(user, item.Name);
				}
				else
				{
					await _userManager.RemoveFromRoleAsync(user, item.Name);
				}
			}
			return RedirectToAction("UserList");
		}
	}
}