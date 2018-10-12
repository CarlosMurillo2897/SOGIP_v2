using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Seleccion
    {
        public int SeleccionId { get; set; }
        public string Nombre_Seleccion { get; set; }
        public ApplicationUser Usuario { get; set; }
        public Deporte Deporte_Id { get; set; }
        public Categoria Categoria_Id { get; set; }
        public Entrenador Entrenador_Id { get; set; }
    }
}