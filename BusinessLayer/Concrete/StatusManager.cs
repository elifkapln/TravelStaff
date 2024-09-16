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
	public class StatusManager : IStatusService
	{
		private readonly IStatusDal _IStatusDal;

		public StatusManager(IStatusDal statusDal)
		{
			_IStatusDal = statusDal;
		}

		public void TAdd(Status t)
		{
			_IStatusDal.Insert(t);
		}
		public void TUpdate(Status t)
		{
			_IStatusDal.Update(t);
		}
		public void TDelete(Status t)
		{
			_IStatusDal.Delete(t);
		}

		public List<Status> TGetAll()
		{
			return _IStatusDal.GetAll();
		}

		public async Task<Status> TGetById(int id)
		{
			return await _IStatusDal.GetById(id);
		}

		public void TMakePassiveStatus(int id)
		{
			_IStatusDal.MakePassiveStatus(id);
		}

		public void TMakeActiveStatus(int id)
		{
			_IStatusDal.MakeActiveStatus(id);
		}
	}
}
