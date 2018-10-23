namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ejercicio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOGIP_Conjunto_Ejercicio", "ColorEjercicio", c => c.String());
            AddColumn("dbo.SOGIP_Conjunto_Ejercicio", "diaEjercicio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SOGIP_Conjunto_Ejercicio", "diaEjercicio");
            DropColumn("dbo.SOGIP_Conjunto_Ejercicio", "ColorEjercicio");
        }
    }
}
