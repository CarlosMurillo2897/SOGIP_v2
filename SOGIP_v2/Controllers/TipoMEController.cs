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
            try { 
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
                if (categoria != null && n!= null)
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
        public JsonResult EditEjercicio(int id, string nombre)
        {
            TipoME tipo = db.TipoME.Single(x => x.Id == id);
            try
            {
                if (tipo != null)
                {
                    tipo.nombre = nombre;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(tipo, JsonRequestBehavior.AllowGet);
        }

    }
}