using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace SOGIP_v2.Models
{
    public class TotalPagos
    {
        [Key]
        public int IdPagos { get; set; }
        public EstadosPagos IdEst { get; set; }
        public DateTime fecha { get; set; }
    }
}