namespace WpfApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddDirectionV3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Direction_Id", "dbo.Directions");
            DropIndex("dbo.Events", new[] { "Direction_Id" });
            DropColumn("dbo.Events", "Direction_Id");

            AddColumn("dbo.Events", "DirectionId", c => c.Int());
            CreateIndex("dbo.Events", "DirectionId");
            AddForeignKey("dbo.Events", "DirectionId", "dbo.Directions", "Id");
        }

        public override void Down()
        {

        }
    }
}
