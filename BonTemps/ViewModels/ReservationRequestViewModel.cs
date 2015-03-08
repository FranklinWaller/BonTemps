using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonTemps.ViewModels
{
	public class ReservationRequestViewModel
	{
		public int PersonCount { get; set; }
		public DateTime ArrivalDate { get; set; }
		public string Comment { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Adres { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }

		public string[] Coupons { get; set; }
		public int[] Menus { get; set; }
	}
}