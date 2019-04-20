using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace SOGIP_v2.Models
{
    public class ListaPagos
    {
        [Key]
        public int Id { get; set; }
        public EstadosPagos IdEsPago { get; set; }
        public DateTime Fecha { get; set; }
    }
}