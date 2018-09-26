namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SexoUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Users", "Sexo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SOGIP_Users", "Sexo");
        }
    }
}
