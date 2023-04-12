namespace WpfApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Photo");
        }
    }
}
