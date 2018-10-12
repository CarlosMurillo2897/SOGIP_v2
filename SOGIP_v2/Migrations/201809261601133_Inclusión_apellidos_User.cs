namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inclusión_apellidos_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Users", "apellido1", c => c.String());
            AddColumn("dbo.SOGIP_Users", "apellido2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SOGIP_Users", "apellido2");
            DropColumn("dbo.SOGIP_Users", "apellido1");
        }
    }
}
