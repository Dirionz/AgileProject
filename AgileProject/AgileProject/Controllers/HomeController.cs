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


        public ActionResult Index(string id)
        {
            var statusList = db.Status.Include("Teacher").OrderBy(s => s.Teacher.LastName).ThenBy(st => st.Teacher.FirstName).ToList();
            if (id != null)
            {
                statusList = statusList.Where(status => 
                                              getTeacherWithCorridor(status.Teacher).Corridor.Name == id 
                                              ).ToList();
            }
            ViewBag.statusList = statusList;

            if (User.Identity.Name != "")
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                var teacher = db.Teacher.FirstOrDefault(t => t.User.Id == user.Id);
                ViewBag.LoggedInUser = (user != null);
                if (teacher == null)
                {
                    return RedirectToAction("Create", "Teachers");
                }
                ViewBag.teacher = teacher;

                var model = new PostStatusModel
                {
                    getStatus = new SelectList(getStatuses(), "Value", "Text")
                };
                var status = db.Status.FirstOrDefault(s => s.Teacher.Id == teacher.Id);
                if (status != null)
                {
                    model.statusId = status.StatusId;
                    model.date = status.Date.ToString();

                }
                return View(model);
            }

            return View();
        }

        private Teacher getTeacherWithCorridor(Teacher teacher)
        {
            return db.Teacher.Include("Corridor").FirstOrDefault(t => t.Id == teacher.Id);
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
                    
                };
                db.Status.Add(status);
            } else
            {
                status.StatusId = model.statusId;
                
            }
            status.Date = null;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeTime(PostStatusModel model)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var teacher = db.Teacher.FirstOrDefault(t => t.User.Id == user.Id);

            var status = db.Status.FirstOrDefault(s => s.Teacher.Id == teacher.Id);
            DateTime dt = DateTime.ParseExact(model.date, "dd/MM/yyyy HH:mm",System.Globalization.CultureInfo.InvariantCulture);

            status.Date = dt;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}