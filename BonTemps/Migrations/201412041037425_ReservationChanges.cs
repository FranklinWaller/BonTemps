namespace BonTemps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CouponReservations", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.CouponReservations", "CouponId", "dbo.Coupons");
            DropForeignKey("dbo.OrderReservations", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.OrderReservations", "OrderId", "dbo.Menus");
            DropIndex("dbo.CouponReservations", new[] { "ReservationId" });
            DropIndex("dbo.CouponReservations", new[] { "CouponId" });
            DropIndex("dbo.OrderReservations", new[] { "ReservationId" });
            DropIndex("dbo.OrderReservations", new[] { "OrderId" });
            CreateTable(
                "dbo.ReservationOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReservationId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => t.MenuId);
            
            AddColumn("dbo.Reservations", "Coupon", c => c.String());
            AddColumn("dbo.Reservations", "Menu_Id", c => c.Int());
            AddColumn("dbo.Reservations", "Coupon_Id", c => c.Int());
            CreateIndex("dbo.Reservations", "Menu_Id");
            CreateIndex("dbo.Reservations", "Coupon_Id");
            AddForeignKey("dbo.Reservations", "Menu_Id", "dbo.Menus", "Id");
            AddForeignKey("dbo.Reservations", "Coupon_Id", "dbo.Coupons", "Id");
            DropTable("dbo.CouponReservations");
            DropTable("dbo.OrderReservations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderReservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.OrderId });
            
            CreateTable(
                "dbo.CouponReservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false),
                        CouponId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.CouponId });
            
            DropForeignKey("dbo.Reservations", "Coupon_Id", "dbo.Coupons");
            DropForeignKey("dbo.Reservations", "Menu_Id", "dbo.Menus");
            DropForeignKey("dbo.ReservationOrders", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.ReservationOrders", "MenuId", "dbo.Menus");
            DropIndex("dbo.ReservationOrders", new[] { "MenuId" });
            DropIndex("dbo.ReservationOrders", new[] { "ReservationId" });
            DropIndex("dbo.Reservations", new[] { "Coupon_Id" });
            DropIndex("dbo.Reservations", new[] { "Menu_Id" });
            DropColumn("dbo.Reservations", "Coupon_Id");
            DropColumn("dbo.Reservations", "Menu_Id");
            DropColumn("dbo.Reservations", "Coupon");
            DropTable("dbo.ReservationOrders");
            CreateIndex("dbo.OrderReservations", "OrderId");
            CreateIndex("dbo.OrderReservations", "ReservationId");
            CreateIndex("dbo.CouponReservations", "CouponId");
            CreateIndex("dbo.CouponReservations", "ReservationId");
            AddForeignKey("dbo.OrderReservations", "OrderId", "dbo.Menus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderReservations", "ReservationId", "dbo.Reservations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CouponReservations", "CouponId", "dbo.Coupons", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CouponReservations", "ReservationId", "dbo.Reservations", "Id", cascadeDelete: true);
        }
    }
}
