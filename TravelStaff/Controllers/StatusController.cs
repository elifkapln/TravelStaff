using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StatusDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaff.Controllers
{
	[Authorize(Roles = "Manager, Admin")]
	public class StatusController : BaseController
	{
		private readonly IStatusService _statusService;
		private readonly IMapper _mapper;
		private readonly UserManager<Staff> _userManager;

		public StatusController(IStatusService statusService, IMapper mapper, UserManager<Staff> userManager) : base(userManager)
		{
			_statusService = statusService;
			_mapper = mapper;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			var status = _mapper.Map<List<StatusListDto>>(_statusService.TGetAll());
			return View(status);
		}

		[HttpGet]
		public IActionResult AddStatus()
		{
			return View();
		}		
		
		[HttpPost]
		public IActionResult AddStatus(Status status)
		{
			status.Active= true;
			_statusService.TAdd(status);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> UpdateStatus(int id)
		{
			var values = _mapper.Map<StatusListDto>(await _statusService.TGetById(id));
			return View(values);
		}

		[HttpPost]
		public IActionResult UpdateStatus(Status status)
		{
			status.Active= true;
			_statusService.TUpdate(status);
			return RedirectToAction("Index");
		}

		public IActionResult MakeActive(int id)
		{
			_statusService.TMakeActiveStatus(id);
			return RedirectToAction("Index");
		}

		public IActionResult MakePassive(int id)
		{
			_statusService.TMakePassiveStatus(id);
			return RedirectToAction("Index");
		}
	}
}