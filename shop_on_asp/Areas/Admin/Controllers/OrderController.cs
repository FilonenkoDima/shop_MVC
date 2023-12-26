using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;

namespace shop_on_asp.Areas.Admin.Controllers
{
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
		{
			return View();
		}

		#region API CALLS

		[HttpGet]
		public IActionResult GetAll()
		{
			List<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperty: "ApplicationUser").ToList();
			return Json(new { data = objOrderHeaders });
		}

		#endregion
	}
}
