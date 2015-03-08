using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonTemps.Models
{
    public enum ReservationOrderStatusEnum
    {
        Ordered = 0,
        InProgress = 1,
        Completed = 2,
        Delivered = 3,
        Cancelled = 4
    }
}