using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BonTemps.Models;

namespace BonTemps.Areas.Api.Controllers
{
    public class ReservationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Services.SeatService seatService;
        private Services.CouponService couponService;

        public ReservationsController()
        {
            seatService = new Services.SeatService(db);
            couponService = new Services.CouponService(db);
        }

        // GET: api/Reservations
        public IQueryable<Reservation> GetReservations()
        {
            return db.Reservations;
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.Id)
            {
                return BadRequest();
            }

            db.Entry(reservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult PostReservation(Reservation reservation)
        {
            // If the model is invalid, throw an error.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            // If the person previously ordered with the specified email
            // Update the older entry so we don't have unneeded rows.
            var previous = db.Persons.Where(p => p.Email == reservation.Reserver.Email).FirstOrDefault();
            if (previous != null)
            {
                reservation.Reserver.Id = previous.Id;
            }
            // Check if there any seats available in the time specified.
            if (!seatService.HasFreeSeats(reservation.PersonCount, reservation.ArrivalDate, reservation.EndDate))
            {
                return BadRequest("Not enough seats");
            }
            // If a coupon is specified, make sure its valid.
            if (!string.IsNullOrWhiteSpace(reservation.Coupon))
            {
                if (!couponService.IsValidCoupon(reservation.Coupon))
                {
                    return BadRequest("Invalid Coupon");
                }
            }
            reservation.Seats = seatService.AssignSeats(reservation.PersonCount, reservation.ArrivalDate, reservation.EndDate).ToList();

            db.Reservations.Add(reservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            db.SaveChanges();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.Id == id) > 0;
        }
    }
}