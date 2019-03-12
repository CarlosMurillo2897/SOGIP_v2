using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Atleta
    {
        public int AtletaId { get; set; }
        public ApplicationUser Usuario { get; set; }
        public SubSeleccion SubSeleccion { get; set; }
        public Asociacion_Deportiva Asociacion_Deportiva { get; set; }
    }
}