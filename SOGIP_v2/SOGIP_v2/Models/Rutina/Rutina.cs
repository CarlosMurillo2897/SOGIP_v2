using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Rutina
    {
        public int RutinaId { get; set; }
        public ApplicationUser Usuario { get; set; }
        public DateTime RutinaFecha { get; set; }
        public string RutinaObservaciones { get; set; }
    }
}