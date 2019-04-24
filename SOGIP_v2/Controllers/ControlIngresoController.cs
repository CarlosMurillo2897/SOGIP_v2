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
    public class ControlIngresoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ControlIngreso
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult ingreso(string id)
        {
            var consulta = db.Archivo.Where(x => x.Usuario.Cedula == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveIngreso(string id)
        {
            ApplicationUser User = db.Users.Single(x => x.Cedula == id);
            ControlIngreso  nueva = new ControlIngreso();
            DateTime fecha = DateTime.Now;
            try
            {
                if (User != null)
                {
                    nueva.Usuario = User;
                    nueva.Fecha = fecha;
                    db.ControlIngreso.Add(nueva);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        
            return Json(nueva, JsonRequestBehavior.AllowGet);
        }


        //--------------> L  I  S  A  N  D  R  A

        public ActionResult Grafico()
        {
            return View();
        }

        //------------> HOMBRES Y MUJERES POR UNA SELECCIÓN EN CONCRETO POR MES EN CONCRETO
        //------------> HOMBRES Y MUJERES POR UNA SELECCIÓN EN CONCRETO POR VARIOS MESES
        //------------> HOMBRES Y MUJERES POR UNA SELECCIÓN VARIAS POR MES EN CONCRETO
        //------------> HOMBRES Y MUJERES POR UNA SELECCIÓN VARIAS POR VARIOS MESES


    }
}