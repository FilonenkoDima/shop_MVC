using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace shop_on_asp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
