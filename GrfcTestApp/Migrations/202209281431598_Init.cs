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
                .ForeignKey("dbo.EngineBases", t => t.EngineType_Id, cascadeDelete: true)
                .Index(t => new { t.Name, t.CarMark_Id }, name: "IX_NameAndCarMark")
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
                "dbo.EngineBases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 2147483647),
                        Discriminator = c.String(nullable: false, maxLength: 128),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EngineBases", t => t.EngineType_Id, cascadeDelete: true)
                .Index(t => t.EngineType_Id);
            
            CreateTable(
                "dbo.OperationMaintenances",
                c => new
                    {
                        Operation_Id = c.Int(nullable: false),
                        Maintenance_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Operation_Id, t.Maintenance_Id })
                .ForeignKey("dbo.Operations", t => t.Operation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Maintenances", t => t.Maintenance_Id, cascadeDelete: true)
                .Index(t => t.Operation_Id)
                .Index(t => t.Maintenance_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OperationMaintenances", "Maintenance_Id", "dbo.Maintenances");
            DropForeignKey("dbo.OperationMaintenances", "Operation_Id", "dbo.Operations");
            DropForeignKey("dbo.Operations", "EngineType_Id", "dbo.EngineBases");
            DropForeignKey("dbo.Maintenances", "Automobile_Id", "dbo.Automobiles");
            DropForeignKey("dbo.Automobiles", "CarModel_Id", "dbo.CarModels");
            DropForeignKey("dbo.CarModels", "EngineType_Id", "dbo.EngineBases");
            DropForeignKey("dbo.CarModels", "CarMark_Id", "dbo.CarMarks");
            DropIndex("dbo.OperationMaintenances", new[] { "Maintenance_Id" });
            DropIndex("dbo.OperationMaintenances", new[] { "Operation_Id" });
            DropIndex("dbo.Operations", new[] { "EngineType_Id" });
            DropIndex("dbo.Maintenances", new[] { "Automobile_Id" });
            DropIndex("dbo.EngineBases", new[] { "Name" });
            DropIndex("dbo.CarMarks", new[] { "Name" });
            DropIndex("dbo.CarModels", new[] { "EngineType_Id" });
            DropIndex("dbo.CarModels", "IX_NameAndCarMark");
            DropIndex("dbo.Automobiles", new[] { "CarModel_Id" });
            DropTable("dbo.OperationMaintenances");
            DropTable("dbo.Operations");
            DropTable("dbo.Maintenances");
            DropTable("dbo.EngineBases");
            DropTable("dbo.CarMarks");
            DropTable("dbo.CarModels");
            DropTable("dbo.Automobiles");
        }
    }
}
