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
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        private readonly Context c;

        public EfMessageDal(Context c) : base(c)
        {
            this.c = c;
        }

        public async Task<Message> GetLatestMessageByTravelId(int travelId)
        {
            return await c.Messages
                     .Where(m => m.TravelID == travelId)
                     .OrderByDescending(m => m.SendDate)
                     .FirstOrDefaultAsync();
        }

        public async Task<List<Message>> GetMessagesByTravelId(int travelId)
        {
            return await c.Messages
                .Where(m => m.TravelID == travelId)
                .AsNoTracking() // Değişiklik izlemeyi kapatıyoruz
                .ToListAsync();
        }
    }
}