﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [Index(IsUnique = true), MaxLength(60)]
        public string Descripcion { get; set; }
    }
}