using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonTemps.Models
{
	public class Seating
	{
		[Key]
		public int Id { get; set; }
         [Display(Name = "Naam")]
        public string Name { get; set; }
         [Display(Name = "Zittingen")]
		public int Seats { get; set; }

        [JsonIgnore]
		public virtual ICollection<Reservation> Reservations { get; set; }
	}
}