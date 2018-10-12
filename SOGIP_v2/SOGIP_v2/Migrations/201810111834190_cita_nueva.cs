namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cita_nueva : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SOGIP_Cita", "HorarioId_HorarioId", "dbo.SOGIP_Horario");
            DropIndex("dbo.SOGIP_Cita", new[] { "HorarioId_HorarioId" });
            AddColumn("dbo.SOGIP_Cita", "FechaHoraInicio", c => c.DateTime(nullable: false));
            AddColumn("dbo.SOGIP_Cita", "FechaHoraFinal", c => c.DateTime(nullable: false));
            DropColumn("dbo.SOGIP_Cita", "HorarioId_HorarioId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Cita", "HorarioId_HorarioId", c => c.Int());
            DropColumn("dbo.SOGIP_Cita", "FechaHoraFinal");
            DropColumn("dbo.SOGIP_Cita", "FechaHoraInicio");
            CreateIndex("dbo.SOGIP_Cita", "HorarioId_HorarioId");
            AddForeignKey("dbo.SOGIP_Cita", "HorarioId_HorarioId", "dbo.SOGIP_Horario", "HorarioId");
        }
    }
}
