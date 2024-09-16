using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EfStatusDal : GenericRepository<Status>, IStatusDal
	{
		private readonly Context c;

		public EfStatusDal(Context c) :base(c)
		{
			this.c = c;
		}

		public void MakeActiveStatus(int id)
		{
			var values = c.Statuses.Find(id);
			values.Active = true;
			c.Update(values);
			c.SaveChanges();
		}

		public void MakePassiveStatus(int id)
		{
			var values = c.Statuses.Find(id);
			values.Active = false;
			c.Update(values);
			c.SaveChanges();
		}
	}
}
