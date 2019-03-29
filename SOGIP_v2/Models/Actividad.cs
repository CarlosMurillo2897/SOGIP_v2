using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Actividad
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public string Lugar { get; set; }
    }
}