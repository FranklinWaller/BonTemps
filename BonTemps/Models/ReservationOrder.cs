using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonTemps.Models
{
    public class ReservationOrder
    {
        public int Id { get; set; }
        

        public int ReservationId { get; set; }
        public int MenuId { get; set; }
        public int Amount { get; set; }
        public ReservationOrderStatusEnum Status { get; set; }

        [ForeignKey("ReservationId")]
        public virtual Reservation Reservation { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
    }
}