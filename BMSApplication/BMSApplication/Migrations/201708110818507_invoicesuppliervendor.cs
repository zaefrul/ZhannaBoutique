namespace BMSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicesuppliervendor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        VarietyId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Varieties", t => t.VarietyId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.VarietyId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Date = c.DateTime(nullable: false),
                        GSTCode = c.String(),
                        GSTAmount = c.String(),
                        SupplierId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.VendorId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Name = c.String(),
                        Ic = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItems", "VarietyId", "dbo.Varieties");
            DropForeignKey("dbo.Invoices", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Invoices", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Invoices", new[] { "VendorId" });
            DropIndex("dbo.Invoices", new[] { "SupplierId" });
            DropIndex("dbo.InvoiceItems", new[] { "VarietyId" });
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropTable("dbo.Vendors");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceItems");
        }
    }
}
