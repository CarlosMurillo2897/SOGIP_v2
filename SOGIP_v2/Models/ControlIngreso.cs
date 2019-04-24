using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class ControlIngreso
    {
        public int Id { get; set; }
        public ApplicationUser Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}