using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITravelDal : IGenericDal<Travel>
    {
        public Task<List<Travel>> GetTravelByAdmin(int id);
		public Task MakePassiveTravel(int id);
		public Task MakeActiveTravel(int id);
		public Task<List<Travel>> GetAllTravelByStaffid(int id);
		public Task<List<Travel>> GetAllTravelByAdminid(int id);
		public Task<List<Travel>> GetAllTravelWithStaffAndStatus(int id);
		public Task<Travel> GetTravelWithStaffAndStatus(int id);
        public bool IsAdminOfTravel(int travelId, int userId);
		public Task<List<Travel>> GetPassiveTravelsByStaffid(int id);
		public Task<List<Travel>> GetAllActiveTravels();

	}
}