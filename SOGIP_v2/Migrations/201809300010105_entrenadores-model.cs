namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entrenadoresmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SOGIP_Entrenadores", "Usuario_Id", "dbo.SOGIP_Users");
            DropIndex("dbo.SOGIP_Entrenadores", new[] { "Usuario_Id" });
            AlterColumn("dbo.SOGIP_Entrenadores", "Usuario_Id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SOGIP_Entrenadores", "Usuario_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.SOGIP_Entrenadores", "Usuario_Id");
            AddForeignKey("dbo.SOGIP_Entrenadores", "Usuario_Id", "dbo.SOGIP_Users", "Id");
        }
    }
}
