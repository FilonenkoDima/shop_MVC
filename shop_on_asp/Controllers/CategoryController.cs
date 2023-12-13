using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess.Data;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;

namespace shop_on_asp.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoryController(ICategoryRepository db)
		{
			_categoryRepository = db;
		}
		public IActionResult Index()
		{
			List<Category> objCategoryList = _categoryRepository.GetAll().ToList();
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
				_categoryRepository.Add(obj);
				_categoryRepository.Save();
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

			Category? categoryFromDB = _categoryRepository.Get(a => a.Id == id);

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
				_categoryRepository.Upadate(obj);
				_categoryRepository.Save();
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

			Category? categoryFromDB = _categoryRepository.Get(a => a.Id == id);

			if (categoryFromDB == null)
			{
				return NotFound();
			}

			return View(categoryFromDB);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Category obj = _categoryRepository.Get(a => a.Id == id);

			if (obj == null)
				return NotFound();

			_categoryRepository.Remove(obj);
			_categoryRepository.Save();
			TempData["success"] = "Data deleted succesfully";
			return RedirectToAction("Index");
		}
	}
}
