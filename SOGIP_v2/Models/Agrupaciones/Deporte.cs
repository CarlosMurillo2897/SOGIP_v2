using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Deporte
    {
        public int DeporteId { get; set; }
        [Index(IsUnique = true), MaxLength(60)]
        public string Nombre { get; set; }
        public Tipo_Deporte TipoDeporte { get; set; }
    }
}