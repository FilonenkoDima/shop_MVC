using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		//T - Category
		IEnumerable<T> GetAll(string? includeProperty = null);
		T Get(Expression<Func<T, bool>> filter, string? includeProperty = null, bool tracked = false);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
