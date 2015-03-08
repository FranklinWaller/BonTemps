using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using BonTemps.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Web.Http.Description;

namespace BonTemps.Areas.Api.Controllers
{
    public class OrdersController : ApiController
    {
        public Models.ApplicationDbContext db = new Models.ApplicationDbContext();

        public List<Models.ReservationOrder> GetOrders(Models.ReservationOrderStatusEnum status = Models.ReservationOrderStatusEnum.Ordered)
        {
            IEnumerable<Models.ReservationOrder> orders = db.ReservationOrders.Where(ro => ro.Status == status);
            return orders.ToList();
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Models.ReservationOrderStatusEnum status = Models.ReservationOrderStatusEnum.Completed) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReservationOrder order = db.ReservationOrders.FirstOrDefault(o => o.Id == id);
            order.Status = status;
            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationOrderExists(id))
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
        private bool ReservationOrderExists(int id)
        {
            return db.ReservationOrders.Count(e => e.Id == id) > 0;
        }
    }

}
