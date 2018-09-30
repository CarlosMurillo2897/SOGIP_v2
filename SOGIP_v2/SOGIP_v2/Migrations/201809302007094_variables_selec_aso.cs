namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class variables_selec_aso : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Selecciones", "Categoria_CategoriaId", "dbo.SOGIP_Categorias");
            DropForeignKey("dbo.SOGIP_Selecciones", "Deporte_DeporteId", "dbo.SOGIP_Deportes");
            DropForeignKey("dbo.SOGIP_Selecciones", "Entrenador_EntrenadorId", "dbo.SOGIP_Entrenadores");
            DropForeignKey("dbo.SOGIP_Selecciones", "Usuario_Id", "dbo.SOGIP_Users");
            DropIndex("dbo.SOGIP_Asociacion_Deportiva", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Categoria_CategoriaId" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Deporte_DeporteId" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Entrenador_EntrenadorId" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Usuario_Id" });
            AddColumn("dbo.SOGIP_Selecciones", "Usuario", c => c.String());
            AddColumn("dbo.SOGIP_Selecciones", "Deporte_Id", c => c.Int(nullable: false));
            AddColumn("dbo.SOGIP_Selecciones", "Categoria_Id", c => c.Int(nullable: false));
            AddColumn("dbo.SOGIP_Selecciones", "Entrenador_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", c => c.String());
            DropColumn("dbo.SOGIP_Selecciones", "Categoria_CategoriaId");
            DropColumn("dbo.SOGIP_Selecciones", "Deporte_DeporteId");
            DropColumn("dbo.SOGIP_Selecciones", "Entrenador_EntrenadorId");
            DropColumn("dbo.SOGIP_Selecciones", "Usuario_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Selecciones", "Usuario_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.SOGIP_Selecciones", "Entrenador_EntrenadorId", c => c.Int());
            AddColumn("dbo.SOGIP_Selecciones", "Deporte_DeporteId", c => c.Int());
            AddColumn("dbo.SOGIP_Selecciones", "Categoria_CategoriaId", c => c.Int());
            AlterColumn("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.SOGIP_Selecciones", "Entrenador_Id");
            DropColumn("dbo.SOGIP_Selecciones", "Categoria_Id");
            DropColumn("dbo.SOGIP_Selecciones", "Deporte_Id");
            DropColumn("dbo.SOGIP_Selecciones", "Usuario");
            CreateIndex("dbo.SOGIP_Selecciones", "Usuario_Id");
            CreateIndex("dbo.SOGIP_Selecciones", "Entrenador_EntrenadorId");
            CreateIndex("dbo.SOGIP_Selecciones", "Deporte_DeporteId");
            CreateIndex("dbo.SOGIP_Selecciones", "Categoria_CategoriaId");
            CreateIndex("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id");
            AddForeignKey("dbo.SOGIP_Selecciones", "Usuario_Id", "dbo.SOGIP_Users", "Id");
            AddForeignKey("dbo.SOGIP_Selecciones", "Entrenador_EntrenadorId", "dbo.SOGIP_Entrenadores", "EntrenadorId");
            AddForeignKey("dbo.SOGIP_Selecciones", "Deporte_DeporteId", "dbo.SOGIP_Deportes", "DeporteId");
            AddForeignKey("dbo.SOGIP_Selecciones", "Categoria_CategoriaId", "dbo.SOGIP_Categorias", "CategoriaId");
            AddForeignKey("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", "dbo.SOGIP_Users", "Id");
        }
    }
}
