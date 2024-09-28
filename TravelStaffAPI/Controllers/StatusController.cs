using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.StatusDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _IStatusService;
        private readonly IMapper _mapper;

        public StatusController(IStatusService statusService, IMapper mapper)
        {
            _IStatusService = statusService;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var status = _mapper.Map<List<StatusListDto>>(_IStatusService.TGetAll());
            return Ok(status);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var status = _IStatusService.TGetById(id);
            return Ok(status);
        }

        [HttpPost("create")]

        public IActionResult Create(CreateStatusDto status)
        {
            if (ModelState.IsValid)
            {
                _IStatusService.TAdd(new Status { Information = status.Information });
                return StatusCode(StatusCodes.Status201Created);
            }
            
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("update")]
        public IActionResult Update(Status status)
        {
            _IStatusService.TUpdate(status);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _IStatusService.TGetById(id);
            _IStatusService.TDelete(status);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
