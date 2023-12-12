using Shop.DataAccess.Data;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repository
{
	internal class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
			_db = db;
        }

        public void Save()
		{
			_db.SaveChanges();
		}

		public void Upadate(Category obj)
		{
			_db.Categories.Update(obj);
		}
	}
}
