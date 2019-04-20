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
    public class MaquinaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rutinas
        public ActionResult Index()
        {
            return View();
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
        public JsonResult MaquinaRepetido(string nombre)
        {
            return Json(!db.Ejercicio.Any(x => x.Nombre == nombre), JsonRequestBehavior.AllowGet);
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
        public JsonResult getNombre(int id)
        {
            var consulta = db.Maquina.Where(x => x.Id == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditMaquina(int id, string nombre)
        {
            Maquina maquina = db.Maquina.Single(x => x.Id == id);
            try
            {
                if (maquina != null)
                {
                    maquina.Nombre = nombre;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(maquina, JsonRequestBehavior.AllowGet);
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
                               Id = t.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEjerMaquina()
        {
            var consulta = from t in db.Ejercicio
                           select new
                           {
                               Nombre = t.Nombre,
                               Id = t.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}