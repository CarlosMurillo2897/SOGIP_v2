namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SOGIP_Asociacion_Deportiva", name: "Usuario_Id_Id", newName: "Usuario_Id");
            RenameColumn(table: "dbo.SOGIP_Entrenadores", name: "Usuario_Id_Id", newName: "Usuario_Id");
            RenameIndex(table: "dbo.SOGIP_Asociacion_Deportiva", name: "IX_Usuario_Id_Id", newName: "IX_Usuario_Id");
            RenameIndex(table: "dbo.SOGIP_Entrenadores", name: "IX_Usuario_Id_Id", newName: "IX_Usuario_Id");
            AlterColumn("dbo.SOGIP_Categorias", "Descripcion", c => c.String(maxLength: 60));
            AlterColumn("dbo.SOGIP_Deportes", "Nombre", c => c.String(maxLength: 60));
            AlterColumn("dbo.SOGIP_Tipo_Deporte", "Descripcion", c => c.String(maxLength: 100));
            AlterColumn("dbo.SOGIP_Tipo_Entidad", "Descripcion", c => c.String(maxLength: 100));
            AlterColumn("dbo.SOGIP_Estados", "Descripcion", c => c.String(maxLength: 60));
            CreateIndex("dbo.SOGIP_Categorias", "Descripcion", unique: true);
            CreateIndex("dbo.SOGIP_Deportes", "Nombre", unique: true);
            CreateIndex("dbo.SOGIP_Tipo_Deporte", "Descripcion", unique: true);
            CreateIndex("dbo.SOGIP_Tipo_Entidad", "Descripcion", unique: true);
            CreateIndex("dbo.SOGIP_Estados", "Descripcion", unique: true);
            DropColumn("dbo.SOGIP_Entidad_Publica", "NombreEntidad_Publica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Entidad_Publica", "NombreEntidad_Publica", c => c.String());
            DropIndex("dbo.SOGIP_Estados", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Tipo_Entidad", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Tipo_Deporte", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Deportes", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Categorias", new[] { "Descripcion" });
            AlterColumn("dbo.SOGIP_Estados", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Tipo_Entidad", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Tipo_Deporte", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Deportes", "Nombre", c => c.String());
            AlterColumn("dbo.SOGIP_Categorias", "Descripcion", c => c.String());
            RenameIndex(table: "dbo.SOGIP_Entrenadores", name: "IX_Usuario_Id", newName: "IX_Usuario_Id_Id");
            RenameIndex(table: "dbo.SOGIP_Asociacion_Deportiva", name: "IX_Usuario_Id", newName: "IX_Usuario_Id_Id");
            RenameColumn(table: "dbo.SOGIP_Entrenadores", name: "Usuario_Id", newName: "Usuario_Id_Id");
            RenameColumn(table: "dbo.SOGIP_Asociacion_Deportiva", name: "Usuario_Id", newName: "Usuario_Id_Id");
        }
    }
}
