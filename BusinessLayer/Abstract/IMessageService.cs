using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IMessageService : IGenericService<Message>
	{
        public Task<List<Message>> TGetMessagesByTravelId(int travelId);
        public Task<Message> TGetLatestMessageByTravelId(int travelId);
    }
}
