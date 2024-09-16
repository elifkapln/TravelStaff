using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.RoleDTOs
{
	public class AssignRoleDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Exist { get; set; }
	}
}
