namespace BonTemps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryMenu",
                c => new
                    {
                        MenuId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuId, t.CategoryId })
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.FoodCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.MenuId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.CouponMenu",
                c => new
                    {
                        MenuId = c.Int(nullable: false),
                        CouponId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuId, t.CouponId })
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.Coupons", t => t.CouponId, cascadeDelete: true)
                .Index(t => t.MenuId)
                .Index(t => t.CouponId);
            
            CreateTable(
                "dbo.CouponReservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        CouponId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.CouponId })
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .ForeignKey("dbo.Coupons", t => t.CouponId, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => t.CouponId);
            
            CreateTable(
                "dbo.OrderReservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.OrderId })
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .ForeignKey("dbo.Menus", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.SeatReservation",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        SeatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.SeatId })
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .ForeignKey("dbo.Seatings", t => t.SeatId, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => t.SeatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeatReservation", "SeatId", "dbo.Seatings");
            DropForeignKey("dbo.SeatReservation", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.OrderReservations", "OrderId", "dbo.Menus");
            DropForeignKey("dbo.OrderReservations", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.CouponReservations", "CouponId", "dbo.Coupons");
            DropForeignKey("dbo.CouponReservations", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.CouponMenu", "CouponId", "dbo.Coupons");
            DropForeignKey("dbo.CouponMenu", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.CategoryMenu", "CategoryId", "dbo.FoodCategories");
            DropForeignKey("dbo.CategoryMenu", "MenuId", "dbo.Menus");
            DropIndex("dbo.SeatReservation", new[] { "SeatId" });
            DropIndex("dbo.SeatReservation", new[] { "ReservationId" });
            DropIndex("dbo.OrderReservations", new[] { "OrderId" });
            DropIndex("dbo.OrderReservations", new[] { "ReservationId" });
            DropIndex("dbo.CouponReservations", new[] { "CouponId" });
            DropIndex("dbo.CouponReservations", new[] { "ReservationId" });
            DropIndex("dbo.CouponMenu", new[] { "CouponId" });
            DropIndex("dbo.CouponMenu", new[] { "MenuId" });
            DropIndex("dbo.CategoryMenu", new[] { "CategoryId" });
            DropIndex("dbo.CategoryMenu", new[] { "MenuId" });
            DropTable("dbo.SeatReservation");
            DropTable("dbo.OrderReservations");
            DropTable("dbo.CouponReservations");
            DropTable("dbo.CouponMenu");
            DropTable("dbo.CategoryMenu");
        }
    }
}
