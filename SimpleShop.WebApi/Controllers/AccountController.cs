﻿using Microsoft.AspNetCore.Mvc;

namespace SimpleShop.WebApi.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
