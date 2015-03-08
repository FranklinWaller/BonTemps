namespace BonTemps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryMenu", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.CategoryMenu", "CategoryId", "dbo.FoodCategories");
            DropForeignKey("dbo.CouponMenu", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.CouponMenu", "CouponId", "dbo.Coupons");
            DropForeignKey("dbo.Reservations", "Menu_Id", "dbo.Menus");
            DropIndex("dbo.Reservations", new[] { "Menu_Id" });
            DropIndex("dbo.CategoryMenu", new[] { "MenuId" });
            DropIndex("dbo.CategoryMenu", new[] { "CategoryId" });
            DropIndex("dbo.CouponMenu", new[] { "MenuId" });
            DropIndex("dbo.CouponMenu", new[] { "CouponId" });
            AddColumn("dbo.Menus", "Coupon_Id", c => c.Int());
            CreateIndex("dbo.Menus", "Coupon_Id");
            AddForeignKey("dbo.Menus", "Coupon_Id", "dbo.Coupons", "Id");
            DropColumn("dbo.Reservations", "Menu_Id");
            DropTable("dbo.FoodCategories");
            DropTable("dbo.CategoryMenu");
            DropTable("dbo.CouponMenu");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CouponMenu",
                c => new
                    {
                        MenuId = c.Int(nullable: false),
                        CouponId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuId, t.CouponId });
            
            CreateTable(
                "dbo.CategoryMenu",
                c => new
                    {
                        MenuId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuId, t.CategoryId });
            
            CreateTable(
                "dbo.FoodCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Reservations", "Menu_Id", c => c.Int());
            DropForeignKey("dbo.Menus", "Coupon_Id", "dbo.Coupons");
            DropIndex("dbo.Menus", new[] { "Coupon_Id" });
            DropColumn("dbo.Menus", "Coupon_Id");
            CreateIndex("dbo.CouponMenu", "CouponId");
            CreateIndex("dbo.CouponMenu", "MenuId");
            CreateIndex("dbo.CategoryMenu", "CategoryId");
            CreateIndex("dbo.CategoryMenu", "MenuId");
            CreateIndex("dbo.Reservations", "Menu_Id");
            AddForeignKey("dbo.Reservations", "Menu_Id", "dbo.Menus", "Id");
            AddForeignKey("dbo.CouponMenu", "CouponId", "dbo.Coupons", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CouponMenu", "MenuId", "dbo.Menus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryMenu", "CategoryId", "dbo.FoodCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryMenu", "MenuId", "dbo.Menus", "Id", cascadeDelete: true);
        }
    }
}
