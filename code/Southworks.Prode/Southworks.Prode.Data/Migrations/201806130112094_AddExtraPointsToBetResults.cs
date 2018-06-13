namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExtraPointsToBetResults : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BetResults", "ExtraPoint", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BetResults", "ExtraPoint");
        }
    }
}
