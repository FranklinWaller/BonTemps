using BonTemps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonTemps.Services
{
    public class CouponService
    {
        private ApplicationDbContext db;

        public CouponService(ApplicationDbContext context)
        {
            db = context;
        }

        public bool IsValidCoupon(string coupon)
        {
            if (string.IsNullOrWhiteSpace(coupon))
                return false;
            return db.Coupons.Any(c => c.Code == coupon);
        }
    }
}