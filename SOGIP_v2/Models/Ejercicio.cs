﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Ejercicio
    {
        public int Id { get; set; }
        public TipoME TipoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}