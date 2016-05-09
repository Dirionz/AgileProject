using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgileProject.Models;
using AgileProject.Helpers;

namespace AgileProject.Controllers
{
    public class TeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teachers
        public ActionResult Index()
        {
            if (!IsAdminHelper.isAdminBackend(User.Identity.Name))
            {
                return RedirectToAction("Index", "Home");
            }
            // TODO: Only view our own "teacher"
            return View(db.Teacher.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {  
            var model = new RegisterTeacherModel
            {
                getCorridors = new SelectList(db.Corridors, "Id", "Name")
            };
            return View(model);
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //[Bind(Include = "Id,FirstName,LastName,Phone,isAdmin,Corridor")] Teacher teacher
        public ActionResult Create(RegisterTeacherModel teachermodel)
        {
            if (ModelState.IsValid && db.Teacher.Where(t => t.User.UserName == User.Identity.Name).ToList().Count == 0)
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                var corridor = db.Corridors.FirstOrDefault(c => c.Id == teachermodel.corridorId);

                db.Teacher.Add(new Teacher
                {
                    FirstName = teachermodel.FirstName,
                    LastName = teachermodel.LastName,
                    Phone = teachermodel.Phone,
                    isAdmin = false,
                    Corridor = corridor,
                    User = user
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(teachermodel);
        }

        // GET: Teachers/Edit/5
        public ActionResult Editall(int? id)
        {
            if(!IsAdminHelper.isAdminBackend(User.Identity.Name)) {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }


        // GET: Teachers/Edit
        public ActionResult Edit()
        {

            Teacher teacher = db.Teacher.FirstOrDefault(t => t.User.UserName == User.Identity.Name);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Phone")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Manage");
            }
            return View(teacher);
        }

        // POST: Teachers/Editall/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editall([Bind(Include = "Id,FirstName,LastName,Phone,isAdmin")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!IsAdminHelper.isAdminBackend(User.Identity.Name))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            db.Teacher.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
