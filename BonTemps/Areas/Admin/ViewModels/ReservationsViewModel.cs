using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonTemps.Areas.Admin.ViewModels
{
	public class ReservationsViewModel
	{
        [Key]
        public int Id { get; set; }
		public string Email { get; set; }
		[Display(Name = "Tel. Nummer")]
		public string PhoneNumber { get; set; }
		[Display(Name = "Voornaam")]
		public string FirstName { get; set; }
		[Display(Name = "Achternaam")]
		public string LastName { get; set; }
		[Display(Name = "Adres")]
		public string Adres { get; set; }
		[Display(Name = "Woonplaats")]
		public string City { get; set; }
		[Display(Name = "Postcode")]
		public string ZipCode { get; set; }

		[Display(Name = "Aantal Bezoekers")]
		public int PersonCount { get; set; }
		[Display(Name = "Aankom Tijd")]
		public DateTime ArrivalDate { get; set; }
		[Display(Name = "Vertrek Tijd")]
		public DateTime EndDate { get; set; }
		[Display(Name = "Reactie")]
		public string Comment { get; set; }

        [Display(Name = "Reserveerder")]
        public virtual string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
	}
}