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
            var statusList = db.Status.ToList();
            ViewBag.statusList = statusList;

            if (User.Identity.Name != "")
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                var teacher = db.Teacher.FirstOrDefault(t => t.User.Id == user.Id);
                ViewBag.LoggedInUser = (user != null);
                if(teacher == null)
                {
                    return RedirectToAction("Create", "Teachers");
                }
                ViewBag.teacher = teacher;
                teacherList.Remove(teacher);
                ViewBag.teacherList = teacherList;

                var model = new PostStatusModel
                {
                    getStatus = new SelectList(getStatuses(), "Value", "Text")
                };
                var status = db.Status.FirstOrDefault(s => s.Teacher.Id == teacher.Id);
                if(status != null)
                {
                    model.statusId = status.StatusId;
                    
                }
                return View(model);
            }

            
            ViewBag.teacherList = teacherList;

            return View();
        }


        private IEnumerable<SelectListItem> getStatuses()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Available", Value = "10"});
            list.Add(new SelectListItem { Text = "Busy", Value = "20" });
            list.Add(new SelectListItem { Text = "Busy (Students)", Value = "30" });
            list.Add(new SelectListItem { Text = "Away", Value = "40" });

            return list;
        }

        [HttpPost]
        public ActionResult ChangeStatus(PostStatusModel model)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var teacher = db.Teacher.FirstOrDefault(t => t.User.Id == user.Id);

            var status = db.Status.FirstOrDefault(s => s.Teacher.Id == teacher.Id);
            if(status == null)
            {
                status = new Status()
                {
                    StatusId = model.statusId,
                    Teacher = teacher,
                    Date = DateTime.Now
                };
                db.Status.Add(status);
            } else
            {
                status.StatusId = model.statusId;
                status.Date = DateTime.Now;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}