using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class Conjunto_Ejercicio
    {
        public int Conjunto_EjercicioId { get; set; }
        public string NombreEjercicio { get; set; }
        public int Serie1 { get; set; }
        public int Repeticion1 { get; set; }
        public int Peso1 { get; set; }
        public int Serie2 { get; set; }
        public int Repeticion2 { get; set; }
        public int Peso2 { get; set; }
        public int Serie3 { get; set; }
        public int Repeticion3 { get; set; }
        public int Peso3 { get; set; }
        public Rutina ConjuntoEjercicioRutina { get; set; }
        public string ColorEjercicio { get; set; }
        public string diaEjercicio { get; set; }

    }
}