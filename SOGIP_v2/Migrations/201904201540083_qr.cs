namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Archivo", "maquina_Id", c => c.Int());
            CreateIndex("dbo.SOGIP_Archivo", "maquina_Id");
            AddForeignKey("dbo.SOGIP_Archivo", "maquina_Id", "dbo.SOGIP_Maquina", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_Archivo", "maquina_Id", "dbo.SOGIP_Maquina");
            DropIndex("dbo.SOGIP_Archivo", new[] { "maquina_Id" });
            DropColumn("dbo.SOGIP_Archivo", "maquina_Id");
        }
    }
}
