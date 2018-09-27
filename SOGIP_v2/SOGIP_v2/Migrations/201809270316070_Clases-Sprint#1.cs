namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClasesSprint1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Asociacion_Deportiva",
                c => new
                    {
                        Asociacion_DeportivaId = c.Int(nullable: false, identity: true),
                        Localidad = c.String(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Asociacion_DeportivaId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Atletas",
                c => new
                    {
                        AtletaId = c.Int(nullable: false, identity: true),
                        Localidad = c.String(),
                        Asociacion_Deportiva_Asociacion_DeportivaId = c.Int(),
                        Seleccion_SeleccionId = c.Int(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AtletaId)
                .ForeignKey("dbo.SOGIP_Asociacion_Deportiva", t => t.Asociacion_Deportiva_Asociacion_DeportivaId)
                .ForeignKey("dbo.SOGIP_Selecciones", t => t.Seleccion_SeleccionId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Asociacion_Deportiva_Asociacion_DeportivaId)
                .Index(t => t.Seleccion_SeleccionId)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Selecciones",
                c => new
                    {
                        SeleccionId = c.Int(nullable: false, identity: true),
                        Nombre_Seleccion = c.String(),
                        Categoria_CategoriaId = c.Int(),
                        Deporte_DeporteId = c.Int(),
                        Entrenador_EntrenadorId = c.Int(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SeleccionId)
                .ForeignKey("dbo.SOGIP_Categorias", t => t.Categoria_CategoriaId)
                .ForeignKey("dbo.SOGIP_Deportes", t => t.Deporte_DeporteId)
                .ForeignKey("dbo.SOGIP_Entrenadores", t => t.Entrenador_EntrenadorId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Categoria_CategoriaId)
                .Index(t => t.Deporte_DeporteId)
                .Index(t => t.Entrenador_EntrenadorId)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.SOGIP_Deportes",
                c => new
                    {
                        DeporteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        TipoDeporte_Tipo_DeporteId = c.Int(),
                    })
                .PrimaryKey(t => t.DeporteId)
                .ForeignKey("dbo.SOGIP_Tipo_Deporte", t => t.TipoDeporte_Tipo_DeporteId)
                .Index(t => t.TipoDeporte_Tipo_DeporteId);
            
            CreateTable(
                "dbo.SOGIP_Tipo_Deporte",
                c => new
                    {
                        Tipo_DeporteId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Tipo_DeporteId);
            
            CreateTable(
                "dbo.SOGIP_Entrenadores",
                c => new
                    {
                        EntrenadorId = c.Int(nullable: false, identity: true),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EntrenadorId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Entidad_Publica",
                c => new
                    {
                        Entidad_PublicaId = c.Int(nullable: false, identity: true),
                        NombreEntidad_Publica = c.String(),
                        Tipo_Entidad_Tipo_EntidadId = c.Int(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Entidad_PublicaId)
                .ForeignKey("dbo.SOGIP_Tipo_Entidad", t => t.Tipo_Entidad_Tipo_EntidadId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Tipo_Entidad_Tipo_EntidadId)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Tipo_Entidad",
                c => new
                    {
                        Tipo_EntidadId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Tipo_EntidadId);
            
            CreateTable(
                "dbo.SOGIP_Estados",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.EstadoId);
            
            CreateTable(
               "dbo.SOGIP_Funcionario_ICODER",
                c => new
                    {
                        Funcionario_ICODERId = c.Int(nullable: false, identity: true),
                        Entrenador_Id = c.String(maxLength: 128),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Funcionario_ICODERId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Entrenador_Id)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Entrenador_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_Funcionario_ICODER", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Funcionario_ICODER", "Entrenador_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Entidad_Publica", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Entidad_Publica", "Tipo_Entidad_Tipo_EntidadId", "dbo.SOGIP_Tipo_Entidad");
            DropForeignKey("dbo.SOGIP_Atletas", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Atletas", "Seleccion_SeleccionId", "dbo.SOGIP_Selecciones");
            DropForeignKey("dbo.SOGIP_Selecciones", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Selecciones", "Entrenador_EntrenadorId", "dbo.SOGIP_Entrenadores");
            DropForeignKey("dbo.SOGIP_Entrenadores", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Selecciones", "Deporte_DeporteId", "dbo.SOGIP_Deportes");
            DropForeignKey("dbo.SOGIP_Deportes", "TipoDeporte_Tipo_DeporteId", "dbo.SOGIP_Tipo_Deporte");
            DropForeignKey("dbo.SOGIP_Selecciones", "Categoria_CategoriaId", "dbo.SOGIP_Categorias");
            DropForeignKey("dbo.SOGIP_Atletas", "Asociacion_Deportiva_Asociacion_DeportivaId", "dbo.SOGIP_Asociacion_Deportiva");
            DropForeignKey("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", "dbo.SOGIP_Users");
            DropIndex("dbo.SOGIP_Funcionario_ICODER", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Funcionario_ICODER", new[] { "Entrenador_Id" });
            DropIndex("dbo.SOGIP_Entidad_Publica", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Entidad_Publica", new[] { "Tipo_Entidad_Tipo_EntidadId" });
            DropIndex("dbo.SOGIP_Entrenadores", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Deportes", new[] { "TipoDeporte_Tipo_DeporteId" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Entrenador_EntrenadorId" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Deporte_DeporteId" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Categoria_CategoriaId" });
            DropIndex("dbo.SOGIP_Atletas", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Atletas", new[] { "Seleccion_SeleccionId" });
            DropIndex("dbo.SOGIP_Atletas", new[] { "Asociacion_Deportiva_Asociacion_DeportivaId" });
            DropIndex("dbo.SOGIP_Asociacion_Deportiva", new[] { "Usuario_Id" });
            DropTable("dbo.SOGIP_Funcionario_ICODER");
            DropTable("dbo.SOGIP_Estados");
            DropTable("dbo.SOGIP_Tipo_Entidad");
            DropTable("dbo.SOGIP_Entidad_Publica");
            DropTable("dbo.SOGIP_Entrenadores");
            DropTable("dbo.SOGIP_Tipo_Deporte");
            DropTable("dbo.SOGIP_Deportes");
            DropTable("dbo.SOGIP_Categorias");
            DropTable("dbo.SOGIP_Selecciones");
            DropTable("dbo.SOGIP_Atletas");
            DropTable("dbo.SOGIP_Asociacion_Deportiva");
        }
    }
}
