using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.DataAccess.Data;
using Shop.DataAccess.Repository.IRepository;
using Shop.Models;
using Shop.Utility;

namespace shop_on_asp.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class UserController : Controller
	{
		private ApplicationDbContext _db { get; set; }
		public UserController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			return View();
		}

		#region API CALLS

		[HttpGet]
		public IActionResult GetAll()
		{
			IEnumerable<ApplicationUser> users = _db.ApplicationUsers.Include(u => u.Company);

			var userRoles = _db.UserRoles.ToList();
			var roles = _db.Roles.ToList();

			foreach (var user in users)
			{
				var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
				user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

				if (user == null)
					user.Company = new() { Name = "" };
			}

			return Json(new { data = users });
		}

		[HttpPost]
		public IActionResult LockUnlock([FromBody]string id)
		{
			var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
			if (objFromDb == null)
				return Json(new { success = false, message = "Error while Locking/Unlocking" });

			if(objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
			{
				//user is currently locked and we need to unlock them
				objFromDb.LockoutEnd = DateTime.Now;
			} 
			else
			{
				objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
			}
			_db.SaveChanges();
			return Json(new { success = true, message = "Operation Successful" });
		}

		#endregion
	}
}
