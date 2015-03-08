using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonTemps.Models
{
	public class Menu
	{
		[Key]
		public int Id { get; set; }

        [Display(Name="Naam")]
		public string Name { get; set; }
        [Display(Description = "Beschrijving")]
		public string Description { get; set; }

        [DisplayName("Prijs")]
		public decimal Price { get; set; }

        [DisplayName("Afbeelding")]
		public string Image { get; set; }

        [DisplayName("Menu type")]
		public MenuTypeEnum MenuType { get; set; }

        //[DisplayName("Categorie")]
        //public virtual ICollection<FoodCategory> Categories { get; set; }

        //public virtual ICollection<Reservation> Reservations { get; set; }

        //public virtual ICollection<Coupon> Coupons { get; set; }
	}
}