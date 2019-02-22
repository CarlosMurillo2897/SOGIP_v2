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
            1. Fotos de Actividades.
            2. Fotos de Usuarios.
            3. InBody.
            4. Prueba de Fuerza.
            5. CV.
            6. Ingresos Masivos.
         */
    }
}