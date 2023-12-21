using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;
using Shop.Utility;

namespace shop_on_asp.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class CompanyController : Controller
	{
		private IUnitOfWork _unitOfWork { get; set; }
		public CompanyController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			IEnumerable<Company> companies = _unitOfWork.Company.GetAll();
			return View(companies);
		}

		public IActionResult Upsert(int? id) // UpdateInsert
		{
			Company company = new Company();
			if (id == 0 || id == null)
			{
				return View(company);
			}
			else
			{
				company = _unitOfWork.Company.Get(u => u.Id == id);
				return View(company);
			}
		}

		[HttpPost]
		public IActionResult Upsert(Company company)
		{
			if (ModelState.IsValid)
			{
				if (company.Id == 0)
				{
					_unitOfWork.Company.Add(company);
					TempData["Success"] = "Company created succesffuly.";
				}
				else
				{
					_unitOfWork.Company.Update(company);
					TempData["Success"] = "Company created succesffuly.";
				}
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View(company);
		}

		#region API CALLS

		[HttpGet]
		public IActionResult GetAll()
		{
			IEnumerable<Company> companies = _unitOfWork.Company.GetAll();
			return Json(new { data = companies } );
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
			if (companyToBeDeleted == null)
				return Json(new { success = false, message = "Error while deleting" });

			_unitOfWork.Company.Remove(companyToBeDeleted);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Delete Successful" });
		}

		#endregion
	}
}
