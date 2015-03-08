using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonTemps.Services
{
	public class ReservationService
	{
		public Models.Reservation AddReservation(ViewModels.ReservationRequestViewModel reservationRequest)
		{
			Models.Person person = new Models.Person()
				{
					Adres = reservationRequest.Adres,
					City = reservationRequest.City,
					Email = reservationRequest.Email,
					FirstName = reservationRequest.FirstName,
					LastName = reservationRequest.LastName,
					PhoneNumber = reservationRequest.PhoneNumber,
					ZipCode = reservationRequest.ZipCode
				};

			Models.Reservation reservation = new Models.Reservation()
			{
				PersonCount = reservationRequest.PersonCount,
				ArrivalDate = reservationRequest.ArrivalDate,
				Comment = reservationRequest.Comment,
				Reserver = person 
			};



			return reservation;
		}
	}
}