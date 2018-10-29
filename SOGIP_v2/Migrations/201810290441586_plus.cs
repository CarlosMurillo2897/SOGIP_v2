namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Serie1", c => c.String());
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Repeticion1", c => c.String());
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Peso1", c => c.String());
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Serie2", c => c.String());
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Repeticion2", c => c.String());
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Peso2", c => c.String());
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Serie3", c => c.String());
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Repeticion3", c => c.String());
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Peso3", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Peso3", c => c.Int(nullable: false));
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Repeticion3", c => c.Int(nullable: false));
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Serie3", c => c.Int(nullable: false));
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Peso2", c => c.Int(nullable: false));
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Repeticion2", c => c.Int(nullable: false));
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Serie2", c => c.Int(nullable: false));
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Peso1", c => c.Int(nullable: false));
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Repeticion1", c => c.Int(nullable: false));
            AlterColumn("dbo.SOGIP_Conjunto_Ejercicio", "Serie1", c => c.Int(nullable: false));
        }
    }
}
