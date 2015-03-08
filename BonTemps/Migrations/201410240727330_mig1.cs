namespace BonTemps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Seatings", new[] { "Reservation_Id" });
            AlterColumn("dbo.Seatings", "Reservation_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Seatings", "Reservation_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Seatings", new[] { "Reservation_Id" });
            AlterColumn("dbo.Seatings", "Reservation_ID", c => c.Int());
            CreateIndex("dbo.Seatings", "Reservation_Id");
        }
    }
}
