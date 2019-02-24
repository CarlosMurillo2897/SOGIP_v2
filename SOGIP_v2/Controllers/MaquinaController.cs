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
        public ActionResult Index()
        {
            return View();
        }
        
      public JsonResult SaveMaquina(string nombre)
        {
            Maquina maquina = new Maquina();
            try
            {
                if (nombre != null)
                {
                    maquina.Descripcion = nombre;
                    db.Maquina.Add(maquina);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(maquina, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTipos()
        {
            var Maquina = db.Maquina.ToList();           
            return Json(Maquina, JsonRequestBehavior.AllowGet);

        }
    }
}