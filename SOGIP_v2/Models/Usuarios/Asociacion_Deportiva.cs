﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Asociacion_Deportiva
    {
        public int Asociacion_DeportivaId { get; set; }
        public string Localidad { get; set; }
        public ApplicationUser Usuario { get; set; }
        public string Nombre_DepAso { get; set; }
    }
}