using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgileProject.Models;

namespace AgileProject.Controllers
{
    public class CorridorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Corridors
        public ActionResult Index()
        {
            return View(db.Corridors.ToList());
        }

        // GET: Corridors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corridor corridor = db.Corridors.Find(id);
            if (corridor == null)
            {
                return HttpNotFound();
            }
            return View(corridor);
        }

        // GET: Corridors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Corridors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Corridor corridor)
        {
            if (ModelState.IsValid)
            {
                db.Corridors.Add(corridor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(corridor);
        }

        // GET: Corridors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corridor corridor = db.Corridors.Find(id);
            if (corridor == null)
            {
                return HttpNotFound();
            }
            return View(corridor);
        }

        // POST: Corridors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Corridor corridor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corridor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(corridor);
        }

        // GET: Corridors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corridor corridor = db.Corridors.Find(id);
            if (corridor == null)
            {
                return HttpNotFound();
            }
            return View(corridor);
        }

        // POST: Corridors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Corridor corridor = db.Corridors.Find(id);
            db.Corridors.Remove(corridor);
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
