using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOGIP_v2.Models;
using System.Text.RegularExpressions;

namespace SOGIP_v2.Controllers
{
    public class RutinasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rutinas
        [Authorize(Roles = "Administrador,Supervisor,Entrenador")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRutinas()
        {
            var Rutinas = db.Rutinas.Include("Usuario").ToList();
            return Json(Rutinas, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetUsuarios()
        {

            var consulta1 = from f in db.Funcionario_ICODER
                            from u in db.Users.Where(u => u.Id == f.Usuario.Id)
                            select new
                            {
                                Accion = "",
                                Cedula = u.Cedula,
                                Nombre = u.Nombre1,
                                Apellido1 = u.Apellido1,
                                Apellido2 = u.Apellido2,
                                Rol = "Funcionario"
                            };

            var consulta = from a in db.Atletas
                           from u in db.Users.Where(u => u.Id == a.Usuario.Id)
                           select new
                           {
                               Accion = "",
                               Cedula = u.Cedula,
                               Nombre = u.Nombre1,
                               Apellido1 = u.Apellido1,
                               Apellido2 = u.Apellido2,
                               Rol = "Atleta"
                           };

            var enume = Enumerable.Union(consulta1, consulta);
            var usuarios = enume.ToList();
            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveRutina(DateTime fecha, string obs, string id)
        {
            Rutina nueva = new Rutina();
            try
            {
                ApplicationUser User = db.Users.Single(x => x.Cedula == id);
                if (User != null)
                {
                    nueva.Usuario = User;
                    nueva.RutinaFecha = fecha;
                    nueva.RutinaObservaciones = obs;
                    db.Rutinas.Add(nueva);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(nueva, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditRutina(DateTime fecha, string obs, int id)
        {
            Rutina rutina = db.Rutinas.Single(x => x.RutinaId == id);
            try
            {
                if (rutina != null)
                {
                    rutina.RutinaFecha = fecha;
                    rutina.RutinaObservaciones = obs;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(rutina, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRutina(int rutinaId)
        {
            var status = false;
            var v = db.Rutinas.Where(a => a.RutinaId == rutinaId).FirstOrDefault();
            if (v != null)
            {
                var getEjercicio = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == rutinaId).ToList();
                foreach (var n in getEjercicio)
                {
                    int i = n.Conjunto_EjercicioId;
                    Conjunto_Ejercicio conjunto = db.Conjunto_Ejercicios.Find(i);
                    db.Conjunto_Ejercicios.Remove(conjunto);
                }
                db.Rutinas.Remove(v);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public JsonResult GetEjercicio(string dia)
        {

            int d = int.Parse(dia);
            Rutina rutina = db.Rutinas.SingleOrDefault(x => x.RutinaId == d);
            //var Ejercicio = db.Conjunto_Ejercicios.Where(x=> x.ConjuntoEjercicioRutina.RutinaId == rutina.RutinaId).ToList();
            var getEjercicio = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == rutina.RutinaId).ToList();
            //var Ejercicio = getEjercicio.Where(x=>x.DiaEjercicio == "Dia1").ToList();
            return Json(getEjercicio, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador,Supervisor,Entrenador")]
        public ActionResult Ejercicio(int? idRutina)
        {
            Rutina rutina = db.Rutinas.Include("Usuario").SingleOrDefault(x => x.RutinaId == idRutina);
            if (idRutina != null)
            {
                int i = rutina.RutinaId;
                string n = i.ToString();
                ViewData["rutina"] = n;
                string nombre = rutina.Usuario.Cedula + " - " + rutina.Usuario.Nombre1 + " " + rutina.Usuario.Apellido1 + " " + rutina.Usuario.Apellido2;
                ViewData["nombre"] = nombre;

                var getEjercicio1 = db.Conjunto_Ejercicios
                    .Where(x => x.ConjuntoEjercicioRutina.RutinaId == idRutina 
                    && (x.DiaEjercicio == "Dia1" 
                    || x.DiaEjercicio == "Dia2" 
                    || x.DiaEjercicio == "Dia3" 
                    || x.DiaEjercicio == "Dia4" 
                    || x.DiaEjercicio == "Dia5"))
                    .OrderBy(x => x.DiaEjercicio)
                    .ToList();

                ViewBag.Conjunto_Ejercicios1 = (getEjercicio1.Count > 0) ? getEjercicio1 : null;

            }

            return View();
        }

        public JsonResult DeleteEjercicio(int ejercicioId)
        {
            var status = false;
            var v = db.Conjunto_Ejercicios.Where(a => a.Conjunto_EjercicioId == ejercicioId).FirstOrDefault();
            if (v != null)
            {

                db.Conjunto_Ejercicios.Remove(v);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public JsonResult ObtenerEjer(int ejercicioId)
        {
            var ejercicio = db.Conjunto_Ejercicios.Where(a => a.Conjunto_EjercicioId == ejercicioId).FirstOrDefault();
            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditEjer(Conjunto_Ejercicio data)
        {
            var status = false;
            Conjunto_Ejercicio ejer = db.Conjunto_Ejercicios.Where(a => a.Conjunto_EjercicioId == data.Conjunto_EjercicioId).FirstOrDefault();
            if (ejer != null)
            {
                ejer.NombreEjercicio = data.NombreEjercicio;
                ejer.Serie1 = data.Serie1;
                ejer.Repeticion1 = data.Repeticion1;
                ejer.Peso1 = data.Peso1;
                ejer.Serie2 = data.Serie2;
                ejer.Repeticion2 = data.Repeticion2;
                ejer.Peso2 = data.Peso2;
                ejer.Serie3 = data.Serie3;
                ejer.Repeticion3 = data.Repeticion3;
                ejer.Peso3 = data.Peso3;
                ejer.DiaEjercicio = data.DiaEjercicio;
                ejer.ColorEjercicio = data.ColorEjercicio;
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult Ejercicio(string data, List<Conjunto_Ejercicio> ejercicios) //AGREGAR EL ID DE LA RUTINA
        {
            var status = false;
            //Busco el id de la rutina.
            //int d = 5;
            int d = int.Parse(data);
            Rutina rutina = new Rutina();
            rutina = db.Rutinas.Single(x => x.RutinaId == d);
            //Asigno ejercicios a la rutina
            if (rutina != null)
            {
                for (int i = 0; i < ejercicios.Count; i++)
                {
                    Conjunto_Ejercicio conjunto = new Conjunto_Ejercicio()
                    {
                        ConjuntoEjercicioRutina = rutina,
                        NombreEjercicio = ejercicios[i].NombreEjercicio,
                        Serie1 = ejercicios[i].Serie1,
                        Repeticion1 = ejercicios[i].Repeticion1,
                        Peso1 = ejercicios[i].Peso1,
                        Serie2 = ejercicios[i].Serie2,
                        Repeticion2 = ejercicios[i].Repeticion2,
                        Peso2 = ejercicios[i].Peso2,
                        Serie3 = ejercicios[i].Serie3,
                        Repeticion3 = ejercicios[i].Repeticion3,
                        Peso3 = ejercicios[i].Peso3,
                        ColorEjercicio = ejercicios[i].ColorEjercicio,
                        DiaEjercicio = ejercicios[i].DiaEjercicio
                    };
                    db.Conjunto_Ejercicios.Add(conjunto);
                }
                db.SaveChanges();
            }
            return new JsonResult { Data = new { status = status } };
        }

        [Authorize(Roles = "Administrador,Supervisor,Entrenador,Atleta,Atleta Becados,Funcionarios ICODER")]
        public ActionResult ListaEjercicio(int? id, string idUsuario)
        {
            if (id != null)
            {
                Rutina rutina = db.Rutinas.Find(id);
                int i = rutina.RutinaId;
                string n = i.ToString();
                ViewData["rutina"] = n;

                var getEjercicio1 = db.Conjunto_Ejercicios
                    .Where(x => x.ConjuntoEjercicioRutina.RutinaId == id &&
                    (x.DiaEjercicio == "Dia1" 
                    || x.DiaEjercicio == "Dia2" 
                    || x.DiaEjercicio == "Dia3" 
                    || x.DiaEjercicio == "Dia4" 
                    || x.DiaEjercicio == "Dia5"))
                    .OrderBy(x => x.DiaEjercicio)
                    .ToList();

                ViewBag.Conjunto_Ejercicios1 = (getEjercicio1.Count > 0) ? getEjercicio1 : null;
            }

            if (idUsuario != null)
            {
                Rutina rutina = db.Rutinas.Include("Usuario").FirstOrDefault(x => x.Usuario.Id == idUsuario);
                ViewBag.idUsuario = idUsuario;

                if (rutina != null)
                {
                    ViewBag.Usuario = rutina.Usuario.Cedula + " - " + rutina.Usuario.Nombre1 + " " + rutina.Usuario.Nombre2 + " " + rutina.Usuario.Apellido1 + " " + rutina.Usuario.Apellido2;
                    int i = rutina.RutinaId;
                    string n = i.ToString();
                    ViewData["rutina"] = n;

                    var getEjercicio1 = db.Conjunto_Ejercicios
                        .Where(x => x.ConjuntoEjercicioRutina.RutinaId == i &&
                        (x.DiaEjercicio == "Dia1" 
                        || x.DiaEjercicio == "Dia2" 
                        || x.DiaEjercicio == "Dia3" 
                        || x.DiaEjercicio == "Dia4" 
                        || x.DiaEjercicio == "Dia5")).ToList();

                    ViewBag.Conjunto_Ejercicios1 = (getEjercicio1.Count > 0) ? getEjercicio1 : null;
                }
                else
                {
                    string men = "No tiene rutina";
                    ViewData["mensaje"] = men;
                }
            }
            return View();
        }
    }
}