namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inclusión_Nombre1_2Apellido1_2Fecha_Nacimiento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Users", "Nombre1", c => c.String());
            AddColumn("dbo.SOGIP_Users", "Nombre2", c => c.String());
            AddColumn("dbo.SOGIP_Users", "Fecha_Nacimiento", c => c.DateTime(nullable: false));
            DropColumn("dbo.SOGIP_Users", "Nombre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Users", "Nombre", c => c.String());
            DropColumn("dbo.SOGIP_Users", "Fecha_Nacimiento");
            DropColumn("dbo.SOGIP_Users", "Nombre2");
            DropColumn("dbo.SOGIP_Users", "Nombre1");
        }
    }
}
