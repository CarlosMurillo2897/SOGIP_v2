﻿using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SOGIP_v2.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        
        public string Cedula { get; set; }
        public string CedulaExtra { get; set; }
        public DateTime Fecha_Expiracion { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public Boolean Sexo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            userIdentity.AddClaim(new Claim("Cedula", this.Cedula));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("SOGIP_Users");
            modelBuilder.Entity<IdentityRole>().ToTable("SOGIP_Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("SOGIP_UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("SOGIP_UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("SOGIP_UserLogins");

            modelBuilder.Entity<Asociacion_Deportiva>().ToTable("SOGIP_Asociacion_Deportiva");
            modelBuilder.Entity<Atleta>().ToTable("SOGIP_Atletas");
            modelBuilder.Entity<Categoria>().ToTable("SOGIP_Categorias");
            modelBuilder.Entity<Deporte>().ToTable("SOGIP_Deportes");
            modelBuilder.Entity<Entidad_Publica>().ToTable("SOGIP_Entidad_Publica");
            modelBuilder.Entity<Entrenador>().ToTable("SOGIP_Entrenadores");
            modelBuilder.Entity<Estado>().ToTable("SOGIP_Estados");
            modelBuilder.Entity<Funcionario_ICODER>().ToTable("SOGIP_Funcionario_ICODER");
            modelBuilder.Entity<Tipo_Deporte>().ToTable("SOGIP_Tipo_Deporte");
            modelBuilder.Entity<Tipo_Entidad>().ToTable("SOGIP_Tipo_Entidad");
            modelBuilder.Entity<Seleccion>().ToTable("SOGIP_Selecciones");
            modelBuilder.Entity<Horario>().ToTable("SOGIP_Horario");
            modelBuilder.Entity<Cita>().ToTable("SOGIP_Cita");
            //modelBuilder.Entity<Ejercicio>().ToTable("SOGIP_Ejercicio");
            //modelBuilder.Entity<Conjunto_Ejercicio>().ToTable("SOGIP_Conjunto_Ejercicio");
            //modelBuilder.Entity<Rutina>().ToTable("SOGIP_Rutina");

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Estado> Estados { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Tipo_Deporte> Tipo_Deporte { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Deporte> Deportes { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Entrenador> Entrenadores { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Seleccion> Selecciones { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Asociacion_Deportiva> Asociacion_Deportiva { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Atleta> Atletas { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Funcionario_ICODER> Funcionario_ICODER { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Tipo_Entidad> Tipo_Entidad { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Entidad_Publica> Entidad_Publica { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Horario> Horario { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Cita> Cita { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Ejercicio> Ejercicios { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Conjunto_Ejercicio> Conjunto_Ejercicios { get; set; }

        public System.Data.Entity.DbSet<SOGIP_v2.Models.Rutina> Rutinas { get; set; }


    }
}