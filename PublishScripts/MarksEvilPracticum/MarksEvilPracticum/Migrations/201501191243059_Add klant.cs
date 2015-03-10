namespace MarksEvilPracticum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addklant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kenteken = c.String(nullable: false),
                        Titel = c.String(nullable: false),
                        Omschrijving = c.String(nullable: false),
                        Datum = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Klant_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Klant_Id)
                .Index(t => t.Klant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Claims", "Klant_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Claims", new[] { "Klant_Id" });
            DropTable("dbo.Claims");
        }
    }
}
