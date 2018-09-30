using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Funcionario_ICODER
    {
        public int Funcionario_ICODERId { get; set; }
        public string Usuario_Id { get; set; }
        public ApplicationUser Entrenador { get; set; } //Este es el Administrador.
    }
}