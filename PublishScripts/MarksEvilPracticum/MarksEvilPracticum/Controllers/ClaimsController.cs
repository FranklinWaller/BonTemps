using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarksEvilPracticum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarksEvilPracticum.Controllers
{
    public class ClaimsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Claims
        [Authorize]
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var currentUser = manager.FindById(currentUserId);

            return View(db.Claims.ToList().Where(c => c.Klant == currentUser));


           // db.Claims.Where(m => m.Klant = '');

            //return View(db.Claims.ToList());
        }

        // GET: Claims/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claims claims = db.Claims.Find(id);
            if (claims == null)
            {
                return HttpNotFound();
            }
            return View(claims);
        }

        // GET: Claims/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Kenteken,Titel,Omschrijving,Datum,Status")] Claims claims)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.Identity.GetUserId();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var currentUser = manager.FindById(currentUserId);

                claims.Klant = currentUser;

                db.Claims.Add(claims);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(claims);
        }

        // GET: Claims/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claims claims = db.Claims.Find(id);
            if (claims == null)
            {
                return HttpNotFound();
            }
            return View(claims);
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Kenteken,Titel,Omschrijving,Datum,Status")] Claims claims)
        {
            if (ModelState.IsValid)
            {
                db.Entry(claims).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(claims);
        }

        // GET: Claims/Delete/5
        /*
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claims claims = db.Claims.Find(id);
            if (claims == null)
            {
                return HttpNotFound();
            }
            return View(claims);
        }
         * /

        // POST: Claims/Delete/5
        /*
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Claims claims = db.Claims.Find(id);
            db.Claims.Remove(claims);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
