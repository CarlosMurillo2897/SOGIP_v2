namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entre_aso_lam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.SOGIP_Entrenadores", "Usuario_Id_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id_Id");
            CreateIndex("dbo.SOGIP_Entrenadores", "Usuario_Id_Id");
            AddForeignKey("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id_Id", "dbo.SOGIP_Users", "Id");
            AddForeignKey("dbo.SOGIP_Entrenadores", "Usuario_Id_Id", "dbo.SOGIP_Users", "Id");
            DropColumn("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id");
            DropColumn("dbo.SOGIP_Entrenadores", "Usuario_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Entrenadores", "Usuario_Id", c => c.String());
            AddColumn("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", c => c.String());
            DropForeignKey("dbo.SOGIP_Entrenadores", "Usuario_Id_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id_Id", "dbo.SOGIP_Users");
            DropIndex("dbo.SOGIP_Entrenadores", new[] { "Usuario_Id_Id" });
            DropIndex("dbo.SOGIP_Asociacion_Deportiva", new[] { "Usuario_Id_Id" });
            DropColumn("dbo.SOGIP_Entrenadores", "Usuario_Id_Id");
            DropColumn("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id_Id");
        }
    }
}
