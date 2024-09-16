using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
	[AllowAnonymous]
	[Route("api/[controller]")]
	[ApiController]
	public class MessageController : ControllerBase
	{
		private readonly IMessageService _messageService;
		private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageController(IMessageService messageService, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _messageService = messageService;
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet("getall")]
		public IActionResult GetAll()
		{
			var values = _mapper.Map<List<GetMessageDto>>(_messageService.TGetAll());
			return Ok(values);
		}

        [HttpGet("getallbytravel/{travelId}")]
        public async Task<IActionResult> GetMessagesByTravel(int travelId)
        {
			var messages = await _messageService.TGetMessagesByTravelId(travelId);
            //var messages = _messageService.TGetAll().Where(m => m.TravelID == travelId).ToList();

            var values = _mapper.Map<List<GetMessageDto>>(messages);
            return Ok(values);
        }

        [HttpGet("getlatest/{travelId}")]
        public async Task<IActionResult> GetLatestMessageByTravel(int travelId)
        {
            //var latestMessage = await _messageService.TGetLatestMessageByTravelId(travelId);

            //var values = _mapper.Map<GetMessageDto>(latestMessage);
            //return Ok(values);
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedMessageService = scope.ServiceProvider.GetRequiredService<IMessageService>();

                var latestMessage = await scopedMessageService.TGetLatestMessageByTravelId(travelId);
                var values = _mapper.Map<GetMessageDto>(latestMessage);
                return Ok(values);
            }
        }


        [HttpGet("getbyid/{id}")]
		public async Task<IActionResult> GetById(int id)
		{

			var value = _mapper.Map<GetMessageDto>(await _messageService.TGetById(id));
			return Ok(value);
		}

		[HttpPost("create")]
		public IActionResult Create(CreateMessageDto message)
		{
			if (ModelState.IsValid)
			{
				_messageService.TAdd(new Message
				{
					Content = message.Content,
					SendDate = DateTime.Now,
					FromAdmin = message.FromAdmin,
					Active = true,
					TravelID = message.TravelID,
				});
				return StatusCode(StatusCodes.Status201Created);
			}
			return BadRequest(ModelState);
			//return StatusCode(StatusCodes.Status400BadRequest);
		}

		[HttpPut("update")]
		public IActionResult Update(Message message)
		{
			_messageService.TUpdate(message);
			return StatusCode(StatusCodes.Status200OK);
		}

		[HttpDelete("delete")]
		public IActionResult Delete(Message message)
		{
			_messageService.TDelete(message);
			return StatusCode(StatusCodes.Status200OK);
		}
	}
}
