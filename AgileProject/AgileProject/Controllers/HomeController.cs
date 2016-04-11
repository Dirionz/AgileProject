﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgileProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var user = User.Identity.Name;
            if (user != "")
            {
                ViewBag.Message = "User";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}