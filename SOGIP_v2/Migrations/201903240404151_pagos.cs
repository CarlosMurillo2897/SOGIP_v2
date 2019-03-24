namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pagos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SOGIP_Ejercicio", "TipoId_Id", "dbo.SOGIP_TipoME");
            DropForeignKey("dbo.SOGIP_Maquina", "TipoId_Id", "dbo.SOGIP_TipoME");
            DropIndex("dbo.SOGIP_Ejercicio", new[] { "TipoId_Id" });
            DropIndex("dbo.SOGIP_Maquina", new[] { "TipoId_Id" });
            CreateTable(
                "dbo.SOGIP_CategoriaMonto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        monto = c.Int(nullable: false),
                        IdCategoria_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleViewModels", t => t.IdCategoria_Id)
                .Index(t => t.IdCategoria_Id);
            
            CreateTable(
                "dbo.RoleViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOGIP_EstadosPagos",
                c => new
                    {
                        IdEsPago = c.Int(nullable: false, identity: true),
                        FechaPago = c.DateTime(nullable: false),
                        estado = c.Boolean(nullable: false),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdEsPago)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_TotalPagos",
                c => new
                    {
                        IdPagos = c.Int(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false),
                        IdEst_IdEsPago = c.Int(),
                    })
                .PrimaryKey(t => t.IdPagos)
                .ForeignKey("dbo.SOGIP_EstadosPagos", t => t.IdEst_IdEsPago)
                .Index(t => t.IdEst_IdEsPago);
            
            AddColumn("dbo.SOGIP_Ejercicio", "EjercicioId", c => c.Int(nullable: false));
            AddColumn("dbo.SOGIP_Maquina", "MaquinaId", c => c.Int(nullable: false));
            AddColumn("dbo.SOGIP_Maquina", "Descripcion", c => c.String());
            DropColumn("dbo.SOGIP_Ejercicio", "TipoId_Id");
            DropColumn("dbo.SOGIP_Maquina", "Nombre");
            DropColumn("dbo.SOGIP_Maquina", "TipoId_Id");
            DropTable("dbo.SOGIP_TipoME");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.SOGIP_Maquina", "TipoId_Id", c => c.Int());
            AddColumn("dbo.SOGIP_Maquina", "Nombre", c => c.String());
            AddColumn("dbo.SOGIP_Ejercicio", "TipoId_Id", c => c.Int());
            DropForeignKey("dbo.SOGIP_TotalPagos", "IdEst_IdEsPago", "dbo.SOGIP_EstadosPagos");
            DropForeignKey("dbo.SOGIP_EstadosPagos", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_CategoriaMonto", "IdCategoria_Id", "dbo.RoleViewModels");
            DropIndex("dbo.SOGIP_TotalPagos", new[] { "IdEst_IdEsPago" });
            DropIndex("dbo.SOGIP_EstadosPagos", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_CategoriaMonto", new[] { "IdCategoria_Id" });
            DropColumn("dbo.SOGIP_Maquina", "Descripcion");
            DropColumn("dbo.SOGIP_Maquina", "MaquinaId");
            DropColumn("dbo.SOGIP_Ejercicio", "EjercicioId");
            DropTable("dbo.SOGIP_TotalPagos");
            DropTable("dbo.SOGIP_EstadosPagos");
            DropTable("dbo.RoleViewModels");
            DropTable("dbo.SOGIP_CategoriaMonto");
            CreateIndex("dbo.SOGIP_Maquina", "TipoId_Id");
            CreateIndex("dbo.SOGIP_Ejercicio", "TipoId_Id");
            AddForeignKey("dbo.SOGIP_Maquina", "TipoId_Id", "dbo.SOGIP_TipoME", "Id");
            AddForeignKey("dbo.SOGIP_Ejercicio", "TipoId_Id", "dbo.SOGIP_TipoME", "Id");
        }
    }
}
