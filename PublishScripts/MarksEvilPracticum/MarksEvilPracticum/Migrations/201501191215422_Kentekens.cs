namespace MarksEvilPracticum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kentekens : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Kenteken", c => c.String());
            AddColumn("dbo.AspNetUsers", "Voornaam", c => c.String());
            AddColumn("dbo.AspNetUsers", "Tussenvoegsel", c => c.String());
            AddColumn("dbo.AspNetUsers", "Achternaam", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Achternaam");
            DropColumn("dbo.AspNetUsers", "Tussenvoegsel");
            DropColumn("dbo.AspNetUsers", "Voornaam");
            DropColumn("dbo.AspNetUsers", "Kenteken");
        }
    }
}
