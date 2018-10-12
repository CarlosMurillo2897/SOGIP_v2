using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sistema Gestor de Usuarios de Gimnasio.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Comunicarse al 2549 0700.";

            return View();
        }
        public ActionResult header()
        {
            return View();
        }
    }
}