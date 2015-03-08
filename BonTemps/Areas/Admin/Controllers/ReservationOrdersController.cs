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
    public class ReservationOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ReservationOrders
        public ActionResult Index()
        {
            var reservationOrders = db.ReservationOrders.Include(r => r.Menu).Include(r => r.Reservation);
            return View(reservationOrders.ToList());
        }

        // GET: Admin/ReservationOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationOrder reservationOrder = db.ReservationOrders.Find(id);
            if (reservationOrder == null)
            {
                return HttpNotFound();
            }
            return View(reservationOrder);
        }

        // GET: Admin/ReservationOrders/Create
        public ActionResult Create(int id)
        {
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "Name");
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "Comment");
            ViewBag.ID = id;
            return View();
        }

        // POST: Admin/ReservationOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReservationId,MenuId,Amount,Status")] ReservationOrder reservationOrder)
        {
            if (ModelState.IsValid)
            {
                db.ReservationOrders.Add(reservationOrder);
                db.SaveChanges();
                return RedirectToAction("Details", "Reservations", routeValues: new { id = reservationOrder.ReservationId });
            }

            ViewBag.MenuId = new SelectList(db.Menus, "Id", "Name", reservationOrder.MenuId);
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "Comment", reservationOrder.ReservationId);
            return View(reservationOrder);
        }

        // GET: Admin/ReservationOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationOrder reservationOrder = db.ReservationOrders.Find(id);
            if (reservationOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "Name", reservationOrder.MenuId);
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "Comment", reservationOrder.ReservationId);
            return View(reservationOrder);
        }

        // POST: Admin/ReservationOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReservationId,MenuId,Amount,Status")] ReservationOrder reservationOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservationOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(db.Menus, "Id", "Name", reservationOrder.MenuId);
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "Comment", reservationOrder.ReservationId);
            return View(reservationOrder);
        }

        // GET: Admin/ReservationOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationOrder reservationOrder = db.ReservationOrders.Find(id);
            if (reservationOrder == null)
            {
                return HttpNotFound();
            }
            return View(reservationOrder);
        }

        // POST: Admin/ReservationOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservationOrder reservationOrder = db.ReservationOrders.Find(id);
            db.ReservationOrders.Remove(reservationOrder);
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
