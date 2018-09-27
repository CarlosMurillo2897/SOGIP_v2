using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Entrenador
    {
        public int IdEntrenador { get; set; }
        //public varBinary(MAX) titulo { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}