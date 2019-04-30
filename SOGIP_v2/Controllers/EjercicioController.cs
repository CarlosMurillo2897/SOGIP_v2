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
    public class EjercicioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Ejercicio
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getEjercicios()
        {
            var consulta = from t in db.Ejercicio
                           select new
                           {
                               Nombre = t.Nombre,
                               Id = t.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult EjercicioRepetido(string nombre)
        {
            return Json(!db.Ejercicio.Any(x => x.Nombre == nombre), JsonRequestBehavior.AllowGet);
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
        public JsonResult getNombre(int id)
        {
            var consulta = db.Ejercicio.Where(x => x.Id == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
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

    }
}