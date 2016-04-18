using AgileProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgileProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var teacherList = db.Teacher.ToList();
            if (User.Identity.Name != "")
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                var teacher = db.Teacher.FirstOrDefault(t => t.User.Id == user.Id);
                ViewBag.LoggedInUser = (user != null);
                ViewBag.teacher = teacher;
                teacherList.Remove(teacher);
            }
            return View(teacherList);
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