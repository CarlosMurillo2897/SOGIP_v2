namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pagos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_EstadosPagos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaPago = c.DateTime(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Monto = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                        Estado = c.Int(nullable: false),
                        IdPago_Id = c.Int(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SOGIP_TipoPago", t => t.IdPago_Id)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.IdPago_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_TipoPago",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOGIP_ListaPagos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        IdEsPago_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SOGIP_EstadosPagos", t => t.IdEsPago_Id)
                .Index(t => t.IdEsPago_Id);
            
            CreateTable(
                "dbo.SOGIP_PagoUsuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdEsPago_Id = c.Int(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SOGIP_EstadosPagos", t => t.IdEsPago_Id)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.IdEsPago_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_PagoUsuario", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_PagoUsuario", "IdEsPago_Id", "dbo.SOGIP_EstadosPagos");
            DropForeignKey("dbo.SOGIP_ListaPagos", "IdEsPago_Id", "dbo.SOGIP_EstadosPagos");
            DropForeignKey("dbo.SOGIP_EstadosPagos", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_EstadosPagos", "IdPago_Id", "dbo.SOGIP_TipoPago");
            DropIndex("dbo.SOGIP_PagoUsuario", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_PagoUsuario", new[] { "IdEsPago_Id" });
            DropIndex("dbo.SOGIP_ListaPagos", new[] { "IdEsPago_Id" });
            DropIndex("dbo.SOGIP_EstadosPagos", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_EstadosPagos", new[] { "IdPago_Id" });
            DropTable("dbo.SOGIP_PagoUsuario");
            DropTable("dbo.SOGIP_ListaPagos");
            DropTable("dbo.SOGIP_TipoPago");
            DropTable("dbo.SOGIP_EstadosPagos");
        }
    }
}
