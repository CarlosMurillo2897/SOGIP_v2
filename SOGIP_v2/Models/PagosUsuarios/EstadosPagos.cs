using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SOGIP_v2.Models
{
    public class EstadosPagos
    {
        [Key]
        public int IdEsPago { get; set; }
        public ApplicationUser Usuario { get; set; }
        public DateTime FechaPago { get; set; }
        public Boolean estado { get; set; }
    }
}