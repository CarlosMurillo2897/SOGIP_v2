﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public Actividad IdActividad {get; set;}
        [Required]
        public DateTime FechaHoraInicio { get; set; }
        [Required]
        public DateTime FechaHoraFinal { get; set; }
    }
}