namespace Southworks.Prode.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 250),
                        Code = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 250),
                        Email = c.String(maxLength: 250),
                        AccessLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropTable("dbo.Users");
            DropTable("dbo.Countries");
        }
    }
}
