using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TravelStaff.Controllers
{
	public class TravelController : BaseController
	{
		private readonly ITravelService _travelService;
		private readonly IMapper _mapper;
		private readonly UserManager<Staff> _userManager;

		public TravelController(ITravelService travelService, IMapper mapper, UserManager<Staff> userManager) : base(userManager)
		{
			_travelService = travelService;
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);

			var travel = _mapper.Map<List<TravelListDto>>(await _travelService.TGetAllTravelByStaffid(user.Id));
			return View(travel);
		}

		public async Task<IActionResult> MyPassiveTravels()
		{
			var values = await _userManager.FindByNameAsync(User.Identity?.Name);
			var valuesList = _mapper.Map<List<TravelListDto>>(await _travelService.TGetPassiveTravelsByStaffid(values.Id));
			return View(valuesList);
		}

		[HttpGet]
		public IActionResult AddTravel()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddTravel(CreateTravelDto travel)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				if (ModelState.IsValid)
				{
					_travelService.TAdd(new Travel
					{
						StaffID = user.Id,
						AdminID = user.AdminID.Value,
						City = travel.City,
						StartDate = travel.StartDate,
						EndDate = travel.EndDate,
						CreateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
						Description = travel.Description,
						Stay = travel.Stay,
						Vehicle = travel.Vehicle,
						StatusID = 1,
						Active = true
					});
					return RedirectToAction("Index");
				}
			}
			return View();
		}

		public async Task <IActionResult> DeleteTravel(int id)
		{
			await _travelService.TMakePassiveTravel(id);
			TempData["SuccessMessage"] = "Seyahat başarıyla silindi.";
			return RedirectToAction("Index");

		}

		[HttpGet]
		public async Task<IActionResult> UpdateTravel(int id)
		{
			var values = _mapper.Map<UpdateTravelDto>(await _travelService.TGetById(id));
			return View(values);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateTravel(UpdateTravelDto updateTravelDto)
		{
			var travel = await _travelService.TGetById(updateTravelDto.TravelID);
			travel.City = updateTravelDto.City;
			travel.StartDate = updateTravelDto.StartDate;
			travel.EndDate = updateTravelDto.EndDate;
			travel.Description = updateTravelDto.Description;
			travel.Stay= updateTravelDto.Stay;
			travel.Vehicle = updateTravelDto.Vehicle;
			travel.StatusID = 1;

			_travelService.TUpdate(travel);
			return RedirectToAction("Index");
		}
	}
}