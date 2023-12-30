﻿using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess.Repository.IRepository;
using Shop.Utility;
using System.Security.Claims;

namespace shop_on_asp.ViewComponents
{
	public class ShoppingCartViewComponent : ViewComponent
	{
		private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvoKeResult()
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				if(HttpContext.Session.GetInt32(SD.SessionCart) == null)
				{
					HttpContext.Session.SetInt32(SD.SessionCart,
						_unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
				}

				return View(HttpContext.Session.GetInt32(SD.SessionCart));
			}
			else
			{
				HttpContext.Session.Clear();
				return View(0);
			}
		}
    }
}
