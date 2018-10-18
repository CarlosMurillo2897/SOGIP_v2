namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iduser_cit : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SOGIP_Cita", name: "UsuarioId_Id", newName: "UsuarioId_Id_Id");
            RenameIndex(table: "dbo.SOGIP_Cita", name: "IX_UsuarioId_Id", newName: "IX_UsuarioId_Id_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SOGIP_Cita", name: "IX_UsuarioId_Id_Id", newName: "IX_UsuarioId_Id");
            RenameColumn(table: "dbo.SOGIP_Cita", name: "UsuarioId_Id_Id", newName: "UsuarioId_Id");
        }
    }
}
