using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOGIP_v2.Models;
namespace SOGIP_v2.Controllers
{
    public class CategoriaMontoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CategoriaMonto
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCategorias()
        {
            var lista = db.Roles.ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}