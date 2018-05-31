namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMatchResultsPenalties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MatchResults", "HomePenalties", c => c.Int());
            AlterColumn("dbo.MatchResults", "AwayPenalties", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MatchResults", "AwayPenalties", c => c.Int(nullable: false));
            AlterColumn("dbo.MatchResults", "HomePenalties", c => c.Int(nullable: false));
        }
    }
}
