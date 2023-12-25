using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repository.IRepository
{
	public interface IOrderHeaderRepository : IRepository<OrderHeader>
	{
		void Upadate(OrderHeader obj);
		void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

		void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);
	}
}
