namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class file : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Archivo",
                c => new
                    {
                        ArchivoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Contenido = c.Binary(),
                        Tipo = c.String(),
                        Extension = c.String(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArchivoId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_Archivo", "Usuario_Id", "dbo.SOGIP_Users");
            DropIndex("dbo.SOGIP_Archivo", new[] { "Usuario_Id" });
            DropTable("dbo.SOGIP_Archivo");
        }
    }
}
