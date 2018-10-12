namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supr : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_Conjunto_Ejercicio", "ConjuntoEjercicioRutina_RutinaId", "dbo.SOGIP_Rutina");
            DropForeignKey("dbo.SOGIP_Rutina", "Usuario_Id", "dbo.SOGIP_Users");
            DropIndex("dbo.SOGIP_Rutina", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Conjunto_Ejercicio", new[] { "ConjuntoEjercicioRutina_RutinaId" });
            DropTable("dbo.SOGIP_Rutina");
            DropTable("dbo.SOGIP_Conjunto_Ejercicio");
        }
    }
}
