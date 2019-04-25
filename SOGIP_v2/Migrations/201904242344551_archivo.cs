namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class archivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Archivo", "ejercicio_Id", c => c.Int());
            CreateIndex("dbo.SOGIP_Archivo", "ejercicio_Id");
            AddForeignKey("dbo.SOGIP_Archivo", "ejercicio_Id", "dbo.SOGIP_Ejercicio", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_Archivo", "ejercicio_Id", "dbo.SOGIP_Ejercicio");
            DropIndex("dbo.SOGIP_Archivo", new[] { "ejercicio_Id" });
            DropColumn("dbo.SOGIP_Archivo", "ejercicio_Id");
        }
    }
}
