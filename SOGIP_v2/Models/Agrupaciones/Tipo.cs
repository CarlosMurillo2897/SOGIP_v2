using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models.Agrupaciones
{
    public class Tipo
    {
        public int TipoId { get; set; }
        [Index(IsUnique = true), MaxLength(60)]
        public string Nombre { get; set; }
        /*
         Tipos de Archivos (Esto agilizará las búsquedas y recolección total de estos datos):
            1. Fotos de usuarios.
            2. InBody.
            3. Prueba de Fuerza.
            4. PDF.
            5. Fotos de Actividades.
            6. ¿?
         */
    }
}