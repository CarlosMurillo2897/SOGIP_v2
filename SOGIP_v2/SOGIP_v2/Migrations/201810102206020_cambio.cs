namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Conjunto_Ejercicio", newName: "SOGIP_Conjunto_Ejercicio");
            RenameTable(name: "dbo.Rutinas", newName: "SOGIP_Rutina");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SOGIP_Rutina", newName: "Rutinas");
            RenameTable(name: "dbo.SOGIP_Conjunto_Ejercicio", newName: "Conjunto_Ejercicio");
        }
    }
}
