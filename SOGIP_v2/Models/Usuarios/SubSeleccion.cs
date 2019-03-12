using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class SubSeleccion
    {
        public int SubSeleccionId { get; set; }
        public Seleccion Seleccion { get; set; }
        public ApplicationUser Entrenador { get; set; }
        public Categoria Categoria_Id { get; set; }
    }
}