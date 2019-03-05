using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class MaquinaEjercicio
    {
        public int Id { get; set; }
        public Maquina Maquina { get; set; }
        public Ejercicio Ejercicio { get; set; }
    }
}