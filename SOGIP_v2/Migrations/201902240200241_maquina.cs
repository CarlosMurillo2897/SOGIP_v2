namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maquina : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Ejercicio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EjercicioId = c.Int(nullable: false),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOGIP_Maquina",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaquinaId = c.Int(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SOGIP_Maquina");
            DropTable("dbo.SOGIP_Ejercicio");
        }
    }
}
