namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchIdToResults : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MatchResults", "MatchId", c => c.Guid(nullable: false));
            CreateIndex("dbo.MatchResults", "MatchId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MatchResults", new[] { "MatchId" });
            DropColumn("dbo.MatchResults", "MatchId");
        }
    }
}
