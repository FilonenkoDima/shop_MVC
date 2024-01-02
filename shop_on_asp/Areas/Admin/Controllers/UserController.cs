﻿using Microsoft.AspNetCore.Authorization;
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
			return Json(new { data = users });
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			return Json(new { success = true, message = "Delete Successful" });
		}

		#endregion
	}
}
