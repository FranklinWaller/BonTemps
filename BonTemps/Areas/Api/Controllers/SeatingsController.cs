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
    public class SeatingsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Seatings
        public IQueryable<Seating> GetSeatings()
        {
            return db.Seatings;
        }

        // GET: api/Seatings/5
        [ResponseType(typeof(Seating))]
        public IHttpActionResult GetSeating(int id)
        {
            Seating seating = db.Seatings.Find(id);
            if (seating == null)
            {
                return NotFound();
            }

            return Ok(seating);
        }

        // PUT: api/Seatings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeating(int id, Seating seating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seating.Id)
            {
                return BadRequest();
            }

            db.Entry(seating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatingExists(id))
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

        // POST: api/Seatings
        [ResponseType(typeof(Seating))]
        public IHttpActionResult PostSeating(Seating seating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Seatings.Add(seating);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = seating.Id }, seating);
        }

        // DELETE: api/Seatings/5
        [ResponseType(typeof(Seating))]
        public IHttpActionResult DeleteSeating(int id)
        {
            Seating seating = db.Seatings.Find(id);
            if (seating == null)
            {
                return NotFound();
            }

            db.Seatings.Remove(seating);
            db.SaveChanges();

            return Ok(seating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeatingExists(int id)
        {
            return db.Seatings.Count(e => e.Id == id) > 0;
        }
    }
}