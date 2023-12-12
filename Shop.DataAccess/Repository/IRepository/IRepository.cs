
namespace Shop.DataAccess.Repository.IRepository
{
	internal interface IRepository<T> where T : class
	{
		//T - Category
		IEnumarabke<T> GetAll();
		T Get(Expression<Func<T, bool>> filter);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumarable<T> entities);
	}
}