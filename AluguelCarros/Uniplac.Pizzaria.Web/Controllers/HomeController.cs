﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Uniplac.Pizzaria.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Início";
            return View();
        }
    }
}
