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
        public string Serie1 { get; set; }
        public string Repeticion1 { get; set; }
        public string Peso1 { get; set; }
        public string Serie2 { get; set; }
        public string Repeticion2 { get; set; }
        public string Peso2 { get; set; }
        public string Serie3 { get; set; }
        public string Repeticion3 { get; set; }
        public string Peso3 { get; set; }
        public Rutina ConjuntoEjercicioRutina { get; set; }
        public string ColorEjercicio { get; set; }
        public string diaEjercicio { get; set; }

    }
}