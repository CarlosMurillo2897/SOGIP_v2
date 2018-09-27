using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Funcionario_ICODER
    {
        public int IdFuncionario_ICODER { get; set; }
        public ApplicationUser Usuario { get; set; }
        public ApplicationUser Entrenador { get; set; } //Este es el Administrador.
    }
}