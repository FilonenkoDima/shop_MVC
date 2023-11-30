using Microsoft.AspNetCore.Mvc;

namespace shop_on_asp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
