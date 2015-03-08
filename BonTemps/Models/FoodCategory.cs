using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonTemps.Models
{
	public class FoodCategory
	{
		[Key]
		public int Id { get; set; }
        [Display(Name="Naam")]
		public string Name { get; set; }

		public virtual ICollection<Menu> Menus { get; set; }
	}
}