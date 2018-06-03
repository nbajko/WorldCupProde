namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetResultNotNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MatchBets", "Result", c => c.Int(nullable: false));
            AlterColumn("dbo.MatchResults", "Result", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MatchResults", "Result", c => c.Int());
            AlterColumn("dbo.MatchBets", "Result", c => c.Int());
        }
    }
}
