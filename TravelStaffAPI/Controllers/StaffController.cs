using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.StatusDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _IStaffService;
        private readonly IMapper _mapper;


        public StaffController(IStaffService staffService, IMapper mapper)
        {
            _IStaffService = staffService;
            _mapper = mapper;
        }

        [HttpGet("getallstaffs")]
        public IActionResult GetAllStaffs()
        {
            var allStaff = _IStaffService.TGetAll();
            var values = allStaff.Where(staff => !staff.IsAdmin).ToList();
            var staff = _mapper.Map<List<StaffListDto>>(values);
            return Ok(staff);
        }

        [HttpGet("getalladmins")]
        public IActionResult GetAllAdmins()
        {
            var allStaff = _IStaffService.TGetAll();
            var values = allStaff.Where(admin => admin.IsAdmin).ToList();
            var admin = _mapper.Map<List<StaffListDto>>(values);
            return Ok(admin);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var status = _IStaffService.TGetById(id);
            return Ok(status);
        }

        [HttpPost("create")]

        public IActionResult Create(CreateStaffDto staff)
        {
            if (ModelState.IsValid)
            {
                var newStaff = new Staff
                {
                    Name = staff.Name,
                    Surname = staff.Surname,
                    IsAdmin = staff.IsAdmin,
                    AdminID = staff.AdminID
                };

                _IStaffService.TAdd(newStaff);
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("update")]
        public IActionResult Update(Staff staff)
        {
            _IStaffService.TUpdate(staff);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var staff = await _IStaffService.TGetById(id);
            _IStaffService.TDelete(staff);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}