using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess.Data;
using Shop.Models;

namespace shop_on_asp.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<Category> objCategoryList = _db.Categories.ToList();
			return View(objCategoryList);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", $"The {nameof(obj.DisplayOrder)} can`t exactly match the {nameof(obj.Name)}");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Data created succesfully";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Edit(int? id)
		{
			if (id == 0 || id == null)
			{
				return NotFound();
			}

			Category? categoryFromDB = _db.Categories.FirstOrDefault(a => a.Id == id);

			if (categoryFromDB == null)
			{
				return NotFound();
			}

			return View(categoryFromDB);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Data edited succesfully";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == 0 || id == null)
			{
				return NotFound();
			}

			Category? categoryFromDB = _db.Categories.FirstOrDefault(a => a.Id == id);

			if (categoryFromDB == null)
			{
				return NotFound();
			}

			return View(categoryFromDB);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Category obj = _db.Categories.FirstOrDefault(c => c.Id == id);

			if (obj == null)
				return NotFound();

			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Data deleted succesfully";
			return RedirectToAction("Index");
		}
	}
}
