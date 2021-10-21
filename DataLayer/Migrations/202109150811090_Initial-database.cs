namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductComments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Website = c.String(),
                        Comment = c.String(nullable: false, maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.ProductComments", t => t.ParentID)
                .Index(t => t.ProductID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductTitle = c.String(nullable: false, maxLength: 250),
                        ShortDescrioption = c.String(nullable: false, maxLength: 350),
                        Text = c.String(nullable: false, maxLength: 500),
                        Price = c.Int(nullable: false),
                        ImageName = c.String(maxLength: 150),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductFeatures",
                c => new
                    {
                        PF_ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        FeatureID = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.PF_ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductGalleries",
                c => new
                    {
                        GalleryID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ImageName = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.GalleryID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductSelectGroups",
                c => new
                    {
                        PG_ID = c.Int(nullable: false, identity: true),
                        ProdctID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                        Products_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.PG_ID)
                .ForeignKey("dbo.ProductGroups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Products_ProductID)
                .Index(t => t.GroupID)
                .Index(t => t.Products_ProductID);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupTitle = c.String(nullable: false),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.ProductGroups", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        Tag = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.TagID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false),
                        RoleTitle = c.String(maxLength: 250),
                        RoleName = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 150),
                        Password = c.String(nullable: false, maxLength: 150),
                        AciveCode = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.ProductComments", "ParentID", "dbo.ProductComments");
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductSelectGroups", "Products_ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductGroups", "ParentID", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductSelectGroups", "GroupID", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGalleries", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductFeatures", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductComments", "ProductID", "dbo.Products");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.ProductTags", new[] { "ProductID" });
            DropIndex("dbo.ProductGroups", new[] { "ParentID" });
            DropIndex("dbo.ProductSelectGroups", new[] { "Products_ProductID" });
            DropIndex("dbo.ProductSelectGroups", new[] { "GroupID" });
            DropIndex("dbo.ProductGalleries", new[] { "ProductID" });
            DropIndex("dbo.ProductFeatures", new[] { "ProductID" });
            DropIndex("dbo.ProductComments", new[] { "ParentID" });
            DropIndex("dbo.ProductComments", new[] { "ProductID" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.ProductSelectGroups");
            DropTable("dbo.ProductGalleries");
            DropTable("dbo.ProductFeatures");
            DropTable("dbo.Products");
            DropTable("dbo.ProductComments");
        }
    }
}
