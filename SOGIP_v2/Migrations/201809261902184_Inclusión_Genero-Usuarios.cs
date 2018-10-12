namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inclusión_GeneroUsuarios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Users", "Genero", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SOGIP_Users", "Genero");
        }
    }
}
