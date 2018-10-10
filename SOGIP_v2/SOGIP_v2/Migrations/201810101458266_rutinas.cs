namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rutinas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SOGIP_Ejercicio", "TipoEjercicio_EjercicioId", "dbo.SOGIP_Ejercicio");
            DropForeignKey("dbo.SOGIP_Conjunto_Ejercicio", "ConjuntoEjercicios_EjercicioId", "dbo.SOGIP_Ejercicio");
            DropForeignKey("dbo.SOGIP_Conjunto_Ejercicio", "Rutina_RutinaId", "dbo.SOGIP_Rutina");
            DropForeignKey("dbo.SOGIP_Rutina", "Usuario_Id", "dbo.SOGIP_Users");
            DropIndex("dbo.SOGIP_Conjunto_Ejercicio", new[] { "ConjuntoEjercicios_EjercicioId" });
            DropIndex("dbo.SOGIP_Conjunto_Ejercicio", new[] { "Rutina_RutinaId" });
            DropIndex("dbo.SOGIP_Ejercicio", new[] { "TipoEjercicio_EjercicioId" });
            DropIndex("dbo.SOGIP_Rutina", new[] { "Usuario_Id" });
            DropTable("dbo.SOGIP_Conjunto_Ejercicio");
            DropTable("dbo.SOGIP_Ejercicio");
            DropTable("dbo.SOGIP_Rutina");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SOGIP_Rutina",
                c => new
                    {
                        RutinaId = c.Int(nullable: false, identity: true),
                        RutinaFecha = c.DateTime(nullable: false),
                        RutinaObservaciones = c.String(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RutinaId);
            
            CreateTable(
                "dbo.SOGIP_Ejercicio",
                c => new
                    {
                        EjercicioId = c.Int(nullable: false, identity: true),
                        EjercicioNombre = c.String(),
                        EjercicioDescripcion = c.String(),
                        TipoEjercicio_EjercicioId = c.Int(),
                    })
                .PrimaryKey(t => t.EjercicioId);
            
            CreateTable(
                "dbo.SOGIP_Conjunto_Ejercicio",
                c => new
                    {
                        Conjunto_EjercicioId = c.Int(nullable: false, identity: true),
                        ConjuntoEjercicioSerie = c.Int(nullable: false),
                        ConjuntoEjercicioRepeticion = c.Int(nullable: false),
                        ConjuntoEjercicioPeso = c.Int(nullable: false),
                        ConjuntoEjercicios_EjercicioId = c.Int(),
                        Rutina_RutinaId = c.Int(),
                    })
                .PrimaryKey(t => t.Conjunto_EjercicioId);
            
            CreateIndex("dbo.SOGIP_Rutina", "Usuario_Id");
            CreateIndex("dbo.SOGIP_Ejercicio", "TipoEjercicio_EjercicioId");
            CreateIndex("dbo.SOGIP_Conjunto_Ejercicio", "Rutina_RutinaId");
            CreateIndex("dbo.SOGIP_Conjunto_Ejercicio", "ConjuntoEjercicios_EjercicioId");
            AddForeignKey("dbo.SOGIP_Rutina", "Usuario_Id", "dbo.SOGIP_Users", "Id");
            AddForeignKey("dbo.SOGIP_Conjunto_Ejercicio", "Rutina_RutinaId", "dbo.SOGIP_Rutina", "RutinaId");
            AddForeignKey("dbo.SOGIP_Conjunto_Ejercicio", "ConjuntoEjercicios_EjercicioId", "dbo.SOGIP_Ejercicio", "EjercicioId");
            AddForeignKey("dbo.SOGIP_Ejercicio", "TipoEjercicio_EjercicioId", "dbo.SOGIP_Ejercicio", "EjercicioId");
        }
    }
}
