using SOGIP_v2.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
namespace SOGIP_v2.Controllers
{
    public class Ejercicio_RutinaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
        public  ActionResult Create()
        {
            var getAtletas = db.Users.ToList();
            SelectList listaAtletas = new SelectList(getAtletas, "Cedula", "Nombre1");
            ViewBag.Atletas = listaAtletas;
     
            return View();
        }

            public ActionResult Edit()
        {
            return View();
        }
    }
}