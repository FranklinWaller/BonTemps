using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BonTemps.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public DbSet<Menu> Menus { get; set; }
		public DbSet<Coupon> Coupons { get; set; }
		public DbSet<Seating> Seatings { get; set; }
		//public DbSet<FoodCategory> FoodCategories { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<Person> Persons { get; set; }
        public DbSet<ReservationOrder> ReservationOrders { get; set; }

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Reservation>()
				.HasMany(r => r.Seats)
				.WithMany(r => r.Reservations)
				.Map(m =>
				{
					m.MapLeftKey("ReservationId");
					m.MapRightKey("SeatId");
					m.ToTable("SeatReservation");
				});

            //modelBuilder.Entity<Menu>()
            //    .HasMany(r => r.Categories)
            //    .WithMany(r => r.Menus)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("MenuId");
            //        m.MapRightKey("CategoryId");
            //        m.ToTable("CategoryMenu");
            //    });

            //modelBuilder.Entity<Menu>()
            //    .HasMany(r => r.Coupons)
            //    .WithMany(r => r.CouponMenus)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("MenuId");
            //        m.MapRightKey("CouponId");
            //        m.ToTable("CouponMenu");
            //    });


			base.OnModelCreating(modelBuilder);
		}
	}
}