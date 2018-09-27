using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Deporte
    {
        public int IdDeporte { get; set; }
        public string Nombre { get; set; }
        public Tipo_Deporte TipoDeporte { get; set; }
    }
}