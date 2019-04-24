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
        public int Id { get; set; }
        public ApplicationUser Usuario { get; set; }
        public DateTime FechaPago { get; set; }
        public int Cantidad { get; set; }
        public float Monto { get; set; }
        public string Concepto { get; set; }
        public Estado Estado { get; set; }
        public TipoPago TipoPago { get; set; }
    }
}