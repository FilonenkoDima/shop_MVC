using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;
using Shop.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace shop_on_asp.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperty: "Category");
			return View(productList);
		}

		public IActionResult Details(int productId)
		{
			ShoppingCart cart = new ShoppingCart()
			{
				Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperty: "Category"),
				Count = 1,
				ProductId = productId
			};

			return View(cart);
		}

		[HttpPost]
		[Authorize]
		public IActionResult Details(ShoppingCart shoppingCart)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			shoppingCart.ApplicationUserId = userId;

			ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
			u.ProductId == shoppingCart.ProductId);

			if (cartFromDb != null)
			{
				//shopping cart exists
				cartFromDb.Count += shoppingCart.Count;
				_unitOfWork.ShoppingCart.Upadate(cartFromDb);
				_unitOfWork.Save();
			}
			else
			{
				// add card record
				_unitOfWork.ShoppingCart.Add(shoppingCart);
				_unitOfWork.Save();
				HttpContext.Session.SetInt32(SD.SessionCart,
					_unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
			}

			TempData["Success"] = "Cart updated successfully";


			return RedirectToAction(nameof(Index));
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}