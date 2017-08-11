namespace BMSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Selected = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifyBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        ModifyBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifyBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.String(),
                        Cost = c.Double(nullable: false),
                        Sell = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        ModifyBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Total = c.Double(nullable: false),
                        Method = c.String(),
                        Tendered = c.Double(nullable: false),
                        Change = c.Double(nullable: false),
                        CreditCardInvoice = c.String(),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                        ModifyBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        VarietyId = c.Int(nullable: false),
                        ReceiptId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Sell = c.Double(nullable: false),
                        ChannelId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Channels", t => t.ChannelId, cascadeDelete: true)
                .ForeignKey("dbo.Receipts", t => t.ReceiptId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Varieties", t => t.VarietyId, cascadeDelete: true)
                .Index(t => t.VarietyId)
                .Index(t => t.ReceiptId)
                .Index(t => t.UserId)
                .Index(t => t.ChannelId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        Email = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Varieties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                        ColorId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SizeId)
                .Index(t => t.ColorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "VarietyId", "dbo.Varieties");
            DropForeignKey("dbo.Varieties", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.Varieties", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Varieties", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.Transactions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "ReceiptId", "dbo.Receipts");
            DropForeignKey("dbo.Transactions", "ChannelId", "dbo.Channels");
            DropForeignKey("dbo.Receipts", "MemberId", "dbo.Members");
            DropIndex("dbo.Varieties", new[] { "ColorId" });
            DropIndex("dbo.Varieties", new[] { "SizeId" });
            DropIndex("dbo.Varieties", new[] { "ProductId" });
            DropIndex("dbo.Transactions", new[] { "ChannelId" });
            DropIndex("dbo.Transactions", new[] { "UserId" });
            DropIndex("dbo.Transactions", new[] { "ReceiptId" });
            DropIndex("dbo.Transactions", new[] { "VarietyId" });
            DropIndex("dbo.Receipts", new[] { "MemberId" });
            DropTable("dbo.Varieties");
            DropTable("dbo.Users");
            DropTable("dbo.Transactions");
            DropTable("dbo.Sizes");
            DropTable("dbo.Receipts");
            DropTable("dbo.Products");
            DropTable("dbo.Members");
            DropTable("dbo.Colors");
            DropTable("dbo.Channels");
        }
    }
}
