namespace TravelGuide.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.About",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AboutImage = c.String(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(nullable: false),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUsername = c.String(),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUsername = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Image = c.String(),
                        Slogan = c.String(nullable: false),
                        DateInformation = c.String(nullable: false),
                        WanderPlaces = c.String(nullable: false),
                        Foods = c.String(nullable: false),
                        OtherInformations = c.String(nullable: false),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUsername = c.String(),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUsername = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        CommentText = c.String(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Owner_Id)
                .Index(t => t.CityId)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        RoleId = c.Int(nullable: false),
                        ProfileImage = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        RePassword = c.String(nullable: false, maxLength: 100),
                        PasswordResetDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUsername = c.String(),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUsername = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUsername = c.String(),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUsername = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUsername = c.String(),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUsername = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Image = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false),
                        RoadDescribe = c.String(nullable: false),
                        Latitude = c.String(nullable: false),
                        Longitude = c.String(nullable: false),
                        CityId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUsername = c.String(),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUsername = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.RoadDescribeUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Detail = c.String(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUsername = c.String(),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUsername = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Twitter = c.String(),
                        Instagram = c.String(),
                        Telegram = c.String(),
                        Github = c.String(),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUsername = c.String(),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUsername = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoadDescribeUnits", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Members", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Comments", "Owner_Id", "dbo.Members");
            DropForeignKey("dbo.Comments", "CityId", "dbo.Cities");
            DropIndex("dbo.RoadDescribeUnits", new[] { "PlaceId" });
            DropIndex("dbo.Places", new[] { "CityId" });
            DropIndex("dbo.Members", new[] { "RoleId" });
            DropIndex("dbo.Comments", new[] { "Owner_Id" });
            DropIndex("dbo.Comments", new[] { "CityId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropTable("dbo.Contact");
            DropTable("dbo.RoadDescribeUnits");
            DropTable("dbo.Places");
            DropTable("dbo.Countries");
            DropTable("dbo.Roles");
            DropTable("dbo.Members");
            DropTable("dbo.Comments");
            DropTable("dbo.Cities");
            DropTable("dbo.About");
        }
    }
}
