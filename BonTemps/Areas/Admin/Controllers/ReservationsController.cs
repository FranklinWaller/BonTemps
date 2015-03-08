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
	public class ReservationsController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
        private Services.SeatService seatService;
        private Services.CouponService couponService;

        public ReservationsController()
        {
            seatService = new Services.SeatService(db);
            couponService = new Services.CouponService(db);
        }

		// GET: Admin/Reservations
		public ActionResult Index()
		{
            return View(db.Reservations.Select(r =>
                new ViewModels.ReservationsViewModel()
                {
                    Id = r.Id,
                    ArrivalDate = r.ArrivalDate,
                    Comment = r.Comment,
                    EndDate = r.EndDate,
                    PersonCount = r.PersonCount,
                    Adres = r.Reserver.Adres,
                    City = r.Reserver.City,
                    Email = r.Reserver.Email,
                    FirstName = r.Reserver.FirstName,
                    LastName = r.Reserver.LastName,
                    PhoneNumber = r.Reserver.PhoneNumber,
                    ZipCode = r.Reserver.ZipCode
                }).OrderByDescending(m => m.ArrivalDate));
		}

		// GET: Admin/Reservations/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Reservation reservation = db.Reservations.Find(id);
			if (reservation == null)
			{
				return HttpNotFound();
			}
            return View(reservation);
		}

		// GET: Admin/Reservations/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/Reservations/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ViewModels.ReservationsViewModel reservationViewModel)
		{
			if (ModelState.IsValid)
			{
				Reservation reservation = new Reservation()
				{
                    Id = reservationViewModel.Id,
					ArrivalDate = reservationViewModel.ArrivalDate,
					Comment = reservationViewModel.Comment,
					EndDate = reservationViewModel.EndDate,
					PersonCount = reservationViewModel.PersonCount,
				};
				Person person = new Person()
				{
					Adres = reservationViewModel.Adres,
					City = reservationViewModel.City,
					Email = reservationViewModel.Email,
					FirstName = reservationViewModel.FirstName,
					LastName = reservationViewModel.LastName,
					PhoneNumber = reservationViewModel.PhoneNumber,
					ZipCode = reservationViewModel.ZipCode
				};

                // If the person previously ordered with the specified email
                // Update the older entry so we don't have unneeded rows.

                // Check if there any seats available in the time specified.
                if (seatService.HasFreeSeats(reservation.PersonCount, reservation.ArrivalDate, reservation.EndDate))
                {
                    // If a coupon is specified, make sure its valid.
                    if (!string.IsNullOrWhiteSpace(reservation.Coupon))
                    {
                        if (!couponService.IsValidCoupon(reservation.Coupon))
                        {
                            ModelState.AddModelError("", "De coupon is niet geldig.");
                            return View(reservationViewModel);
                        }
                    }

                    var previous = db.Persons.Where(p => p.Email == person.Email).FirstOrDefault();
                    if (previous != null)
                    {
                        person.Id = previous.Id;
                        db.Entry(person).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Persons.Add(person);
                    }
                    db.SaveChanges();

                    reservation.Reserver = person;
                    reservation.Seats = seatService.AssignSeats(reservation.PersonCount, reservation.ArrivalDate, reservation.EndDate).ToList();
                    db.Reservations.Add(reservation);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = reservation.Id });
                }
                else
                {
                    ModelState.AddModelError("", "Er zijn niet genoeg zitplaatsen.");
                }
			}
			return View(reservationViewModel);
		}

		public List<Seating> GetSeatings(List<Seating> seats, int seatCount, List<Seating> result = null)
		{
			if (result == null)
				result = new List<Seating>();

			foreach (Seating i in seats)
			{
				if(i.Seats <= seatCount) {
					result.Add(i);
					return result;
				}
				seatCount -= i.Seats;
				seats.Remove(i);
				return GetSeatings(seats, seatCount, result);
			}
			return result;
		}

		// GET: Admin/Reservations/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Reservation reservation = db.Reservations.Find(id);
			if (reservation == null)
			{
				return HttpNotFound();
			}
			return View(reservation);
		}

		// POST: Admin/Reservations/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,PersonCount,ArrivalDate,EndDate,Comment")] Reservation reservation)
		{
			if (ModelState.IsValid)
			{
				db.Entry(reservation).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(reservation);
		}

		// GET: Admin/Reservations/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Reservation reservation = db.Reservations.Find(id);
			if (reservation == null)
			{
				return HttpNotFound();
			}
			return View(reservation);
		}

		// POST: Admin/Reservations/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Reservation reservation = db.Reservations.Find(id);
			db.Reservations.Remove(reservation);
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
