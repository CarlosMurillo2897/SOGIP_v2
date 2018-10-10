using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace SOGIP_v2.Models
{
    public class Ejercicio
    {
        public int EjercicioId { get; set; }
        public string EjercicioNombre { get; set; }
        public string EjercicioDescripcion { get; set; }
        public Ejercicio TipoEjercicio { get; set; }
    }
}