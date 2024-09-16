using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class TravelManager : ITravelService
    {
        private readonly ITravelDal _ITravelDal;

        public TravelManager(ITravelDal iTravelDal)
        {
            _ITravelDal = iTravelDal;
        }

        public void TAdd(Travel t)
        {
            _ITravelDal.Insert(t);     
        }

        public void TDelete(Travel t)
        {
            _ITravelDal.Delete(t);
        }

        public List<Travel> TGetAll()
        {
           return _ITravelDal.GetAll();
        }

		public async Task<List<Travel>> TGetAllTravelByAdminid(int id)
		{
            return await _ITravelDal.GetAllTravelByAdminid(id);
		}

		public async Task<List<Travel>> TGetAllTravelByStaffid(int id)
		{
            return await _ITravelDal.GetAllTravelByStaffid(id);
		}

		public async Task<List<Travel>> TGetAllTravelWithStaffAndStatus(int id)
		{
            return await _ITravelDal.GetAllTravelWithStaffAndStatus(id);
		}

		public async Task<Travel> TGetById(int id)
        {
            return await _ITravelDal.GetById(id);
        }

        public async Task <List<Travel>> TGetTravelByAdmin(int id)
        {
            return await _ITravelDal.GetTravelByAdmin(id);
        }

		public Task<Travel> TGetTravelWithStaffAndStatus(int id)
		{
			return _ITravelDal.GetTravelWithStaffAndStatus(id);
		}

		public bool TIsAdminOfTravel(int travelId, int userId)
		{
			return _ITravelDal.IsAdminOfTravel(travelId, userId);    
		}

		public async Task TMakeActiveTravel(int id)
		{
			await _ITravelDal.MakeActiveTravel(id);
		}

		public async Task TMakePassiveTravel(int id)
		{
			await _ITravelDal.MakePassiveTravel(id);
		}

		public void TUpdate(Travel t)
        {
            _ITravelDal.Update(t);
        }

        public async Task<List<Travel>> TGetPassiveTravelsByStaffid(int id)
        {
            return await _ITravelDal.GetPassiveTravelsByStaffid(id);
        }        
        public async Task<List<Travel>> TGetAllActiveTravels()
        {
            return await _ITravelDal.GetAllActiveTravels();
        }


	}
}