namespace ClothBazar.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDataAnotaion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 70));
        }
    }
}
