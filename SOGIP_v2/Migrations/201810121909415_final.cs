namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SOGIP_Selecciones", name: "Categoria_CategoriaId", newName: "Categoria_Id_CategoriaId");
            RenameColumn(table: "dbo.SOGIP_Selecciones", name: "Deporte_DeporteId", newName: "Deporte_Id_DeporteId");
            RenameColumn(table: "dbo.SOGIP_Selecciones", name: "Entrenador_EntrenadorId", newName: "Entrenador_Id_EntrenadorId");
            RenameIndex(table: "dbo.SOGIP_Selecciones", name: "IX_Categoria_CategoriaId", newName: "IX_Categoria_Id_CategoriaId");
            RenameIndex(table: "dbo.SOGIP_Selecciones", name: "IX_Deporte_DeporteId", newName: "IX_Deporte_Id_DeporteId");
            RenameIndex(table: "dbo.SOGIP_Selecciones", name: "IX_Entrenador_EntrenadorId", newName: "IX_Entrenador_Id_EntrenadorId");
            CreateTable(
                "dbo.SOGIP_Cita",
                c => new
                    {
                        CitaId = c.Int(nullable: false, identity: true),
                        InBody = c.Boolean(nullable: false),
                        Otro = c.Boolean(nullable: false),
                        FechaHoraInicio = c.DateTime(nullable: false),
                        FechaHoraFinal = c.DateTime(nullable: false),
                        UsuarioId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.SOGIP_Users", t => t.UsuarioId_Id)
                .Index(t => t.UsuarioId_Id);
            
            CreateTable(
                "dbo.SOGIP_Conjunto_Ejercicio",
                c => new
                    {
                        Conjunto_EjercicioId = c.Int(nullable: false, identity: true),
                        NombreEjercicio = c.String(),
                        Serie1 = c.Int(nullable: false),
                        Repeticion1 = c.Int(nullable: false),
                        Peso1 = c.Int(nullable: false),
                        Serie2 = c.Int(nullable: false),
                        Repeticion2 = c.Int(nullable: false),
                        Peso2 = c.Int(nullable: false),
                        Serie3 = c.Int(nullable: false),
                        Repeticion3 = c.Int(nullable: false),
                        Peso3 = c.Int(nullable: false),
                        ConjuntoEjercicioRutina_RutinaId = c.Int(),
                    })
                .PrimaryKey(t => t.Conjunto_EjercicioId)
                .ForeignKey("dbo.SOGIP_Rutina", t => t.ConjuntoEjercicioRutina_RutinaId)
                .Index(t => t.ConjuntoEjercicioRutina_RutinaId);
            
            CreateTable(
                "dbo.SOGIP_Rutina",
                c => new
                    {
                        RutinaId = c.Int(nullable: false, identity: true),
                        RutinaFecha = c.DateTime(nullable: false),
                        RutinaObservaciones = c.String(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RutinaId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Horario",
                c => new
                    {
                        HorarioId = c.Int(nullable: false, identity: true),
                        FechaHoraInicio = c.DateTime(nullable: false),
                        FechaHoraFinal = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HorarioId);
            
            CreateTable(
                "dbo.SOGIP_Expedientes_Fisicos",
                c => new
                    {
                        ExpedienteFisicoId = c.Int(nullable: false, identity: true),
                        InBody = c.String(),
                        PruebaFuerza = c.String(),
                        Atleta_AtletaId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpedienteFisicoId)
                .ForeignKey("dbo.SOGIP_Atletas", t => t.Atleta_AtletaId)
                .Index(t => t.Atleta_AtletaId);
            
            AddColumn("dbo.SOGIP_Asociacion_Deportiva", "Nombre_DepAso", c => c.String());
            AlterColumn("dbo.SOGIP_Categorias", "Descripcion", c => c.String(maxLength: 60));
            AlterColumn("dbo.SOGIP_Deportes", "Nombre", c => c.String(maxLength: 60));
            AlterColumn("dbo.SOGIP_Tipo_Deporte", "Descripcion", c => c.String(maxLength: 100));
            AlterColumn("dbo.SOGIP_Entrenadores", "Usuario_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SOGIP_Tipo_Entidad", "Descripcion", c => c.String(maxLength: 100));
            AlterColumn("dbo.SOGIP_Estados", "Descripcion", c => c.String(maxLength: 60));
            CreateIndex("dbo.SOGIP_Categorias", "Descripcion", unique: true);
            CreateIndex("dbo.SOGIP_Deportes", "Nombre", unique: true);
            CreateIndex("dbo.SOGIP_Tipo_Deporte", "Descripcion", unique: true);
            CreateIndex("dbo.SOGIP_Entrenadores", "Usuario_Id");
            CreateIndex("dbo.SOGIP_Tipo_Entidad", "Descripcion", unique: true);
            CreateIndex("dbo.SOGIP_Estados", "Descripcion", unique: true);
            AddForeignKey("dbo.SOGIP_Entrenadores", "Usuario_Id", "dbo.SOGIP_Users", "Id");
            DropColumn("dbo.SOGIP_Entidad_Publica", "NombreEntidad_Publica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Entidad_Publica", "NombreEntidad_Publica", c => c.String());
            DropForeignKey("dbo.SOGIP_Expedientes_Fisicos", "Atleta_AtletaId", "dbo.SOGIP_Atletas");
            DropForeignKey("dbo.SOGIP_Conjunto_Ejercicio", "ConjuntoEjercicioRutina_RutinaId", "dbo.SOGIP_Rutina");
            DropForeignKey("dbo.SOGIP_Rutina", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Cita", "UsuarioId_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Entrenadores", "Usuario_Id", "dbo.SOGIP_Users");
            DropIndex("dbo.SOGIP_Expedientes_Fisicos", new[] { "Atleta_AtletaId" });
            DropIndex("dbo.SOGIP_Estados", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Tipo_Entidad", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Rutina", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Conjunto_Ejercicio", new[] { "ConjuntoEjercicioRutina_RutinaId" });
            DropIndex("dbo.SOGIP_Cita", new[] { "UsuarioId_Id" });
            DropIndex("dbo.SOGIP_Entrenadores", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Tipo_Deporte", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Deportes", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Categorias", new[] { "Descripcion" });
            AlterColumn("dbo.SOGIP_Estados", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Tipo_Entidad", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Entrenadores", "Usuario_Id", c => c.String());
            AlterColumn("dbo.SOGIP_Tipo_Deporte", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Deportes", "Nombre", c => c.String());
            AlterColumn("dbo.SOGIP_Categorias", "Descripcion", c => c.String());
            DropColumn("dbo.SOGIP_Asociacion_Deportiva", "Nombre_DepAso");
            DropTable("dbo.SOGIP_Expedientes_Fisicos");
            DropTable("dbo.SOGIP_Horario");
            DropTable("dbo.SOGIP_Rutina");
            DropTable("dbo.SOGIP_Conjunto_Ejercicio");
            DropTable("dbo.SOGIP_Cita");
            RenameIndex(table: "dbo.SOGIP_Selecciones", name: "IX_Entrenador_Id_EntrenadorId", newName: "IX_Entrenador_EntrenadorId");
            RenameIndex(table: "dbo.SOGIP_Selecciones", name: "IX_Deporte_Id_DeporteId", newName: "IX_Deporte_DeporteId");
            RenameIndex(table: "dbo.SOGIP_Selecciones", name: "IX_Categoria_Id_CategoriaId", newName: "IX_Categoria_CategoriaId");
            RenameColumn(table: "dbo.SOGIP_Selecciones", name: "Entrenador_Id_EntrenadorId", newName: "Entrenador_EntrenadorId");
            RenameColumn(table: "dbo.SOGIP_Selecciones", name: "Deporte_Id_DeporteId", newName: "Deporte_DeporteId");
            RenameColumn(table: "dbo.SOGIP_Selecciones", name: "Categoria_Id_CategoriaId", newName: "Categoria_CategoriaId");
        }
    }
}
