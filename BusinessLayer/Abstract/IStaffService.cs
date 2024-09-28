using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStaffService : IGenericService<Staff> 
    {
		List<Staff> TGetAllAdmins();
		List<Staff> TGetAllAdminsStaffs(int id);


	}
}
