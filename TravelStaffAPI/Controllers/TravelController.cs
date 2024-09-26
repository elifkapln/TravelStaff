using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TravelStaffAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        private readonly ITravelService _travelService;
        private readonly IMapper _mapper;
        public TravelController(ITravelService travelService, IMapper mapper)
        {
            _travelService = travelService;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var values = _mapper.Map<List<TravelListDto>>(_travelService.TGetAll());
			//var jsonTravel = JsonConvert.SerializeObject(values);
			return Ok(values);
		}

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {

            var travel = await _travelService.TGetById(id);
            return Ok(travel);
        }

        [HttpGet("gettravelbyadmin/{id}")]
        public async Task<IActionResult> GetTravelByAdmin(int id)
        {
            var travel = _mapper.Map<List<TravelListDto>>(await _travelService.TGetTravelByAdmin(id));
            return Ok(travel);
        }

        [HttpPost("create")]
        public IActionResult Create(CreateTravelDto travel)
        {
            if (ModelState.IsValid)
            {
                _travelService.TAdd(new Travel 
                { City = travel.City, 
                  StartDate = travel.StartDate, 
                  EndDate = travel.EndDate, 
                  Description = travel.Description, 
                  Stay = travel.Stay, 
                  Vehicle = travel.Vehicle,
                  //StaffID = travel.StaffID,
                  //AdminID = travel.AdminID,
                  //StatusID = travel.StatusID,
                  //Active = true,
                  //CreateDate = DateTime.Now,

                });
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest(ModelState);
            //return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("update")]
        public IActionResult Update(Travel travel)
        {
            _travelService.TUpdate(travel);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var travel = await _travelService.TGetById(id);
            _travelService.TDelete(travel);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}