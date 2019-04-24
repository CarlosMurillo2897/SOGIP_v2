namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class control : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SOGIP_Ingreso", newName: "SOGIP_TipoPago");
            RenameTable(name: "dbo.Ingresoes", newName: "SOGIP_ControlIngreso");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SOGIP_ControlIngreso", newName: "Ingresoes");
            RenameTable(name: "dbo.SOGIP_TipoPago", newName: "SOGIP_Ingreso");
        }
    }
}
