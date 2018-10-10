using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Conjunto_Ejercicio
    {
        public int Conjunto_EjercicioId { get; set; }
        public Ejercicio ConjuntoEjercicios { get; set; }
        public int ConjuntoEjercicioSerie { get; set; }
        public int ConjuntoEjercicioRepeticion { get; set; }
        public int ConjuntoEjercicioPeso { get; set; }
    }
}