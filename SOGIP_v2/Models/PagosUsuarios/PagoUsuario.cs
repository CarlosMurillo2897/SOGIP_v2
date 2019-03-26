using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class PagoUsuario
    {
        public int Id { get; set; }
        public EstadosPagos  IdEsPago { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}