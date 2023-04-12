namespace WpfApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDirection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Events", "DirectionId", c => c.Int());
            CreateIndex("dbo.Events", "DirectionId");
            AddForeignKey("dbo.Events", "DirectionId", "dbo.Directions", "Id");
            DropColumn("dbo.Events", "Direction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Direction", c => c.String());
            DropForeignKey("dbo.Events", "Direction_Id", "dbo.Directions");
            DropIndex("dbo.Events", new[] { "Direction_Id" });
            DropColumn("dbo.Events", "Direction_Id");
            DropTable("dbo.Directions");
        }
    }
}
