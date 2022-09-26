namespace GrfcTestApp.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Automobiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(nullable: false, maxLength: 2147483647),
                        CarModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarModels", t => t.CarModel_Id, cascadeDelete: true)
                .Index(t => t.CarModel_Id);
            
            CreateTable(
                "dbo.CarModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 2147483647),
                        CarMark_Id = c.Int(nullable: false),
                        EngineType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarMarks", t => t.CarMark_Id, cascadeDelete: true)
                .ForeignKey("dbo.EngineTypes", t => t.EngineType_Id, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.CarMark_Id)
                .Index(t => t.EngineType_Id);
            
            CreateTable(
                "dbo.CarMarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.EngineTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Maintenances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Automobile_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Automobiles", t => t.Automobile_Id, cascadeDelete: true)
                .Index(t => t.Automobile_Id);
            
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 2147483647),
                        EngineType_Id = c.Int(nullable: false),
                        Maintenance_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EngineTypes", t => t.EngineType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Maintenances", t => t.Maintenance_Id)
                .Index(t => t.EngineType_Id)
                .Index(t => t.Maintenance_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Operations", "Maintenance_Id", "dbo.Maintenances");
            DropForeignKey("dbo.Operations", "EngineType_Id", "dbo.EngineTypes");
            DropForeignKey("dbo.Maintenances", "Automobile_Id", "dbo.Automobiles");
            DropForeignKey("dbo.Automobiles", "CarModel_Id", "dbo.CarModels");
            DropForeignKey("dbo.CarModels", "EngineType_Id", "dbo.EngineTypes");
            DropForeignKey("dbo.CarModels", "CarMark_Id", "dbo.CarMarks");
            DropIndex("dbo.Operations", new[] { "Maintenance_Id" });
            DropIndex("dbo.Operations", new[] { "EngineType_Id" });
            DropIndex("dbo.Maintenances", new[] { "Automobile_Id" });
            DropIndex("dbo.EngineTypes", new[] { "Name" });
            DropIndex("dbo.CarMarks", new[] { "Name" });
            DropIndex("dbo.CarModels", new[] { "EngineType_Id" });
            DropIndex("dbo.CarModels", new[] { "CarMark_Id" });
            DropIndex("dbo.CarModels", new[] { "Name" });
            DropIndex("dbo.Automobiles", new[] { "CarModel_Id" });
            DropTable("dbo.Operations");
            DropTable("dbo.Maintenances");
            DropTable("dbo.EngineTypes");
            DropTable("dbo.CarMarks");
            DropTable("dbo.CarModels");
            DropTable("dbo.Automobiles");
        }
    }
}
