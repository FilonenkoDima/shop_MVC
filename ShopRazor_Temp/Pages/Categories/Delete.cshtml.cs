using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShopRazor_Temp.Data;
using ShopRazor_Temp.Models;

namespace ShopRazor_Temp.Pages.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
	{
		private readonly ApplicationDbContext _db;
		public Category Category { get; set; }

		public DeleteModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet(int? id)
		{
			if (id != null || id == 0)
			{
				Category = _db.Categories.Find(id);
			}

		}

		public IActionResult OnPost(int? id)
		{

			if (Category == null)
				return NotFound();

			_db.Categories.Remove(Category);
			_db.SaveChanges();
			TempData["success"] = "Data deleted succesfully";
			return RedirectToPage("Index");
		}
	}
}
