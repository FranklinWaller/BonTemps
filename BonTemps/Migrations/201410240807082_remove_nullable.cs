namespace BonTemps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_nullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Seatings", new[] { "Reservation_Id" });
            AlterColumn("dbo.Reservations", "ArrivalDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservations", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Seatings", "Reservation_Id", c => c.Int());
            AlterColumn("dbo.Seatings", "Reservation_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Seatings", "Reservation_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Seatings", new[] { "Reservation_Id" });
            AlterColumn("dbo.Seatings", "Reservation_ID", c => c.Int());
            AlterColumn("dbo.Seatings", "Reservation_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Reservations", "ArrivalDate", c => c.DateTime());
            CreateIndex("dbo.Seatings", "Reservation_Id");
        }
    }
}
