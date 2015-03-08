using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonTemps.Models
{
	public class Reservation
	{
		[Key]
		public int Id { get; set; }

		public int PersonCount { get; set; }
		public DateTime ArrivalDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Comment { get; set; }

        public string Coupon { get; set; }

		public virtual Person Reserver { get; set; }
		public virtual ICollection<ReservationOrder> Orders { get; set; }

		public virtual ICollection<Seating> Seats { get; set; }
	}
}