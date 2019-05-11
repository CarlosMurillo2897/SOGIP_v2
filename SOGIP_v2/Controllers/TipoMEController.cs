using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOGIP_v2.Models;

namespace SOGIP_v2.Controllers
{
    public class TipoMEController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rutinas
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveCategoria(string categoria)
        {
            TipoME nueva = new TipoME();
            try
            {
                if (categoria != null)
                {
                    nueva.nombre = categoria;
                    db.TipoME.Add(nueva);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(nueva, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveCategoria1(int id, string categoria)
        {
            TipoME nueva = new TipoME();
            try
            {
                TipoME n = db.TipoME.Single(x => x.Id == id);
                if (categoria != null && n != null)
                {
                    nueva.TipoId = id;
                    nueva.nombre = categoria;
                    db.TipoME.Add(nueva);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(nueva, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCategorias()
        {
            var consulta = from t in db.TipoME.Where(x => x.TipoId == 0)
                           select new
                           {
                               t.nombre,
                               t.Id
                           };

            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNombreCat(string nombre)
        {
            var consulta = db.TipoME.Where(x => x.nombre == nombre).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerEjer(int n)
        {

            var consulta = from t in db.TipoME.Where(x => x.TipoId == n)
                           select new
                           {
                               t.nombre,
                               t.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult TipoRepetido(string nombre)
        {
            return Json(!db.TipoME.Any(x => x.nombre == nombre), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNombre(int id)
        {
            var consulta = db.TipoME.Where(x => x.Id == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditCategoria(int id, string categoria)
        {
            TipoME tipo = db.TipoME.Single(x => x.Id == id);
            try
            {
                if (tipo != null)
                {
                    tipo.nombre = categoria;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(tipo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditMaquina(int id, string categoria)
        {
            Maquina maquina = db.Maquina.Single(x => x.Id == id);
            try
            {
                if (maquina != null)
                {
                    maquina.Nombre = categoria;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(maquina, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditEjercicio(int id, string categoria)
        {
            Ejercicio ejercicio = db.Ejercicio.Single(x => x.Id == id);
            try
            {
                if (ejercicio != null)
                {
                    ejercicio.Nombre = categoria;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }
        //Nuevo 
        public JsonResult Maquinas(int id)
        {
            var consulta = from t in db.Maquina.Where(x => x.TipoId.Id == id)
                           select new
                           {
                               t.Nombre,
                               t.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);

        }
        public JsonResult EjerciciosList(int id)
        {
            var consulta = from t in db.Ejercicio.Where(x => x.TipoId.Id == id)
                           select new
                           {
                               t.Nombre,
                               t.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);

        }
        public JsonResult SaveMaquina(int id, string nombre)
        {
            TipoME tipo = db.TipoME.Single(x => x.Id == id);
            Maquina nueva = new Maquina();
            try
            {
                if (nombre != null)
                {
                    nueva.Nombre = nombre;
                    nueva.TipoId = tipo;
                    db.Maquina.Add(nueva);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(nueva, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveEjercicio(int id, string nombre)
        {
            TipoME tipo = db.TipoME.Single(x => x.Id == id);
            Ejercicio nueva = new Ejercicio();
            try
            {
                if (nombre != null)
                {
                    nueva.Nombre = nombre;
                    nueva.TipoId = tipo;
                    db.Ejercicio.Add(nueva);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(nueva, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EjercicioRepetido(string nombre)
        {
            return Json(!db.Ejercicio.Any(x => x.Nombre == nombre), JsonRequestBehavior.AllowGet);
        }
        public JsonResult MaquinaRepetido(string nombre)
        {
            return Json(!db.Maquina.Any(x => x.Nombre == nombre), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNombreEjer(int id)
        {
            var consulta = db.Ejercicio.Where(x => x.Id == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNombreMaq(int id)
        {
            var consulta = db.Maquina.Where(x => x.Id == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Ejercicios(int? id)
        {
            Maquina maquina = db.Maquina.SingleOrDefault(x => x.Id == id);
            if (maquina != null)
            {
                int i = maquina.Id;
                string n = i.ToString();
                ViewData["maquina"] = n;
                string nombre = maquina.Nombre;
                ViewData["nombre"] = nombre;

            }

            return View();
        }
        public JsonResult getEjercicios(string id)
        {
            int d = int.Parse(id);
            var consulta = from t in db.MaquinaEjercicio.Where(x => x.Maquina.Id == d)
                           select new
                           {
                               Nombre = t.Ejercicio.Nombre,
                               Id = t.Ejercicio.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEjerMaquina()
        {
            var data = from a in db.Ejercicio
                       select new
                       {
                           Accion = "",
                           Nombre = a.Nombre,
                           Id = a.Id

                       };
            var ejercicio = data.ToList();
            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMaquinaEjercicio(string nom, int ejer)
        {
            int d = int.Parse(nom);
            Maquina maquina = db.Maquina.SingleOrDefault(x => x.Id == d);
            Ejercicio ejercicio = db.Ejercicio.SingleOrDefault(x => x.Id == ejer);
            MaquinaEjercicio maejer = db.MaquinaEjercicio.FirstOrDefault(x => x.Maquina.Id == maquina.Id && x.Ejercicio.Id == ejercicio.Id);

            db.MaquinaEjercicio.Remove(maejer);
            db.SaveChanges();
            return Json(maquina, JsonRequestBehavior.AllowGet);

        }
        public JsonResult obtenerIdEjer(string n)
        {
            var consulta = db.Ejercicio.Where(x => x.Nombre == n).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveMaquinaEjercicio(string nom, int ejer)
        {
            int d = int.Parse(nom);
            Maquina maquina = db.Maquina.SingleOrDefault(x => x.Id == d);
            Ejercicio ejercicio = db.Ejercicio.SingleOrDefault(x => x.Id == ejer);
            MaquinaEjercicio maejer = new MaquinaEjercicio();
            try
            {
                if (maquina != null && ejercicio != null)
                {
                    maejer.Maquina = maquina;
                    maejer.Ejercicio = ejercicio;
                    db.MaquinaEjercicio.Add(maejer);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(maquina, JsonRequestBehavior.AllowGet);

        }
        public JsonResult SaveEjerciciosMaq(string nom,string nombre)
        {
            int d = int.Parse(nom);
            Maquina maquina = db.Maquina.SingleOrDefault(x => x.Id == d);
            MaquinaEjercicio maejer = new MaquinaEjercicio();
            TipoME tipo = db.TipoME.Single(x => x.Id == maejer.Maquina.TipoId.Id);
            Ejercicio nueva = new Ejercicio();
            try
            {
                if (nombre != null)
                {
                    nueva.Nombre = nombre;
                    nueva.TipoId = tipo;
                    db.Ejercicio.Add(nueva);
                }
                db.SaveChanges();
                if (maquina != null && nueva != null)
                {
                    maejer.Maquina = maquina;
                    maejer.Ejercicio = nueva;
                    db.MaquinaEjercicio.Add(maejer);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(nueva, JsonRequestBehavior.AllowGet);

        }
        public ActionResult MaquinaEjercicio(int? id)
        {
            Maquina maquina = db.Maquina.SingleOrDefault(x => x.Id == id);
            if (maquina != null)
            {
                int i = maquina.Id;
                string n = i.ToString();
                ViewData["maquina"] = n;
                string nombre = maquina.Nombre;
                ViewData["nombre"] = nombre;

            }

            return View();
        }
        public JsonResult Descripcion(int id)
        {
            Ejercicio ejercicio = db.Ejercicio.Single(x => x.Id == id);
            var descripcion = ejercicio.Descripcion;

            return Json(descripcion, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getMaquinas()
        {
            var consulta = from t in db.Maquina
                           select new
                           {
                               Nombre = t.Nombre,
                               Id = t.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Maquina()
        {
            return View();
        }
        
    }
}