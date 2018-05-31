namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchResultsPenalties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MatchResults", "HomePenalties", c => c.Int(nullable: false));
            AddColumn("dbo.MatchResults", "AwayPenalties", c => c.Int(nullable: false));
            AlterColumn("dbo.MatchResults", "HomeGoals", c => c.Int(nullable: false));
            AlterColumn("dbo.MatchResults", "AwayGoals", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MatchResults", "AwayGoals", c => c.Int());
            AlterColumn("dbo.MatchResults", "HomeGoals", c => c.Int());
            DropColumn("dbo.MatchResults", "AwayPenalties");
            DropColumn("dbo.MatchResults", "HomePenalties");
        }
    }
}
