namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tipo_Archivo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Tipo",
                c => new
                    {
                        TipoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.TipoId)
                .Index(t => t.Nombre, unique: true);
            
            CreateTable(
                "dbo.SOGIP_Color",
                c => new
                    {
                        ColorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 60),
                        Codigo = c.String(maxLength: 60),
                        Seleccionado = c.Boolean(nullable: false),
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
            
            AddColumn("dbo.SOGIP_Archivo", "Tipo_TipoId", c => c.Int());
            CreateIndex("dbo.SOGIP_Archivo", "Tipo_TipoId");
            AddForeignKey("dbo.SOGIP_Archivo", "Tipo_TipoId", "dbo.SOGIP_Tipo", "TipoId");
            DropColumn("dbo.SOGIP_Archivo", "Tipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SOGIP_Archivo", "Tipo", c => c.String());
            DropForeignKey("dbo.SOGIP_Archivo", "Tipo_TipoId", "dbo.SOGIP_Tipo");
            DropIndex("dbo.SOGIP_Parametro", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Color", new[] { "Codigo" });
            DropIndex("dbo.SOGIP_Color", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Tipo", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Archivo", new[] { "Tipo_TipoId" });
            DropColumn("dbo.SOGIP_Archivo", "Tipo_TipoId");
            DropTable("dbo.SOGIP_Parametro");
            DropTable("dbo.SOGIP_Color");
            DropTable("dbo.SOGIP_Tipo");
        }
    }
}
