using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Archivo
    {
        public int ArchivoId { get; set; }
        public string Nombre { get; set; }
        public byte[] Contenido { get; set; }
        public string Tipo { get; set; }
        public string Extension { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}