namespace BonTemps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBChanges1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "ArrivalDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservations", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Reservations", "ArrivalDate", c => c.DateTime());
        }
    }
}
