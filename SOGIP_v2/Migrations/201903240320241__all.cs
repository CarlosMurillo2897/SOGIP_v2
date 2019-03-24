namespace SOGIP_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _all : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SOGIP_Actividad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Descripcion = c.String(),
                        Lugar = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOGIP_Archivo",
                c => new
                    {
                        ArchivoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Contenido = c.Binary(),
                        Tipo_TipoId = c.Int(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArchivoId)
                .ForeignKey("dbo.SOGIP_Tipo", t => t.Tipo_TipoId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Tipo_TipoId)
                .Index(t => t.Usuario_Id);
            
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
                "dbo.SOGIP_Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Cedula = c.String(),
                        CedulaExtra = c.String(),
                        Fecha_Expiracion = c.DateTime(nullable: false),
                        Nombre1 = c.String(),
                        Nombre2 = c.String(),
                        Apellido1 = c.String(),
                        Apellido2 = c.String(),
                        Fecha_Nacimiento = c.DateTime(nullable: false),
                        Sexo = c.Boolean(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.SOGIP_UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SOGIP_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SOGIP_UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.SOGIP_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SOGIP_UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.SOGIP_Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.SOGIP_Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SOGIP_Asociacion_Deportiva",
                c => new
                    {
                        Asociacion_DeportivaId = c.Int(nullable: false, identity: true),
                        Localidad = c.String(),
                        Nombre_DepAso = c.String(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Asociacion_DeportivaId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Atletas",
                c => new
                    {
                        AtletaId = c.Int(nullable: false, identity: true),
                        Asociacion_Deportiva_Asociacion_DeportivaId = c.Int(),
                        SubSeleccion_SubSeleccionId = c.Int(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AtletaId)
                .ForeignKey("dbo.SOGIP_Asociacion_Deportiva", t => t.Asociacion_Deportiva_Asociacion_DeportivaId)
                .ForeignKey("dbo.SOGIP_SubSeleccion", t => t.SubSeleccion_SubSeleccionId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Asociacion_Deportiva_Asociacion_DeportivaId)
                .Index(t => t.SubSeleccion_SubSeleccionId)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_SubSeleccion",
                c => new
                    {
                        SubSeleccionId = c.Int(nullable: false, identity: true),
                        Categoria_Id_CategoriaId = c.Int(),
                        Entrenador_Id = c.String(maxLength: 128),
                        Seleccion_SeleccionId = c.Int(),
                    })
                .PrimaryKey(t => t.SubSeleccionId)
                .ForeignKey("dbo.SOGIP_Categorias", t => t.Categoria_Id_CategoriaId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Entrenador_Id)
                .ForeignKey("dbo.SOGIP_Selecciones", t => t.Seleccion_SeleccionId)
                .Index(t => t.Categoria_Id_CategoriaId)
                .Index(t => t.Entrenador_Id)
                .Index(t => t.Seleccion_SeleccionId);
            
            CreateTable(
                "dbo.SOGIP_Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.CategoriaId)
                .Index(t => t.Descripcion, unique: true);
            
            CreateTable(
                "dbo.SOGIP_Selecciones",
                c => new
                    {
                        SeleccionId = c.Int(nullable: false, identity: true),
                        Nombre_Seleccion = c.String(),
                        Deporte_Id_DeporteId = c.Int(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SeleccionId)
                .ForeignKey("dbo.SOGIP_Deportes", t => t.Deporte_Id_DeporteId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Deporte_Id_DeporteId)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Deportes",
                c => new
                    {
                        DeporteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 60),
                        TipoDeporte_Tipo_DeporteId = c.Int(),
                    })
                .PrimaryKey(t => t.DeporteId)
                .ForeignKey("dbo.SOGIP_Tipo_Deporte", t => t.TipoDeporte_Tipo_DeporteId)
                .Index(t => t.Nombre, unique: true)
                .Index(t => t.TipoDeporte_Tipo_DeporteId);
            
            CreateTable(
                "dbo.SOGIP_Tipo_Deporte",
                c => new
                    {
                        Tipo_DeporteId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Tipo_DeporteId)
                .Index(t => t.Descripcion, unique: true);
            
            CreateTable(
                "dbo.SOGIP_Cita",
                c => new
                    {
                        CitaId = c.Int(nullable: false, identity: true),
                        InBody = c.Boolean(nullable: false),
                        Otro = c.Boolean(nullable: false),
                        FechaHoraInicio = c.DateTime(nullable: false),
                        FechaHoraFinal = c.DateTime(nullable: false),
                        UsuarioId_Id_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.SOGIP_Users", t => t.UsuarioId_Id_Id)
                .Index(t => t.UsuarioId_Id_Id);
            
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
                "dbo.SOGIP_Conjunto_Ejercicio",
                c => new
                    {
                        Conjunto_EjercicioId = c.Int(nullable: false, identity: true),
                        NombreEjercicio = c.String(),
                        Serie1 = c.String(),
                        Repeticion1 = c.String(),
                        Peso1 = c.String(),
                        Serie2 = c.String(),
                        Repeticion2 = c.String(),
                        Peso2 = c.String(),
                        Serie3 = c.String(),
                        Repeticion3 = c.String(),
                        Peso3 = c.String(),
                        ColorEjercicio = c.String(),
                        DiaEjercicio = c.String(),
                        ConjuntoEjercicioRutina_RutinaId = c.Int(),
                    })
                .PrimaryKey(t => t.Conjunto_EjercicioId)
                .ForeignKey("dbo.SOGIP_Rutina", t => t.ConjuntoEjercicioRutina_RutinaId)
                .Index(t => t.ConjuntoEjercicioRutina_RutinaId);
            
            CreateTable(
                "dbo.SOGIP_Rutina",
                c => new
                    {
                        RutinaId = c.Int(nullable: false, identity: true),
                        RutinaFecha = c.DateTime(nullable: false),
                        RutinaObservaciones = c.String(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RutinaId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Ejercicio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EjercicioId = c.Int(nullable: false),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOGIP_Entidad_Publica",
                c => new
                    {
                        Entidad_PublicaId = c.Int(nullable: false, identity: true),
                        Tipo_Entidad_Tipo_EntidadId = c.Int(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Entidad_PublicaId)
                .ForeignKey("dbo.SOGIP_Tipo_Entidad", t => t.Tipo_Entidad_Tipo_EntidadId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Tipo_Entidad_Tipo_EntidadId)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Tipo_Entidad",
                c => new
                    {
                        Tipo_EntidadId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Tipo_EntidadId)
                .Index(t => t.Descripcion, unique: true);
            
            CreateTable(
                "dbo.SOGIP_Estados",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.EstadoId)
                .Index(t => t.Descripcion, unique: true);
            
            CreateTable(
                "dbo.SOGIP_Expedientes_Fisicos",
                c => new
                    {
                        ExpedienteFisicoId = c.Int(nullable: false, identity: true),
                        InBody = c.Binary(),
                        PruebaFuerza = c.Binary(),
                        Atleta_AtletaId = c.Int(),
                    })
                .PrimaryKey(t => t.ExpedienteFisicoId)
                .ForeignKey("dbo.SOGIP_Atletas", t => t.Atleta_AtletaId)
                .Index(t => t.Atleta_AtletaId);
            
            CreateTable(
                "dbo.SOGIP_Funcionario_ICODER",
                c => new
                    {
                        Funcionario_ICODERId = c.Int(nullable: false, identity: true),
                        Entrenador_Id = c.String(maxLength: 128),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Funcionario_ICODERId)
                .ForeignKey("dbo.SOGIP_Users", t => t.Entrenador_Id)
                .ForeignKey("dbo.SOGIP_Users", t => t.Usuario_Id)
                .Index(t => t.Entrenador_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.SOGIP_Horario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaHoraInicio = c.DateTime(nullable: false),
                        FechaHoraFinal = c.DateTime(nullable: false),
                        IdActividad_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SOGIP_Actividad", t => t.IdActividad_Id)
                .Index(t => t.IdActividad_Id);
            
            CreateTable(
                "dbo.SOGIP_Maquina",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaquinaId = c.Int(nullable: false),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOGIP_MaquinaEjercicio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ejercicio_Id = c.Int(),
                        Maquina_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SOGIP_Ejercicio", t => t.Ejercicio_Id)
                .ForeignKey("dbo.SOGIP_Maquina", t => t.Maquina_Id)
                .Index(t => t.Ejercicio_Id)
                .Index(t => t.Maquina_Id);
            
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
                "dbo.SOGIP_Reservacion",
                c => new
                    {
                        ReservacionId = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        FechaHoraInicio = c.DateTime(nullable: false),
                        FechaHoraFinal = c.DateTime(nullable: false),
                        Estado_EstadoId = c.Int(),
                        UsuarioId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReservacionId)
                .ForeignKey("dbo.SOGIP_Estados", t => t.Estado_EstadoId)
                .ForeignKey("dbo.SOGIP_Users", t => t.UsuarioId_Id)
                .Index(t => t.Estado_EstadoId)
                .Index(t => t.UsuarioId_Id);
            
            CreateTable(
                "dbo.SOGIP_Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOGIP_UserRoles", "RoleId", "dbo.SOGIP_Roles");
            DropForeignKey("dbo.SOGIP_Reservacion", "UsuarioId_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Reservacion", "Estado_EstadoId", "dbo.SOGIP_Estados");
            DropForeignKey("dbo.SOGIP_MaquinaEjercicio", "Maquina_Id", "dbo.SOGIP_Maquina");
            DropForeignKey("dbo.SOGIP_MaquinaEjercicio", "Ejercicio_Id", "dbo.SOGIP_Ejercicio");
            DropForeignKey("dbo.SOGIP_Horario", "IdActividad_Id", "dbo.SOGIP_Actividad");
            DropForeignKey("dbo.SOGIP_Funcionario_ICODER", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Funcionario_ICODER", "Entrenador_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Expedientes_Fisicos", "Atleta_AtletaId", "dbo.SOGIP_Atletas");
            DropForeignKey("dbo.SOGIP_Entidad_Publica", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Entidad_Publica", "Tipo_Entidad_Tipo_EntidadId", "dbo.SOGIP_Tipo_Entidad");
            DropForeignKey("dbo.SOGIP_Conjunto_Ejercicio", "ConjuntoEjercicioRutina_RutinaId", "dbo.SOGIP_Rutina");
            DropForeignKey("dbo.SOGIP_Rutina", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Cita", "UsuarioId_Id_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Atletas", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Atletas", "SubSeleccion_SubSeleccionId", "dbo.SOGIP_SubSeleccion");
            DropForeignKey("dbo.SOGIP_SubSeleccion", "Seleccion_SeleccionId", "dbo.SOGIP_Selecciones");
            DropForeignKey("dbo.SOGIP_Selecciones", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Selecciones", "Deporte_Id_DeporteId", "dbo.SOGIP_Deportes");
            DropForeignKey("dbo.SOGIP_Deportes", "TipoDeporte_Tipo_DeporteId", "dbo.SOGIP_Tipo_Deporte");
            DropForeignKey("dbo.SOGIP_SubSeleccion", "Entrenador_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_SubSeleccion", "Categoria_Id_CategoriaId", "dbo.SOGIP_Categorias");
            DropForeignKey("dbo.SOGIP_Atletas", "Asociacion_Deportiva_Asociacion_DeportivaId", "dbo.SOGIP_Asociacion_Deportiva");
            DropForeignKey("dbo.SOGIP_Asociacion_Deportiva", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Archivo", "Usuario_Id", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_UserRoles", "UserId", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_UserLogins", "UserId", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_UserClaims", "UserId", "dbo.SOGIP_Users");
            DropForeignKey("dbo.SOGIP_Archivo", "Tipo_TipoId", "dbo.SOGIP_Tipo");
            DropIndex("dbo.SOGIP_Roles", "RoleNameIndex");
            DropIndex("dbo.SOGIP_Reservacion", new[] { "UsuarioId_Id" });
            DropIndex("dbo.SOGIP_Reservacion", new[] { "Estado_EstadoId" });
            DropIndex("dbo.SOGIP_Parametro", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_MaquinaEjercicio", new[] { "Maquina_Id" });
            DropIndex("dbo.SOGIP_MaquinaEjercicio", new[] { "Ejercicio_Id" });
            DropIndex("dbo.SOGIP_Horario", new[] { "IdActividad_Id" });
            DropIndex("dbo.SOGIP_Funcionario_ICODER", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Funcionario_ICODER", new[] { "Entrenador_Id" });
            DropIndex("dbo.SOGIP_Expedientes_Fisicos", new[] { "Atleta_AtletaId" });
            DropIndex("dbo.SOGIP_Estados", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Tipo_Entidad", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Entidad_Publica", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Entidad_Publica", new[] { "Tipo_Entidad_Tipo_EntidadId" });
            DropIndex("dbo.SOGIP_Rutina", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Conjunto_Ejercicio", new[] { "ConjuntoEjercicioRutina_RutinaId" });
            DropIndex("dbo.SOGIP_Color", new[] { "Codigo" });
            DropIndex("dbo.SOGIP_Color", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Cita", new[] { "UsuarioId_Id_Id" });
            DropIndex("dbo.SOGIP_Tipo_Deporte", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_Deportes", new[] { "TipoDeporte_Tipo_DeporteId" });
            DropIndex("dbo.SOGIP_Deportes", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Selecciones", new[] { "Deporte_Id_DeporteId" });
            DropIndex("dbo.SOGIP_Categorias", new[] { "Descripcion" });
            DropIndex("dbo.SOGIP_SubSeleccion", new[] { "Seleccion_SeleccionId" });
            DropIndex("dbo.SOGIP_SubSeleccion", new[] { "Entrenador_Id" });
            DropIndex("dbo.SOGIP_SubSeleccion", new[] { "Categoria_Id_CategoriaId" });
            DropIndex("dbo.SOGIP_Atletas", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Atletas", new[] { "SubSeleccion_SubSeleccionId" });
            DropIndex("dbo.SOGIP_Atletas", new[] { "Asociacion_Deportiva_Asociacion_DeportivaId" });
            DropIndex("dbo.SOGIP_Asociacion_Deportiva", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_UserRoles", new[] { "RoleId" });
            DropIndex("dbo.SOGIP_UserRoles", new[] { "UserId" });
            DropIndex("dbo.SOGIP_UserLogins", new[] { "UserId" });
            DropIndex("dbo.SOGIP_UserClaims", new[] { "UserId" });
            DropIndex("dbo.SOGIP_Users", "UserNameIndex");
            DropIndex("dbo.SOGIP_Tipo", new[] { "Nombre" });
            DropIndex("dbo.SOGIP_Archivo", new[] { "Usuario_Id" });
            DropIndex("dbo.SOGIP_Archivo", new[] { "Tipo_TipoId" });
            DropTable("dbo.SOGIP_Roles");
            DropTable("dbo.SOGIP_Reservacion");
            DropTable("dbo.SOGIP_Parametro");
            DropTable("dbo.SOGIP_MaquinaEjercicio");
            DropTable("dbo.SOGIP_Maquina");
            DropTable("dbo.SOGIP_Horario");
            DropTable("dbo.SOGIP_Funcionario_ICODER");
            DropTable("dbo.SOGIP_Expedientes_Fisicos");
            DropTable("dbo.SOGIP_Estados");
            DropTable("dbo.SOGIP_Tipo_Entidad");
            DropTable("dbo.SOGIP_Entidad_Publica");
            DropTable("dbo.SOGIP_Ejercicio");
            DropTable("dbo.SOGIP_Rutina");
            DropTable("dbo.SOGIP_Conjunto_Ejercicio");
            DropTable("dbo.SOGIP_Color");
            DropTable("dbo.SOGIP_Cita");
            DropTable("dbo.SOGIP_Tipo_Deporte");
            DropTable("dbo.SOGIP_Deportes");
            DropTable("dbo.SOGIP_Selecciones");
            DropTable("dbo.SOGIP_Categorias");
            DropTable("dbo.SOGIP_SubSeleccion");
            DropTable("dbo.SOGIP_Atletas");
            DropTable("dbo.SOGIP_Asociacion_Deportiva");
            DropTable("dbo.SOGIP_UserRoles");
            DropTable("dbo.SOGIP_UserLogins");
            DropTable("dbo.SOGIP_UserClaims");
            DropTable("dbo.SOGIP_Users");
            DropTable("dbo.SOGIP_Tipo");
            DropTable("dbo.SOGIP_Archivo");
            DropTable("dbo.SOGIP_Actividad");
        }
    }
}
