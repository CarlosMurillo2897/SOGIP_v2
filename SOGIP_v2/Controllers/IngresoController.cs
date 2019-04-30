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
    public class IngresoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ingreso
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ingreso(string id)
        {
            var consulta = db.Archivo.Where(x => x.Usuario.Cedula == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
    }
}