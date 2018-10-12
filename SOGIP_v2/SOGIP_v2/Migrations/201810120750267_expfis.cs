namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expfis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Expedientes_Fisicos",
                c => new
                    {
                        ExpedienteFisicoId = c.Int(nullable: false, identity: true),
                        InBody = c.String(),
                        PruebaFuerza = c.String(),
                        Atleta_AtletaId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpedienteFisicoId)
                .ForeignKey("dbo.SOGIP_Atletas", t => t.Atleta_AtletaId)
                .Index(t => t.Atleta_AtletaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_Expedientes_Fisicos", "Atleta_AtletaId", "dbo.SOGIP_Atletas");
            DropIndex("dbo.SOGIP_Expedientes_Fisicos", new[] { "Atleta_AtletaId" });
            DropTable("dbo.SOGIP_Expedientes_Fisicos");
        }
    }
}
