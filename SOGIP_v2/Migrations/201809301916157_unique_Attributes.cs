namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unique_Attributes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Selecciones", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Atletas", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Entidad_Publica", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Funcionario_ICODER", "Usuario_Id", "dbo.SOGIP_Users");
            DropIndex("dbo.SOGIP_Asociacion_Deportiva", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Atletas", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Entidad_Publica", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Funcionario_ICODER", new[] { "Usuario_Id" });
            AlterColumn("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", c => c.String());
            AlterColumn("dbo.SOGIP_Atletas", "Usuario_Id", c => c.String());
            AlterColumn("dbo.SOGIP_Selecciones", "Usuario_Id", c => c.String());
            AlterColumn("dbo.SOGIP_Categorias", "Descripcion", c => c.String(maxLength: 60));
            AlterColumn("dbo.SOGIP_Deportes", "Nombre", c => c.String(maxLength: 60));
            AlterColumn("dbo.SOGIP_Tipo_Deporte", "Descripcion", c => c.String(maxLength: 100));
            AlterColumn("dbo.SOGIP_Entidad_Publica", "Usuario_Id", c => c.String());
            AlterColumn("dbo.SOGIP_Tipo_Entidad", "Descripcion", c => c.String(maxLength: 100));
            AlterColumn("dbo.SOGIP_Estados", "Descripcion", c => c.String(maxLength: 60));
            AlterColumn("dbo.SOGIP_Funcionario_ICODER", "Usuario_Id", c => c.String());
            CreateIndex("dbo.SOGIP_Categorias", "Descripcion", unique: true);
            CreateIndex("dbo.SOGIP_Deportes", "Nombre", unique: true);
            CreateIndex("dbo.SOGIP_Tipo_Deporte", "Descripcion", unique: true);
            CreateIndex("dbo.SOGIP_Tipo_Entidad", "Descripcion", unique: true);
            CreateIndex("dbo.SOGIP_Estados", "Descripcion", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.SOGIP_Estados", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Tipo_Entidad", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Tipo_Deporte", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Deportes", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Categorias", new[] { "Descripcion" });
            AlterColumn("dbo.SOGIP_Funcionario_ICODER", "Usuario_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SOGIP_Estados", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Tipo_Entidad", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Entidad_Publica", "Usuario_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SOGIP_Tipo_Deporte", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Deportes", "Nombre", c => c.String());
            AlterColumn("dbo.SOGIP_Categorias", "Descripcion", c => c.String());
            AlterColumn("dbo.SOGIP_Selecciones", "Usuario_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SOGIP_Atletas", "Usuario_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.SOGIP_Funcionario_ICODER", "Usuario_Id");
            CreateIndex("dbo.SOGIP_Entidad_Publica", "Usuario_Id");
            CreateIndex("dbo.SOGIP_Selecciones", "Usuario_Id");
            CreateIndex("dbo.SOGIP_Atletas", "Usuario_Id");
            CreateIndex("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id");
            AddForeignKey("dbo.SOGIP_Funcionario_ICODER", "Usuario_Id", "dbo.SOGIP_Users", "Id");
            AddForeignKey("dbo.SOGIP_Entidad_Publica", "Usuario_Id", "dbo.SOGIP_Users", "Id");
            AddForeignKey("dbo.SOGIP_Atletas", "Usuario_Id", "dbo.SOGIP_Users", "Id");
            AddForeignKey("dbo.SOGIP_Selecciones", "Usuario_Id", "dbo.SOGIP_Users", "Id");
            AddForeignKey("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", "dbo.SOGIP_Users", "Id");
        }
    }
}
