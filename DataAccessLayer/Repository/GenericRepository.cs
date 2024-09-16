using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
	public class GenericRepository<T> : IGenericDal<T> where T : class
	{
        private readonly Context _context;
        public GenericRepository(Context context)
        {
            _context = context;
        }
        public void Delete(T t)
		{
            _context.Remove(t);
            _context.SaveChanges();
        }

		public List<T> GetAll()
		{
			return _context.Set<T>().DefaultIfEmpty().ToList();  //api tarafında boş liste dönmesi için defaultifempty kullanıldı.
		}

		public void Insert(T t)
		{
            _context.Add(t);
            _context.SaveChanges();
        }

		public void Update(T t)
		{
            _context.Update(t);
            _context.SaveChanges();

        }
		public async Task<T> GetById(int id)
		{
            return await _context.Set<T>().FindAsync(id);
        }

	}
}