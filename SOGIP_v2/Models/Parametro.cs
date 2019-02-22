using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Parametro
    {
        public int ParametroId { get; set; }
        [Index(IsUnique = true), MaxLength(60)]
        public string Nombre { get; set; }
        public string Valor { get; set; }
    }
}