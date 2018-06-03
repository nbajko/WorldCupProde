namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchBetsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchBets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MatchId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        HomeGoals = c.Int(nullable: false),
                        AwayGoals = c.Int(nullable: false),
                        HomePenalties = c.Int(),
                        AwayPenalties = c.Int(),
                        Result = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.MatchId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MatchBets", new[] { "UserId" });
            DropIndex("dbo.MatchBets", new[] { "MatchId" });
            DropTable("dbo.MatchBets");
        }
    }
}
