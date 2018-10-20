namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class varbinarymaxparaExp_Fís : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SOGIP_Expedientes_Fisicos", "InBody", c => c.Binary());
            AlterColumn("dbo.SOGIP_Expedientes_Fisicos", "PruebaFuerza", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SOGIP_Expedientes_Fisicos", "PruebaFuerza", c => c.Byte(nullable: false));
            AlterColumn("dbo.SOGIP_Expedientes_Fisicos", "InBody", c => c.Byte(nullable: false));
        }
    }
}
