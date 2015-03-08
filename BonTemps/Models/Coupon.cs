using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonTemps.Models
{
	public class Coupon
	{
		[Key]
		public int Id { get; set; }
        [Display(Name="Naam")]
		public string Name { get; set; }
		public string Code { get; set; }

		public virtual ICollection<Menu> CouponMenus { get; set; }
        [Display(Name = "Korting")]
		public decimal Discount { get; set; }

		public virtual ICollection<Reservation> Reservations { get; set; }

	}
}