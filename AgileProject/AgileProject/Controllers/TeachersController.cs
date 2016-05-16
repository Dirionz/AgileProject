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
using System.IO;

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
            var username = User.Identity.Name;
            if(userExists(username))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterTeacherModel
            {
                getCorridors = new SelectList(db.Corridors, "Id", "Name")
            };
            return View(model);
        }

        private bool userExists(string username)
        {
            return (db.Users.Where(u => u.UserName == username).ToList().Count > 0);
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
                    User = user,
                    imageURL ="../images/default.jpg"
                });
                db.SaveChanges();
                SetStatus();
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
                var teacherdb = db.Teacher.FirstOrDefault(t => t.Id == teacher.Id);
                teacherdb.FirstName = teacher.FirstName;
                teacherdb.LastName = teacher.LastName;
                teacherdb.Phone = teacher.Phone;
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
                var teacherdb = db.Teacher.FirstOrDefault(t => t.Id == teacher.Id);
                teacherdb.FirstName = teacher.FirstName;
                teacherdb.LastName = teacher.LastName;
                teacherdb.Phone = teacher.Phone;
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

        // GET: Teachers/Edit/5
        public ActionResult UploadImage()
        {

            Teacher teacher = db.Teacher.FirstOrDefault(t => t.User.UserName == User.Identity.Name);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }
        // POST: Teachers/ImageUpload
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string[] fileArray = file.FileName.Split('.');
                string fileName = User.Identity.Name+"."+fileArray[1];
                string pic = System.IO.Path.GetFileName(fileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                var teacher = db.Teacher.FirstOrDefault(t => t.User.Id == user.Id);

                teacher.imageURL = "../images/"+pic;
                db.SaveChanges();
                return RedirectToAction("Index");


            }



            // after successfully uploading redirect the user
            return RedirectToAction("Index", "Manage");
        }

        //After creating a teacher  -> sets it's status to available
        [HttpPost]
        public ActionResult SetStatus()
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var teacher = db.Teacher.FirstOrDefault(t => t.User.Id == user.Id);
            var status = db.Status.FirstOrDefault(s => s.Teacher.Id == teacher.Id);

                status = new Status()
                {
                    StatusId = 10,
                    Teacher = teacher,
                    Date = DateTime.Now
                };
                db.Status.Add(status);
            
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }



    }
}
