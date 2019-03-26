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
    public class EstadosPagosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TipoPago()
        {
            return View();
        }
        public JsonResult SaveTipo(string descripcion)
        {
            TipoPago tipo = new TipoPago();
            try
            {
                if (tipo != null)
                {
                    tipo.Descripcion = descripcion;
                    db.TipoPago.Add(tipo);

                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(tipo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRoles()
        {
            var roles = (from f in db.Roles
                         where f.Id == "3"
                         select new
                            {
                                Id = f.Id,
                                Name = f.Name
                            }).ToList();
            var aux = roles;
            return Json(aux, JsonRequestBehavior.AllowGet);
        }
    }
}