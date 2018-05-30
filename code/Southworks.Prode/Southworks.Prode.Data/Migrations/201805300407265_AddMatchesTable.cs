namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomeTeam = c.Guid(nullable: false),
                        AwayTeam = c.Guid(nullable: false),
                        PlayedOn = c.DateTime(),
                        Stage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.HomeTeam)
                .Index(t => t.AwayTeam)
                .Index(t => t.PlayedOn);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Matches", new[] { "PlayedOn" });
            DropIndex("dbo.Matches", new[] { "AwayTeam" });
            DropIndex("dbo.Matches", new[] { "HomeTeam" });
            DropTable("dbo.Matches");
        }
    }
}
