using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Seleccion
    {
        public int IdSeleccion { get; set; }
        public string Nombre_Seleccion { get; set; }
        public ApplicationUser Usuario { get; set; }
        public Deporte Deporte { get; set; }
        public Categoria Categoria { get; set; }
        public Entrenador Entrenador { get; set; }
    }
}