using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public Boolean InBody { get; set; }
        public Boolean Otro { get; set; }
        public ApplicationUser UsuarioId_Id { get; set; }
        public string UsuarioCedula { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido1 { get; set; }
        public string UsuarioApellido2 { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFinal { get; set; }
        // public Horario HorarioId { get; set; }
    }
}