using BonTemps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonTemps.Services
{
    public class SeatService
    {
        private ApplicationDbContext db;

        public SeatService(ApplicationDbContext context)
        {
            db = context;
        }

        private IEnumerable<Seating> getFreeSeats(DateTime arrivalDate, DateTime endDate)
        {
            var takenSeats = db.Reservations
                .Where(r => (r.ArrivalDate >= arrivalDate && r.EndDate <= arrivalDate) || (r.EndDate >= endDate && r.ArrivalDate <= endDate))
                .SelectMany(r => r.Seats);
            return db.Seatings.Except(takenSeats);
        }

        public bool HasFreeSeats(int amount, DateTime arrivalDate, DateTime endDate)
        {
            var seats = getFreeSeats(arrivalDate, endDate);

            return seats.Sum(s => s.Seats) >= amount;
        }

        public IEnumerable<Seating> AssignSeats(int amount, DateTime arrivalDate, DateTime endDate)
        {
            var seats = getFreeSeats(arrivalDate, endDate).OrderBy(s => s.Seats);

            if (!HasFreeSeats(amount, arrivalDate, endDate))
                return new List<Seating>();

            return assignedSeatFetcher(amount, seats.ToList());
        }

        private IEnumerable<Seating> assignedSeatFetcher(int amount, List<Seating> seats)
        {
            var assignedSeats = new List<Seating>();

            if (amount <= 0)
                return assignedSeats;
           


            foreach (var seat in seats)
            {
                if (seat.Seats >= amount)
                {
                    amount -= seat.Seats;
                    assignedSeats.Add(seat);

                    if (amount <= 0)
                        break;
                }
            }

            return seats;
        }
    }
}