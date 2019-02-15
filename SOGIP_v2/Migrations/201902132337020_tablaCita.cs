namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablaCita : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SOGIP_Cita", "UsuarioCedula");
            DropColumn("dbo.SOGIP_Cita", "UsuarioNombre");
            DropColumn("dbo.SOGIP_Cita", "UsuarioApellido1");
            DropColumn("dbo.SOGIP_Cita", "UsuarioApellido2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Cita", "UsuarioApellido2", c => c.String());
            AddColumn("dbo.SOGIP_Cita", "UsuarioApellido1", c => c.String());
            AddColumn("dbo.SOGIP_Cita", "UsuarioNombre", c => c.String());
            AddColumn("dbo.SOGIP_Cita", "UsuarioCedula", c => c.String());
        }
    }
}
