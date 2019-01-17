using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        [Index(IsUnique = true), MaxLength(60)]
        public string Nombre { get; set; }
        [Index(IsUnique = true), MaxLength(60)]
        public string Codigo { get; set; }
    }
}