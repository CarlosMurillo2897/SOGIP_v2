using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOGIP_v2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace SOGIP_v2.Controllers
{
    public class RutinasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Rutinas
        [Authorize(Roles = "Administrador,Supervisor,Entrenador")]
        public ActionResult Index()
        {
            string userid = HttpContext.User.Identity.GetUserId();
            var userRoles = UserManager.GetRoles(userid);
            ViewBag.Role = userRoles.First();
            return View();
        }

        public JsonResult GetRutinasAdministrador(string usuarioId)
        {
            var consulta = (from f in db.Rutinas.Include("Usuario")
                            from m in db.Funcionario_ICODER
                            where m.Entrenador.Id == usuarioId && f.Usuario == m.Usuario
                            select new
                            {
                                Cedula = f.Usuario.Cedula,
                                Usuario = f.Usuario.Nombre1 + " " + f.Usuario.Apellido1 + " " + f.Usuario.Apellido2,
                                Fecha = f.FechaInicio,
                                Fecha2 = f.FechaFin,
                                Objetivo = f.RutinaObservaciones,
                                Id = f.RutinaId
                            }).ToList();
    
            return Json(consulta, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetRutinasEntrenador(string usuarioId)//Aqui
        {

            var consulta = (from f in db.Rutinas.Include("Usuario")
                            from a in db.Atletas
                            from sub in db.SubSeleccion
                            from c in db.Categorias
                            where (a.SubSeleccion.Entrenador.Id == usuarioId && sub.SubSeleccionId == a.SubSeleccion.SubSeleccionId && sub.Categoria_Id.CategoriaId == c.CategoriaId && a.Usuario == f.Usuario)
                            select new
                            {
                                Cedula = f.Usuario.Cedula,
                                Usuario = f.Usuario.Nombre1 + " " + f.Usuario.Apellido1 + " " + f.Usuario.Apellido2,
                                Fecha = f.FechaInicio,
                                Fecha2 = f.FechaFin,
                                Objetivo = f.RutinaObservaciones,
                                Id = f.RutinaId
                            }).ToList();

            return Json(consulta, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetRutinasSeleccion(string usuarioId)
        {

            var consulta = (from f in db.Rutinas.Include("Usuario")
                            from a in db.Atletas
                            from sub in db.SubSeleccion
                            from c in db.Categorias
                            where (a.SubSeleccion.Entrenador.Id == usuarioId && sub.SubSeleccionId == a.SubSeleccion.SubSeleccionId && sub.Categoria_Id.CategoriaId == c.CategoriaId && a.Usuario == f.Usuario)
                            select new
                            {
                                Cedula = f.Usuario.Cedula,
                                Usuario = f.Usuario.Nombre1 + " " + f.Usuario.Apellido1 + " " + f.Usuario.Apellido2,
                                Fecha = f.FechaInicio,
                                Fecha2 = f.FechaFin,
                                Objetivo = f.RutinaObservaciones,
                                Id = f.RutinaId
                            }).ToList();

            return Json(consulta, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetRutinasAsociacion(string usuarioId)
        {

            var consulta = (from f in db.Rutinas.Include("Usuario")
                            from a in db.Atletas
                            from sub in db.SubSeleccion
                            from c in db.Categorias
                            where (a.SubSeleccion.Entrenador.Id == usuarioId && sub.SubSeleccionId == a.SubSeleccion.SubSeleccionId && sub.Categoria_Id.CategoriaId == c.CategoriaId && a.Usuario == f.Usuario)
                            select new
                            {
                                Cedula = f.Usuario.Cedula,
                                Usuario = f.Usuario.Nombre1 + " " + f.Usuario.Apellido1 + " " + f.Usuario.Apellido2,
                                Fecha = f.FechaInicio,
                                Fecha2 = f.FechaFin,
                                Objetivo = f.RutinaObservaciones,
                                Id = f.RutinaId
                            }).ToList();

            return Json(consulta, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAtletasAdministrador(string usuarioId)
        {
            var consulta = (from f in db.Funcionario_ICODER
                            where f.Entrenador.Id == usuarioId
                            select new
                            {
                                Accion ="",
                                Cedula = f.Usuario.Cedula,
                                Nombre = f.Usuario.Nombre1 + " " + f.Usuario.Nombre2,
                                Apellido1 = f.Usuario.Apellido1,
                                Apellido2 = f.Usuario.Apellido2
                            }).ToList();
            var aux = consulta;
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsuariosEntrenador(string usuarioId)
        {
            var consulta = from a in db.Atletas
                           from sub in db.SubSeleccion
                           from c in db.Categorias
                           where (a.SubSeleccion.Entrenador.Id == usuarioId && sub.SubSeleccionId == a.SubSeleccion.SubSeleccionId && sub.Categoria_Id.CategoriaId == c.CategoriaId)
                           select new
                           {
                               Accion = "",
                               Cedula = a.Usuario.Cedula,
                               Nombre = a.Usuario.Nombre1 + " " + a.Usuario.Nombre2,
                               Apellido1 = a.Usuario.Apellido1,
                               Apellido2 = a.Usuario.Apellido2
                           };
            var aux = consulta.ToList();
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsuariosSeleccion(string usuarioId)
        {
            var consulta = from a in db.Atletas
                           from sub in db.SubSeleccion
                           from c in db.Categorias
                           where (sub.Seleccion.Usuario.Id == usuarioId && a.SubSeleccion.SubSeleccionId == sub.SubSeleccionId && sub.Categoria_Id.CategoriaId == c.CategoriaId)
                           select new
                           {
                               Accion = "",
                               Cedula = a.Usuario.Cedula,
                               Nombre = a.Usuario.Nombre1 + " " + a.Usuario.Nombre2,
                               Apellido1 = a.Usuario.Apellido1,
                               Apellido2 = a.Usuario.Apellido2
                           };

            var traineers = from sub in db.SubSeleccion
                            from u in db.Users
                            from c in db.Categorias
                            where
                            sub.Seleccion.Usuario.Id == usuarioId
                            && sub.Entrenador != null
                            && sub.Entrenador.Id == u.Id
                            && sub.Categoria_Id.CategoriaId == c.CategoriaId
                            select new
                            {
                                Accion = "",
                                Cedula = u.Cedula,
                                Nombre = u.Nombre1 + " " + u.Nombre2,
                                Apellido1 = u.Apellido1,
                                Apellido2 = u.Apellido2
                            };

            var list = Enumerable.Union(consulta, traineers).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAtletasAsociacion(string usuarioId)
        {
            var consulta = (from a in db.Atletas
                            where a.Asociacion_Deportiva.Usuario.Id == usuarioId
                            select new
                            {
                                Accion = "",
                                Cedula = a.Usuario.Cedula,
                                Nombre = a.Usuario.Nombre1 + " " + a.Usuario.Nombre2,
                                Apellido1 = a.Usuario.Apellido1,
                                Apellido2 = a.Usuario.Apellido2
                            }).ToList();
            var aux = consulta;
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

       public JsonResult SaveRutina(DateTime fecha, DateTime fecha2, string obs, string id)
        {
            Rutina nueva = new Rutina();
            try
            {
                ApplicationUser User = db.Users.Single(x => x.Cedula == id);
                if (User != null)
                {
                    nueva.Usuario = User;
                    nueva.FechaInicio = fecha;
                    nueva.FechaFin = fecha2;
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

        public JsonResult EditRutina(DateTime fecha, DateTime fecha2, string obs, string idUs, int id)
        {
            Rutina rutina = db.Rutinas.Single(x => x.RutinaId == id);
            ApplicationUser User = db.Users.Single(x => x.Cedula == idUs);
            try
            {
                if (rutina != null)
                {
                    rutina.FechaInicio = fecha;
                    rutina.FechaFin = fecha2;
                    rutina.RutinaObservaciones = obs;
                    rutina.Usuario = User;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(rutina, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtnerRutina(int id)
        {
            var consulta = db.Rutinas.Include("Usuario").Where(x => x.RutinaId == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
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

                var getEjercicio1 = db.Conjunto_Ejercicios.Include("EjercicioId").Include("ColorId")
                    .Where(x => x.ConjuntoEjercicioRutina.RutinaId == idRutina 
                    && (x.DiaEjercicio == "Dia1" 
                    || x.DiaEjercicio == "Dia2" 
                    || x.DiaEjercicio == "Dia3" 
                    || x.DiaEjercicio == "Dia4" 
                    || x.DiaEjercicio == "Dia5"))
                    .OrderBy(x => x.DiaEjercicio).ToList();


                ViewBag.Conjunto_Ejercicios1 = (getEjercicio1.Count > 0) ? getEjercicio1 : null;

            }

            return View();
        }
        public JsonResult ObtenerColores()
        {
            var consulta = db.Colores;
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtnerEjer(string id)
        {
            var consulta = db.Ejercicio.Where(x => x.Nombre == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtnerColor(int id)
        {
            var consulta = db.Colores.Where(x => x.ColorId == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEjercicio(string n, int ejercicioId)
        {
            int d = int.Parse(n);
            Rutina rutina = new Rutina();
            rutina = db.Rutinas.Single(x => x.RutinaId == d);
            var v = db.Conjunto_Ejercicios.Where(a => a.Conjunto_EjercicioId == ejercicioId).FirstOrDefault();
            if (v != null)
            {

                db.Conjunto_Ejercicios.Remove(v);
                db.SaveChanges();
        
            }
            var getEjercicio1 = db.Conjunto_Ejercicios.Include("EjercicioId").Include("ColorId")
                .Where(x => x.ConjuntoEjercicioRutina.RutinaId == rutina.RutinaId &&
                (x.DiaEjercicio == "Dia1"
                || x.DiaEjercicio == "Dia2"
                || x.DiaEjercicio == "Dia3"
                || x.DiaEjercicio == "Dia4"
                || x.DiaEjercicio == "Dia5"))
                .OrderBy(x => x.DiaEjercicio)
                .ToList();
            return Json(getEjercicio1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerEjer(int ejercicioId)
        {
            var ejercicio = db.Conjunto_Ejercicios.Include("EjercicioId").Include("ColorId").Where(a => a.Conjunto_EjercicioId == ejercicioId).FirstOrDefault();
            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditEjer(string n,Conjunto_Ejercicio data)
        {
            int d = int.Parse(n);
            Rutina rutina = new Rutina();
            rutina = db.Rutinas.Single(x => x.RutinaId == d);

            Conjunto_Ejercicio ejer = db.Conjunto_Ejercicios.Where(a => a.Conjunto_EjercicioId == data.Conjunto_EjercicioId).FirstOrDefault();
            if (ejer != null)
            {
                ejer.EjercicioId  = db.Ejercicio.Where(x => x.Nombre == data.EjercicioId.Nombre).FirstOrDefault();
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
                ejer.ColorId = db.Colores.Where(x => x.Nombre == data.ColorId.Nombre).FirstOrDefault();
                db.SaveChanges();
            }
            var getEjercicio1 = db.Conjunto_Ejercicios.Include("EjercicioId").Include("ColorId")
                 .Where(x => x.ConjuntoEjercicioRutina.RutinaId == rutina.RutinaId &&
                 (x.DiaEjercicio == "Dia1"
                 || x.DiaEjercicio == "Dia2"
                 || x.DiaEjercicio == "Dia3"
                 || x.DiaEjercicio == "Dia4"
                 || x.DiaEjercicio == "Dia5"))
                 .OrderBy(x => x.DiaEjercicio)
                 .ToList();
            return Json(getEjercicio1, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Ejercicio(string data, List<Conjunto_Ejercicio> ejercicios) //AGREGAR EL ID DE LA RUTINA
        {
            //Busco el id de la rutina.
            //int d = 5;
            int d = int.Parse(data);
            Rutina rutina = new Rutina();
            Ejercicio ejercicio = new Ejercicio();
            Color color = new Color();
            rutina = db.Rutinas.Single(x => x.RutinaId == d);
           
            //Asigno ejercicios a la rutina
            if (rutina != null)
            {
                for (int i = 0; i < ejercicios.Count; i++)
                {
                    String n = ejercicios[i].EjercicioId.Nombre;
                    String n1 = ejercicios[i].ColorId.Nombre;
                    ejercicio = db.Ejercicio.Where(x => x.Nombre == n).FirstOrDefault();
                    color = db.Colores.Where(x => x.Nombre == n1).FirstOrDefault();
                    Conjunto_Ejercicio conjunto = new Conjunto_Ejercicio()
                    {
                        ConjuntoEjercicioRutina = rutina,
                        EjercicioId = ejercicio,
                        Serie1 = ejercicios[i].Serie1,
                        Repeticion1 = ejercicios[i].Repeticion1,
                        Peso1 = ejercicios[i].Peso1,
                        Serie2 = ejercicios[i].Serie2,
                        Repeticion2 = ejercicios[i].Repeticion2,
                        Peso2 = ejercicios[i].Peso2,
                        Serie3 = ejercicios[i].Serie3,
                        Repeticion3 = ejercicios[i].Repeticion3,
                        Peso3 = ejercicios[i].Peso3,
                        ColorId = color,
                        DiaEjercicio = ejercicios[i].DiaEjercicio
                    };
                    db.Conjunto_Ejercicios.Add(conjunto);
                }
                db.SaveChanges();
            }
            var getEjercicio1 = db.Conjunto_Ejercicios.Include("EjercicioId").Include("ColorId")
                  .Where(x => x.ConjuntoEjercicioRutina.RutinaId == rutina.RutinaId &&
                  (x.DiaEjercicio == "Dia1"
                  || x.DiaEjercicio == "Dia2"
                  || x.DiaEjercicio == "Dia3"
                  || x.DiaEjercicio == "Dia4"
                  || x.DiaEjercicio == "Dia5"))
                  .OrderBy(x => x.DiaEjercicio)
                  .ToList();
            return Json(getEjercicio1,JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNombreEjer(int id)
        {
            var ejercicio = db.Conjunto_Ejercicios.Include("EjercicioId").Include("ColorId").Where(a => a.Conjunto_EjercicioId == id).FirstOrDefault();
            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador,Supervisor,Entrenador,Atleta,Atleta Becados,Funcionarios ICODER")]
        public ActionResult ListaEjercicio(int? id, string idUsuario)
        {
            if (id != null)
            {
                Rutina rutina = db.Rutinas.Include("Usuario").Where(x => x.RutinaId == id).FirstOrDefault();
                int i = rutina.RutinaId;
                string n = i.ToString();
                ViewData["rutina"] = n;
                ViewBag.Usuario = rutina.Usuario.Cedula + " - " + rutina.Usuario.Nombre1 + " " + rutina.Usuario.Nombre2 + " " + rutina.Usuario.Apellido1 + " " + rutina.Usuario.Apellido2;
                if (rutina != null)
                {
                    var getEjercicio1 = db.Conjunto_Ejercicios.Include("EjercicioId").Include("ColorId")
                        .Where(x => x.ConjuntoEjercicioRutina.RutinaId == id &&
                        (x.DiaEjercicio == "Dia1"
                        || x.DiaEjercicio == "Dia2"
                        || x.DiaEjercicio == "Dia3"
                        || x.DiaEjercicio == "Dia4"
                        || x.DiaEjercicio == "Dia5"))
                        .OrderBy(x => x.DiaEjercicio)
                        .ToList();

                    ViewBag.Conjunto_Ejercicios1 = (getEjercicio1.Count > 0) ? getEjercicio1 : null;
                }else
                {
                    string men = "No tiene rutina";
                    ViewData["mensaje"] = men;
                }
            }

            if (idUsuario != null)
            {

                Rutina rutina = db.Rutinas
                .Include("Usuario") // Traemos al usuario.
                .Where(x => x.Usuario.Id == idUsuario) // Buscamos al usuario por medio de su Id.
                .ToList() // Traemos *TODAS* las rutinas.
                .OrderByDescending(r => r.FechaInicio) // Las ordenamos por fecha, de modo que la más reciente queda de primera.
                .FirstOrDefault(); // Tomamos la primera y con esto obtenemos la más reciente, en caso de no existir rutina simplemente se ve ignorado y dispone como nulo.

                ViewBag.idUsuario = idUsuario;

                if (rutina != null)
                {
                    ViewBag.Usuario = rutina.Usuario.Cedula + " - " + rutina.Usuario.Nombre1 + " " + rutina.Usuario.Nombre2 + " " + rutina.Usuario.Apellido1 + " " + rutina.Usuario.Apellido2;
                    int i = rutina.RutinaId;
                    string n = i.ToString();
                    ViewData["rutina"] = n;
                    ViewData["observaciones"] = rutina.RutinaObservaciones;

                    var getEjercicio1 = db.Conjunto_Ejercicios.Include("EjercicioId").Include("ColorId")
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

        public JsonResult obtenerRutinasUsuario(string id)
        {
            var rutinas = (from r in db.Rutinas
                          where r.Usuario.Id == id
                          select new
                          {
                              r.RutinaId,
                              r.FechaInicio,
                              r.RutinaObservaciones,
                              Usuario = r.Usuario.Cedula + " - " + r.Usuario.Nombre1 + " " + r.Usuario.Nombre2 + " " + r.Usuario.Apellido1 + " " + r.Usuario.Apellido2
                            }).ToList();
            return Json(rutinas, JsonRequestBehavior.AllowGet);
        }
    }
}