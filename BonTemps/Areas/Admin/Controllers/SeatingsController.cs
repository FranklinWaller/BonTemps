using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BonTemps.Models;

namespace BonTemps.Areas.Admin.Controllers
{
    public class SeatingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Seatings
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Seatings.ToList());
        }

        // GET: Admin/Seatings/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seating seating = db.Seatings.Find(id);
            if (seating == null)
            {
                return HttpNotFound();
            }
            return View(seating);
        }

        // GET: Admin/Seatings/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Seatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Seats")] Seating seating)
        {
            if (ModelState.IsValid)
            {
                db.Seatings.Add(seating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seating);
        }

        // GET: Admin/Seatings/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seating seating = db.Seatings.Find(id);
            if (seating == null)
            {
                return HttpNotFound();
            }
            return View(seating);
        }

        // POST: Admin/Seatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,Seats")] Seating seating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seating);
        }

        // GET: Admin/Seatings/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seating seating = db.Seatings.Find(id);
            if (seating == null)
            {
                return HttpNotFound();
            }
            return View(seating);
        }

        // POST: Admin/Seatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Seating seating = db.Seatings.Find(id);
            db.Seatings.Remove(seating);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
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
