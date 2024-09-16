using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.TravelDTOs
{
	public class UpdateTravelDto
	{
		public int TravelID { get; set; }
		public string City { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Description { get; set; }
		public string Stay { get; set; }
		public string Vehicle { get; set; }
		public int StatusID { get; set; }

	}
}
