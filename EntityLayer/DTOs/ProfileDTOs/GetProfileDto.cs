using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.ProfileDTOs
{
	public class GetProfileDto
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string PhoneNumber { get; set; }
		public string Mail { get; set; }
	}
}
