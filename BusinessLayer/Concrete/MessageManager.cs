using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class MessageManager : IMessageService
	{
		private readonly IMessageDal _IMessageDal;

		public MessageManager(IMessageDal messageDal)
		{
			_IMessageDal = messageDal;
		}

		public void TAdd(Message t)
		{
			_IMessageDal.Insert(t);
		}

		public void TDelete(Message t)
		{
			_IMessageDal.Delete(t);

		}

		public List<Message> TGetAll()
		{
			return _IMessageDal.GetAll();
		}

		public async Task<Message> TGetById(int id)
		{
			return await _IMessageDal.GetById(id);
		}

        public async Task<Message> TGetLatestMessageByTravelId(int travelId)
        {
            return await _IMessageDal.GetLatestMessageByTravelId(travelId);

        }

        public async Task<List<Message>> TGetMessagesByTravelId(int travelId)
        {
			return await _IMessageDal.GetMessagesByTravelId(travelId);
        }

        public void TUpdate(Message t)
		{
			_IMessageDal.Update(t);

		}
	}
}
