using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLayer.Abstract
{
    public interface ITravelService : IGenericService<Travel>
    {
        public Task<List<Travel>> TGetTravelByAdmin(int id);
		public Task TMakePassiveTravel(int id);
		public Task TMakeActiveTravel(int id);
		public Task<List<Travel>> TGetAllTravelByStaffid(int id);
		public Task<List<Travel>> TGetAllTravelByAdminid(int id);
		public Task<List<Travel>> TGetAllTravelWithStaffAndStatus(int id);
		public Task<Travel> TGetTravelWithStaffAndStatus(int id);
        public bool TIsAdminOfTravel(int travelId, int userId);
		public Task<List<Travel>> TGetPassiveTravelsByStaffid(int id);
		public Task<List<Travel>> TGetAllActiveTravels();


	}
}
