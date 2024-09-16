using EntityLayer.DTOs.TravelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.MessageDTOs
{
	public class TravelPlusMessageDto
	{
        public AdminTravelListDto? TravelDto { get; set; }
        public List<GetMessageDto>? MessageDto { get; set; }
        public CreateMessageDto? CreateMessageDto { get; set; }
    }
}