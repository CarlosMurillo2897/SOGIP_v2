using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Atleta
    {
        public int IdAtleta { get; set; }
        public string Localidad { get; set; }
        public ApplicationUser Usuario { get; set; }
        public Seleccion Seleccion { get; set; }
        public Asociacion_Deportiva Asociacion_Deportiva { get; set; }
    }
}