namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class selec : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Selecciones", "Categoria_Id_CategoriaId", c => c.Int());
            AddColumn("dbo.SOGIP_Selecciones", "Deporte_Id_DeporteId", c => c.Int());
            AddColumn("dbo.SOGIP_Selecciones", "Entrenador_Id_EntrenadorId", c => c.Int());
            AddColumn("dbo.SOGIP_Selecciones", "Usuario_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.SOGIP_Selecciones", "Categoria_Id_CategoriaId");
            CreateIndex("dbo.SOGIP_Selecciones", "Deporte_Id_DeporteId");
            CreateIndex("dbo.SOGIP_Selecciones", "Entrenador_Id_EntrenadorId");
            CreateIndex("dbo.SOGIP_Selecciones", "Usuario_Id");
            AddForeignKey("dbo.SOGIP_Selecciones", "Categoria_Id_CategoriaId", "dbo.SOGIP_Categorias", "CategoriaId");
            AddForeignKey("dbo.SOGIP_Selecciones", "Deporte_Id_DeporteId", "dbo.SOGIP_Deportes", "DeporteId");
            AddForeignKey("dbo.SOGIP_Selecciones", "Entrenador_Id_EntrenadorId", "dbo.SOGIP_Entrenadores", "EntrenadorId");
            AddForeignKey("dbo.SOGIP_Selecciones", "Usuario_Id", "dbo.SOGIP_Users", "Id");
            DropColumn("dbo.SOGIP_Selecciones", "Usuario");
            DropColumn("dbo.SOGIP_Selecciones", "Deporte_Id");
            DropColumn("dbo.SOGIP_Selecciones", "Categoria_Id");
            DropColumn("dbo.SOGIP_Selecciones", "Entrenador_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Selecciones", "Entrenador_Id", c => c.Int(nullable: false));
            AddColumn("dbo.SOGIP_Selecciones", "Categoria_Id", c => c.Int(nullable: false));
            AddColumn("dbo.SOGIP_Selecciones", "Deporte_Id", c => c.Int(nullable: false));
            AddColumn("dbo.SOGIP_Selecciones", "Usuario", c => c.String());
            DropForeignKey("dbo.SOGIP_Selecciones", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Selecciones", "Entrenador_Id_EntrenadorId", "dbo.SOGIP_Entrenadores");
            DropForeignKey("dbo.SOGIP_Selecciones", "Deporte_Id_DeporteId", "dbo.SOGIP_Deportes");
            DropForeignKey("dbo.SOGIP_Selecciones", "Categoria_Id_CategoriaId", "dbo.SOGIP_Categorias");
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Entrenador_Id_EntrenadorId" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Deporte_Id_DeporteId" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Categoria_Id_CategoriaId" });
            DropColumn("dbo.SOGIP_Selecciones", "Usuario_Id");
            DropColumn("dbo.SOGIP_Selecciones", "Entrenador_Id_EntrenadorId");
            DropColumn("dbo.SOGIP_Selecciones", "Deporte_Id_DeporteId");
            DropColumn("dbo.SOGIP_Selecciones", "Categoria_Id_CategoriaId");
        }
    }
}
