namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Color_Tipo_Parámetro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Color",
                c => new
                    {
                        ColorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 60),
                        Codigo = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.ColorId)
                .Index(t => t.Nombre, unique: true)
                .Index(t => t.Codigo, unique: true);
            
            CreateTable(
                "dbo.SOGIP_Parametro",
                c => new
                    {
                        ParametroId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 60),
                        Valor = c.String(),
                    })
                .PrimaryKey(t => t.ParametroId)
                .Index(t => t.Nombre, unique: true);
            
            CreateTable(
                "dbo.SOGIP_Tipo",
                c => new
                    {
                        TipoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.TipoId)
                .Index(t => t.Nombre, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.SOGIP_Tipo", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Parametro", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Color", new[] { "Codigo" });
            DropIndex("dbo.SOGIP_Color", new[] { "Nombre" });
            DropTable("dbo.SOGIP_Tipo");
            DropTable("dbo.SOGIP_Parametro");
            DropTable("dbo.SOGIP_Color");
        }
    }
}
