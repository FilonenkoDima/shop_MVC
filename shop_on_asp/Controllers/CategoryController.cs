﻿using Microsoft.AspNetCore.Mvc;
using shop_on_asp.Data;
using shop_on_asp.Models;

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
    }
}
