namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchResultsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchResults",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomeGoals = c.Int(),
                        AwayGoals = c.Int(),
                        Result = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MatchResults");
        }
    }
}
