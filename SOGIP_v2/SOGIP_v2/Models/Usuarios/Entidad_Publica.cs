﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Entidad_Publica
    {
        public int Entidad_PublicaId { get; set; }
        public ApplicationUser Usuario { get; set; }
        public Tipo_Entidad Tipo_Entidad { get; set; }
    }
}