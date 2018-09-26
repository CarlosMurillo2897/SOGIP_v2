using System;
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
<<<<<<< HEAD
=======
        public Boolean Sexo { get; set; }
>>>>>>> IngresoMasivo_Carlos

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
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}