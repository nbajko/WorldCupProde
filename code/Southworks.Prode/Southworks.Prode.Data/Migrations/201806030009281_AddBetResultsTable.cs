namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBetResultsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BetResults",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        MatchId = c.Guid(nullable: false),
                        ResultId = c.Guid(nullable: false),
                        HitResult = c.Boolean(nullable: false),
                        HitHomeGoals = c.Boolean(nullable: false),
                        HitAwayGoals = c.Boolean(nullable: false),
                        HitGoalsDif = c.Boolean(nullable: false),
                        HitExactResult = c.Boolean(nullable: false),
                        HitPenalties = c.Boolean(nullable: false),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserId)
                .Index(t => t.MatchId)
                .Index(t => t.ResultId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BetResults", new[] { "ResultId" });
            DropIndex("dbo.BetResults", new[] { "MatchId" });
            DropIndex("dbo.BetResults", new[] { "UserId" });
            DropTable("dbo.BetResults");
        }
    }
}
