using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Reservacion
    {
        public int ReservacionId { get; set; }
        public Estado Estado { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFinal { get; set; }
        public ApplicationUser UsuarioId { get; set; }
    }
}