namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tip : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_TipoME",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoId = c.Int(nullable: false),
                        nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SOGIP_Ejercicio", "TipoId_Id", c => c.Int());
            AddColumn("dbo.SOGIP_Maquina", "Nombre", c => c.String());
            AddColumn("dbo.SOGIP_Maquina", "TipoId_Id", c => c.Int());
            CreateIndex("dbo.SOGIP_Ejercicio", "TipoId_Id");
            CreateIndex("dbo.SOGIP_Maquina", "TipoId_Id");
            AddForeignKey("dbo.SOGIP_Ejercicio", "TipoId_Id", "dbo.SOGIP_TipoME", "Id");
            AddForeignKey("dbo.SOGIP_Maquina", "TipoId_Id", "dbo.SOGIP_TipoME", "Id");
            DropColumn("dbo.SOGIP_Ejercicio", "EjercicioId");
            DropColumn("dbo.SOGIP_Maquina", "MaquinaId");
            DropColumn("dbo.SOGIP_Maquina", "Descripcion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Maquina", "Descripcion", c => c.String());
            AddColumn("dbo.SOGIP_Maquina", "MaquinaId", c => c.Int(nullable: false));
            AddColumn("dbo.SOGIP_Ejercicio", "EjercicioId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SOGIP_Maquina", "TipoId_Id", "dbo.SOGIP_TipoME");
            DropForeignKey("dbo.SOGIP_Ejercicio", "TipoId_Id", "dbo.SOGIP_TipoME");
            DropIndex("dbo.SOGIP_Maquina", new[] { "TipoId_Id" });
            DropIndex("dbo.SOGIP_Ejercicio", new[] { "TipoId_Id" });
            DropColumn("dbo.SOGIP_Maquina", "TipoId_Id");
            DropColumn("dbo.SOGIP_Maquina", "Nombre");
            DropColumn("dbo.SOGIP_Ejercicio", "TipoId_Id");
            DropTable("dbo.SOGIP_TipoME");
        }
    }
}
