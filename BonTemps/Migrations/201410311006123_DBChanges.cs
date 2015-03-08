namespace BonTemps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seatings", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Seatings", "Name");
        }
    }
}
