using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Entidad_Publica
    {
        public int IdEntidad_Publica { get; set; }
        public string NombreEntidad_Publica { get; set; }
        public ApplicationUser Usuario { get; set; }
        public Tipo_Entidad Tipo_Entidad { get; set; }
    }
}