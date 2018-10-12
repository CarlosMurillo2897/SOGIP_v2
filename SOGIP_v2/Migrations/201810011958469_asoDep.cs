namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asoDep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Asociacion_Deportiva", "Nombre_DepAso", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SOGIP_Asociacion_Deportiva", "Nombre_DepAso");
        }
    }
}
