using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Tipo_Deporte
    {
        public int Tipo_DeporteId { get; set; }
        [Index(IsUnique = true), MaxLength(100)]
        public string Descripcion { get; set; }
    }
}