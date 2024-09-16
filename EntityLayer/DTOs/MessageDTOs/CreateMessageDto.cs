using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.MessageDTOs
{
	public class CreateMessageDto
	{
		public string Content { get; set; }
		public int TravelID { get; set; }
		public bool FromAdmin { get; set; }
	}
}
