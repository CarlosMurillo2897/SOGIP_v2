namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio_Nombre : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.SOGIP_UserLogins", newName: "SOGIP_UserLogins");
            RenameTable(name: "dbo.SOGIP_Asociacion_Deportiva", newName: "SOGIP_Asociacion_Deportiva");
            RenameTable(name: "dbo.SOGIP_Atletas", newName: "SOGIP_Atletas");
            RenameTable(name: "dbo.SOGIP_Selecciones", newName: "SOGIP_Selecciones");
            RenameTable(name: "dbo.SOGIP_Categorias", newName: "SOGIP_Categorias");
            RenameTable(name: "dbo.SOGIP_Deportes", newName: "SOGIP_Deportes");
            RenameTable(name: "dbo.SOGIP_Tipo_Deporte", newName: "SOGIP_Tipo_Deporte");
            RenameTable(name: "dbo.SOGIP_Entrenadores", newName: "SOGIP_Entrenadores");
            RenameTable(name: "dbo.SOGIP_Entidad_Publica", newName: "SOGIP_Entidad_Publica");
            RenameTable(name: "dbo.SOGIP_Tipo_Entidad", newName: "SOGIP_Tipo_Entidad");
            RenameTable(name: "dbo.SOGIP_Estados", newName: "SOGIP_Estados");
            RenameTable(name: "dbo.SOGIP_Funcionario_ICODER", newName: "SOGIP_Funcionario_ICODER");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SOGIP_Funcionario_ICODER", newName: "Funcionario_ICODER");
            RenameTable(name: "dbo.SOGIP_Estados", newName: "Estadoes");
            RenameTable(name: "dbo.SOGIP_Tipo_Entidad", newName: "Tipo_Entidad");
            RenameTable(name: "dbo.SOGIP_Entidad_Publica", newName: "Entidad_Publica");
            RenameTable(name: "dbo.SOGIP_Entrenadores", newName: "Entrenadors");
            RenameTable(name: "dbo.SOGIP_Tipo_Deporte", newName: "Tipo_Deporte");
            RenameTable(name: "dbo.SOGIP_Deportes", newName: "Deportes");
            RenameTable(name: "dbo.SOGIP_Categorias", newName: "Categorias");
            RenameTable(name: "dbo.SOGIP_Selecciones", newName: "Seleccions");
            RenameTable(name: "dbo.SOGIP_Atletas", newName: "Atletas");
            RenameTable(name: "dbo.SOGIP_Asociacion_Deportiva", newName: "Asociacion_Deportiva");
        }
    }
}
