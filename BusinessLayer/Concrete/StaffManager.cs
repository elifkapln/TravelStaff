using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class StaffManager : IStaffService
	{
		private readonly IStaffDal _IStaffDal;

		public StaffManager(IStaffDal iStaffDal)
		{
			_IStaffDal = iStaffDal;
		}


		public void TAdd(Staff t)
		{
			_IStaffDal.Insert(t);
		}

		public void TDelete(Staff t)
		{
			_IStaffDal.Delete(t);
		}

		public List<Staff> TGetAll()
		{
			return _IStaffDal.GetAll();
		}

		public List<Staff> TGetAllAdmins(List<Staff> users)
		{
			return _IStaffDal.GetAllAdmins(users);
		}

		public List<Staff> TGetAllAdminsStaffs(int id)
		{
			return _IStaffDal.GetAllAdminsStaffs(id);
		}

		public async Task<Staff> TGetById(int id)
		{
			return await _IStaffDal.GetById(id);
		}

		public void TUpdate(Staff t)
		{
			_IStaffDal.Update(t);
		}
	}
}
