namespace BonTemps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lijst : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seatings", "Reservation_Id", c => c.Int());
            CreateIndex("dbo.Seatings", "Reservation_Id");
            AddForeignKey("dbo.Seatings", "Reservation_Id", "dbo.Reservations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seatings", "Reservation_Id", "dbo.Reservations");
            DropIndex("dbo.Seatings", new[] { "Reservation_Id" });
            DropColumn("dbo.Seatings", "Reservation_Id");
        }
    }
}
