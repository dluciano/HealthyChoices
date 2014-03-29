namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodRegister",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 1000),
                        FoodTypeOther = c.Int(),
                        CreationDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        TakenAtId = c.Int(nullable: false),
                        FoodTypeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodType", t => t.FoodTypeId)
                .ForeignKey("dbo.TakenAt", t => t.TakenAtId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.FoodTypeId)
                .Index(t => t.TakenAtId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.FoodType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 500),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TakenAt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 500),
                        OrderNumber = c.Byte(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        LastName = c.String(nullable: true, maxLength: 200),
                        Gender = c.String(nullable: false, maxLength: 4000),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.FoodRegister", "UserId", "dbo.User");
            DropForeignKey("dbo.FoodRegister", "TakenAtId", "dbo.TakenAt");
            DropForeignKey("dbo.FoodRegister", "FoodTypeId", "dbo.FoodType");
            DropIndex("dbo.FoodRegister", new[] { "UserId" });
            DropIndex("dbo.FoodRegister", new[] { "TakenAtId" });
            DropIndex("dbo.FoodRegister", new[] { "FoodTypeId" });
            DropTable("dbo.User");
            DropTable("dbo.TakenAt");
            DropTable("dbo.FoodType");
            DropTable("dbo.FoodRegister");
        }
    }
}
