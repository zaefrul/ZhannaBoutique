namespace BMSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Category", c => c.String());
            AddColumn("dbo.Products", "Cost", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Sell", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ModifyDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "ModifyBy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ModifyBy");
            DropColumn("dbo.Products", "ModifyDate");
            DropColumn("dbo.Products", "CreatedBy");
            DropColumn("dbo.Products", "CreatedDate");
            DropColumn("dbo.Products", "Status");
            DropColumn("dbo.Products", "Sell");
            DropColumn("dbo.Products", "Cost");
            DropColumn("dbo.Products", "Category");
        }
    }
}
