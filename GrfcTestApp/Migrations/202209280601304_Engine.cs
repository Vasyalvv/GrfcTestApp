namespace GrfcTestApp.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Engine : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EngineTypes", newName: "EngineBases");
            AddColumn("dbo.EngineBases", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EngineBases", "Discriminator");
            RenameTable(name: "dbo.EngineBases", newName: "EngineTypes");
        }
    }
}
