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
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveEjercicio(string nombre)
        {
            Ejercicio ejercicio = new Ejercicio();
            try
            {
                if (nombre != null)
                {
                    ejercicio.Descripcion = nombre;
                    db.Ejercicio.Add(ejercicio);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTipos()
        {
            var Ejercicios = db.Ejercicio.ToList();
            return Json(Ejercicios, JsonRequestBehavior.AllowGet);

        }
    }
}