using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using BonTemps.Models;

namespace BonTemps.Areas.Api.Controllers
{
    [RoutePrefix("api/coupon")]
    public class CouponController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Coupon
        public IQueryable<Coupon> GetCoupons()
        {
            return db.Coupons;
        }

        // GET: api/Coupon/5
        [ResponseType(typeof(Coupon))]
        [Route("{code}")]
        public IHttpActionResult GetCoupon(string code)
        {
            Coupon coupon = db.Coupons.Where(b => b.Code == code).FirstOrDefault();

            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        // PUT: api/Coupon/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoupon(int id, Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coupon.Id)
            {
                return BadRequest();
            }

            db.Entry(coupon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponExists(id))
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

        // POST: api/Coupon
        [ResponseType(typeof(Coupon))]
        public IHttpActionResult PostCoupon(Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Coupons.Add(coupon);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = coupon.Id }, coupon);
        }

        // DELETE: api/Coupon/5
        [ResponseType(typeof(Coupon))]
        [HttpDelete]
        public IHttpActionResult DeleteCoupon(string code)
        {
            Coupon coupon = db.Coupons.Where(b => b.Code == code).FirstOrDefault();
            if (coupon == null)
            {
                return NotFound();
            }

            db.Coupons.Remove(coupon);
            db.SaveChanges();

            return Ok(coupon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CouponExists(int id)
        {
            return db.Coupons.Count(e => e.Id == id) > 0;
        }
    }
}