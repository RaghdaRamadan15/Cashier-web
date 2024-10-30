namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        image = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Discount = c.Single(nullable: false),
                        TotalPrice = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(nullable: false),
                        TotalAmount = c.Single(nullable: false),
                        Discount = c.Single(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        email = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        LastLogin = c.DateTime(),
                        ActiveSession = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.InvoiceItems", new[] { "ProductId" });
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceItems");
            DropTable("dbo.Customers");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
