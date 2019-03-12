namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Reservacion",
                c => new
                    {
                        ReservacionId = c.Int(nullable: false, identity: true),
                        FechaHoraInicio = c.DateTime(nullable: false),
                        FechaHoraFinal = c.DateTime(nullable: false),
                        Estado_EstadoId = c.Int(),
                        UsuarioId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReservacionId)
                .ForeignKey("dbo.SOGIP_Estados", t => t.Estado_EstadoId)
                .ForeignKey("dbo.SOGIP_Users", t => t.UsuarioId_Id)
                .Index(t => t.Estado_EstadoId)
                .Index(t => t.UsuarioId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_Reservacion", "UsuarioId_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Reservacion", "Estado_EstadoId", "dbo.SOGIP_Estados");
            DropIndex("dbo.SOGIP_Reservacion", new[] { "UsuarioId_Id" });
            DropIndex("dbo.SOGIP_Reservacion", new[] { "Estado_EstadoId" });
            DropTable("dbo.SOGIP_Reservacion");
        }
    }
}
