namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maquinaejercicio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_MaquinaEjercicio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ejercicio_Id = c.Int(),
                        Maquina_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SOGIP_Ejercicio", t => t.Ejercicio_Id)
                .ForeignKey("dbo.SOGIP_Maquina", t => t.Maquina_Id)
                .Index(t => t.Ejercicio_Id)
                .Index(t => t.Maquina_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_MaquinaEjercicio", "Maquina_Id", "dbo.SOGIP_Maquina");
            DropForeignKey("dbo.SOGIP_MaquinaEjercicio", "Ejercicio_Id", "dbo.SOGIP_Ejercicio");
            DropIndex("dbo.SOGIP_MaquinaEjercicio", new[] { "Maquina_Id" });
            DropIndex("dbo.SOGIP_MaquinaEjercicio", new[] { "Ejercicio_Id" });
            DropTable("dbo.SOGIP_MaquinaEjercicio");
        }
    }
}
