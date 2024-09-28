using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EfStaffDal : GenericRepository<Staff>, IStaffDal
	{
		private readonly Context c;

		public EfStaffDal(Context c) :base(c)
		{
			this.c = c;
		}

		public List<Staff> GetAllAdmins()
		{
			return c.Staffs.Where(s => s.IsAdmin).ToList();
		}

		public List<Staff> GetAllAdminsStaffs(int id)
		{
			return c.Staffs.Where(x=>x.AdminID == id).ToList();
		}
	}
}