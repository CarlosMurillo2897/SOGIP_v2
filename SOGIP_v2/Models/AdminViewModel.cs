using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SOGIP_v2.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Cedula")]
        public string Cedula { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Cedula extra")]
        public string CedulaExtra { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Primer Nombre")]
        public string Nombre1 { get; set; }

        [Display(Name = "Segundo Nombre")]
        public string Nombre2 { get; set; }

        [Required]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }

        // Los asiáticos normalmente solo tienen un apellido, mejor que no sea requerido.
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime Fecha_Nacimiento { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public bool Sexo { get; set; }

        // public IEnumerable<SelectListItem> RolesList { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

    }

}