namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cita_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Cita", "UsuarioCedula", c => c.String());
            AddColumn("dbo.SOGIP_Cita", "UsuarioNombre", c => c.String());
            AddColumn("dbo.SOGIP_Cita", "UsuarioApellido1", c => c.String());
            AddColumn("dbo.SOGIP_Cita", "UsuarioApellido2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SOGIP_Cita", "UsuarioApellido2");
            DropColumn("dbo.SOGIP_Cita", "UsuarioApellido1");
            DropColumn("dbo.SOGIP_Cita", "UsuarioNombre");
            DropColumn("dbo.SOGIP_Cita", "UsuarioCedula");
        }
    }
}
