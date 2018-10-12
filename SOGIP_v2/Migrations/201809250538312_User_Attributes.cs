namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Users", "Cedula", c => c.String());
            AddColumn("dbo.SOGIP_Users", "CedulaExtra", c => c.String());
            AddColumn("dbo.SOGIP_Users", "Fecha_Expiracion", c => c.DateTime(nullable: false));
            AddColumn("dbo.SOGIP_Users", "Nombre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SOGIP_Users", "Nombre");
            DropColumn("dbo.SOGIP_Users", "Fecha_Expiracion");
            DropColumn("dbo.SOGIP_Users", "CedulaExtra");
            DropColumn("dbo.SOGIP_Users", "Cedula");
        }
    }
}
