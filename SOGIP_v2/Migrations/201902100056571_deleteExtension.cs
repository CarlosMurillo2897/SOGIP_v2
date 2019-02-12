namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteExtension : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SOGIP_Archivo", "Extension");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Archivo", "Extension", c => c.String());
        }
    }
}
