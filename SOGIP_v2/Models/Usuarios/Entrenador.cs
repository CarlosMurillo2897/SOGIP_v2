using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Entrenador
    {
        public int EntrenadorId { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}