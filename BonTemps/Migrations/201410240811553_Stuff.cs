namespace BonTemps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stuff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seatings", "Reservation_Id", "dbo.Reservations");
            DropIndex("dbo.Seatings", new[] { "Reservation_Id" });
            DropColumn("dbo.Seatings", "Occupied");
            DropColumn("dbo.Seatings", "Reservation_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seatings", "Reservation_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Seatings", "Occupied", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Seatings", "Reservation_Id");
            AddForeignKey("dbo.Seatings", "Reservation_Id", "dbo.Reservations", "Id");
        }
    }
}
