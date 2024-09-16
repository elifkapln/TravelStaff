using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
	public interface IMessageDal : IGenericDal<Message>
	{
		public Task<List<Message>> GetMessagesByTravelId(int travelId);
		public Task<Message> GetLatestMessageByTravelId(int travelId);
    }
}
