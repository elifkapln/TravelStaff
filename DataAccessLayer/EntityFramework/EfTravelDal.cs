using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfTravelDal : GenericRepository<Travel>, ITravelDal
    {
		private readonly Context c;

		public EfTravelDal(Context c) :base(c)
		{
			this.c = c;
		}

		public async Task<List<Travel>> GetTravelByAdmin(int id)
        {
            return await c.Travels
                .Where(x => x.AdminID == id)
                .Include(x => x.Admin)
                .DefaultIfEmpty().ToListAsync();
        }

		public async Task MakePassiveTravel(int id)
		{
			var values = await c.Travels.FindAsync(id);
			values.Active = false;
			c.Update(values);
			await c.SaveChangesAsync();
		}
        public async Task MakeActiveTravel(int id)
        {
            var values = await c.Travels.FindAsync(id);
            values.Active = true;
            c.Update(values);
            await c.SaveChangesAsync();
        }

		public async Task<List<Travel>> GetAllTravelByStaffid(int id)
		{
			return await c.Travels
				.Where(x=> x.StaffID == id && x.Active == true)
				.Include(x  => x.Status)
				.OrderByDescending(m => m.StatusID)
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<List<Travel>> GetPassiveTravelsByStaffid(int id)
		{
			return await c.Travels.Include(x => x.Status).OrderByDescending(m => m.StatusID)
				.Where(x => x.Active == false && x.StaffID == id)
				.ToListAsync();
		}

		public async Task<List<Travel>> GetAllActiveTravels()
		{
			return await c.Travels.Include(x => x.Status).OrderByDescending(m => m.StatusID)
				.Where(x => x.Active == true)
				.ToListAsync();
		}


		public async Task<List<Travel>> GetAllTravelByAdminid(int id)
		{
			return await c.Travels.Where(x=> x.AdminID == id).ToListAsync();

		}

		public async Task<List<Travel>> GetAllTravelWithStaffAndStatus(int id)
		{
			return await c.Travels
				.Where(x=>x.StaffID == id && x.Active == true)
				.OrderBy(m => m.StatusID)
				.Include(c => c.Staff)
				.Include (c => c.Status)
				.AsNoTracking() // Verilerin güncel haliyle çekilmesini sağlamak için eklendi.
				.ToListAsync();
		}

		public async Task<Travel> GetTravelWithStaffAndStatus(int id)
		{
            return await c.Travels
                .Include(t => t.Staff)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(t => t.TravelID == id);
        }

		public bool IsAdminOfTravel(int travelId, int userId)
		{
			var travel = c.Travels.FirstOrDefault(t => t.TravelID == travelId);

			if (travel != null)
			{
				return travel.AdminID == userId;
			}
			return false;
		}

	}
}