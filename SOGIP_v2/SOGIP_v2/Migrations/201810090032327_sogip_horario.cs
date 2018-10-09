namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sogip_horario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Cita",
                c => new
                    {
                        CitaId = c.Int(nullable: false, identity: true),
                        InBody = c.Boolean(nullable: false),
                        Otro = c.Boolean(nullable: false),
                        HorarioId_HorarioId = c.Int(),
                        UsuarioId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.SOGIP_Horario", t => t.HorarioId_HorarioId)
                .ForeignKey("dbo.SOGIP_Users", t => t.UsuarioId_Id)
                .Index(t => t.HorarioId_HorarioId)
                .Index(t => t.UsuarioId_Id);
            
            CreateTable(
                "dbo.SOGIP_Horario",
                c => new
                    {
                        HorarioId = c.Int(nullable: false, identity: true),
                        FechaHoraInicio = c.DateTime(nullable: false),
                        FechaHoraFinal = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HorarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_Cita", "UsuarioId_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Cita", "HorarioId_HorarioId", "dbo.SOGIP_Horario");
            DropIndex("dbo.SOGIP_Cita", new[] { "UsuarioId_Id" });
            DropIndex("dbo.SOGIP_Cita", new[] { "HorarioId_HorarioId" });
            DropTable("dbo.SOGIP_Horario");
            DropTable("dbo.SOGIP_Cita");
        }
    }
}
