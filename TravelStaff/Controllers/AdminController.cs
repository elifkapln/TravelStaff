using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace TravelStaff.Controllers
{
	[Authorize(Roles = "Manager, Admin")]
	public class AdminController : BaseController
	{
		private readonly IStaffService _staffService;
		private readonly ITravelService _travelService;
		private readonly IStatusService _statusService;
		private readonly IMapper _mapper;
		private readonly UserManager<Staff> _userManager;
		private readonly HttpClient _httpClient;

		public AdminController(IStaffService staffService, ITravelService travelService, IStatusService statusService, IMapper mapper, UserManager<Staff> userManager, HttpClient httpClient) : base(userManager)
		{
			_staffService = staffService;
			_travelService = travelService;
			_statusService = statusService;
			_mapper = mapper;
			_userManager = userManager;
			_httpClient = httpClient;

			//_httpClient.BaseAddress = new Uri("https://localhost:7143/");
		}

		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return RedirectToAction("Dashboard", "Index");
			}
			var staff = _mapper.Map<List<StaffListDto>>(_staffService.TGetAllAdminsStaffs(user.Id));
			return View(staff);

		}

		[HttpGet]
		public async Task<IActionResult> TravelDetails(int id)
		{
			var travels = await _travelService.TGetAllTravelWithStaffAndStatus(id);
			var values = _mapper.Map<List<AdminTravelListDto>>(travels);
			return View(values);
		}

		[HttpGet]
		public async Task<IActionResult> DisplayTravel(int id)
		//seyahat detayları
		{


            var travel = await _travelService.TGetTravelWithStaffAndStatus(id);
            if (travel == null)
            {
                return NotFound("Travel not found.");
            }
            var travelDto = _mapper.Map<AdminTravelListDto>(travel);

            //seyahat mesajları
            // Yeni bir HttpClient kullanarak mesajları al
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7143/");
            var response = await client.GetAsync($"api/message/getallbytravel/{id}");
            //var response = await _httpClient.GetAsync($"api/message/getallbytravel/{id}");
            if (!response.IsSuccessStatusCode)
			{
				return StatusCode((int)response.StatusCode, "Mesajları alırken bir hata oluştu.");
			}

			var messageDto = await response.Content.ReadFromJsonAsync<List<GetMessageDto>>();

			var travelPlusMessageDto = new TravelPlusMessageDto
			{
				TravelDto = travelDto,
				MessageDto = messageDto
			};

			var user = await _userManager.GetUserAsync(User);
			var userid = user.Id;
			HttpContext.Session.SetInt32("TravelID", id);
			HttpContext.Session.SetInt32("UserID", userid);
			ViewData["TravelID"] = id;

			//kullanıcı admin mi?
            ViewBag.UserIsAdmin = _travelService.TIsAdminOfTravel(id, userid);

            return View(travelPlusMessageDto);
		}

		[HttpPost]
		public async Task<IActionResult> DisplayTravel([FromBody] CreateMessageDto message)
		{
			if (ModelState.IsValid)
			{
				// Session'dan TravelID'yi al
				var travelId = HttpContext.Session.GetInt32("TravelID");
				var userId = HttpContext.Session.GetInt32("UserID");

				if (travelId == null || userId == null)
				{
					return BadRequest("Seyahat veya kullanıcı bilgisi bulunamadı.");
				}

				// Kullanıcı geçerli mi? bu kısım yerine travelın admini mi kontrolü yapılmalı
				var user = await _userManager.FindByIdAsync(userId.ToString());
				if (user == null)
				{
					return Unauthorized("Geçerli bir kullanıcı bulunamadı.");
				}

				bool fromAdmin = _travelService.TIsAdminOfTravel((int)travelId, (int)userId);

				var messageDto = new CreateMessageDto
				{
					Content = message.Content,
					TravelID = (int)travelId,
					FromAdmin = fromAdmin
				};

                // Yeni HttpClient kullanarak mesajı gönder
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7143/");
                var response = await client.PostAsJsonAsync("api/message/create", messageDto);
                //var response = await _httpClient.PostAsJsonAsync("api/message/create", messageDto);
                if (response.IsSuccessStatusCode)
				{
                    return StatusCode((int)response.StatusCode, "Mesaj veritabanına kaydedildi.");
                    //return Ok();
                    //return RedirectToAction("DisplayTravel", new { id = travelId });
                }
			}
			return BadRequest(ModelState);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateTravel(int id)
		{
			var staffs = _staffService.TGetAll();
			ViewBag.Staffs = staffs;

			var travel = await _travelService.TGetById(id);
			var values = _mapper.Map<AdminUpdateTravelDto>(travel);
			return View(values);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateTravel(AdminUpdateTravelDto travelDto)
		{
			var travel = await _travelService.TGetById(travelDto.TravelID);

			if (travelDto.StaffID != 0)
			{
				travel.StaffID = travelDto.StaffID;
			}

			_travelService.TUpdate(travel);
			return RedirectToAction("TravelDetails", new { id = travel.StaffID });
		}

		public async Task<IActionResult> MakeActive(int id)
		{
			var travel = await _travelService.TGetById(id);
			await _travelService.TMakeActiveTravel(id);
			return RedirectToAction("TravelDetails", new { id = travel.StaffID });
		}

		public async Task<IActionResult> MakePassive(int id)
		{
			var travel = await _travelService.TGetById(id);
			await _travelService.TMakePassiveTravel(id);
			return RedirectToAction("TravelDetails", new { id = travel.StaffID });
		}

		public async Task<IActionResult> ApproveTravel(int id)
		{
			var travel = await _travelService.TGetById(id);
			if (travel.StatusID != 1)
			{
				TempData["Message"] = "Seyahatin durumu zaten belirlendi.";
				return RedirectToAction("TravelDetails", new { id = travel.StaffID });
			}

			travel.StatusID = 2;
			_travelService.TUpdate(travel);
			TempData["Message"] = "Seyahat başarıyla onaylandı.";
			return RedirectToAction("TravelDetails", new { id = travel.StaffID });
		}

		public async Task<IActionResult> RejectTravel(int id, string rejectionReason)
		{
			var travel = await _travelService.TGetById(id);
			if (travel.StatusID != 1)
			{
				TempData["Message"] = "Seyahatin durumu zaten belirlendi.";
				return RedirectToAction("TravelDetails", new { id = travel.StaffID });
			}

			travel.StatusID = 3;
			travel.RejectionReason = rejectionReason;
			_travelService.TUpdate(travel);
			TempData["Message"] = "Seyahat başarıyla reddedildi.";
			return RedirectToAction("TravelDetails", new { id = travel.StaffID });
		}

		public async Task<IActionResult> GetTravelStatus(int id)
		{
			var travel = await _travelService.TGetById(id);
			if (travel == null)
			{
				return NotFound();
			}
			return Json(travel.StatusID);
		}

		//staff ve statusu ayrı ayrı güncellemek için yazıldı.
		//[HttpPost]
		//public IActionResult UpdateTravelStaff(AdminUpdateTravelDto travelDto)
		//{
		//	var travel = _travelService.TGetById(travelDto.TravelID);
		//	travel.StaffID = travelDto.StaffID;
		//	_travelService.TUpdate(travel);
		//	return RedirectToAction("TravelDetails", new { id = travel.StaffID });
		//}

		//[HttpPost]
		//public IActionResult UpdateTravelStatus(AdminUpdateTravelDto travelDto)
		//{
		//	var travel = _travelService.TGetById(travelDto.TravelID);
		//	travel.StatusID = travelDto.StatusID;
		//	_travelService.TUpdate(travel);
		//	return RedirectToAction("TravelDetails", new { id = travel.StaffID });
		//}
	}
}