using Microsoft.AspNet.Identity;
using SOGIP_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    public class AtletasAsignadosController : Controller
    {



        private ApplicationDbContext db = new ApplicationDbContext();




        [HttpPost]
        public JsonResult getAtletas()
        {
            ApplicationUser us;
            string userid = HttpContext.User.Identity.GetUserId();
            us = db.Users.Single(x => x.Id == userid);
            var seleccion = db.Selecciones.Single(x => x.Entrenador_Id.Id == us.Id);
            var consulta = //from a in db.Atletas
                       from u in db.Users
                       from a in db.Atletas
                           //where u.Id.Equals(a.Usuario.Id)
                       where u.Id.Equals(a.Usuario.Id) && a.Seleccion.SeleccionId.Equals(seleccion.SeleccionId)
                       select new
                       {
                           Cedula = u.Cedula,
                           Nombre1 = u.Nombre1,
                           Apellido1 = u.Apellido1,
                           Apellido2 = u.Apellido2
                       };



            var getAtletas = consulta.ToList();
            return Json(getAtletas, JsonRequestBehavior.AllowGet); 
        }


        [HttpPost]
        public JsonResult getAtletasS()
        {
            ApplicationUser us;
            string userid = HttpContext.User.Identity.GetUserId();
            us = db.Users.Single(x => x.Id == userid);
            var seleccion = db.Selecciones.Single(x => x.Usuario.Id == us.Id);
            var consulta = //from a in db.Atletas
                       from u in db.Users
                       from a in db.Atletas
                           //where u.Id.Equals(a.Usuario.Id)
                       where u.Id.Equals(a.Usuario.Id) && a.Seleccion.SeleccionId.Equals(seleccion.SeleccionId)
                       select new
                       {
                           Cedula = u.Cedula,
                           Nombre1 = u.Nombre1,
                           Apellido1 = u.Apellido1,
                           Apellido2 = u.Apellido2
                       };



            var getAtletas = consulta.ToList();
            return Json(getAtletas, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAtletasA()
        {
            ApplicationUser us;
            string userid = HttpContext.User.Identity.GetUserId();
            us = db.Users.Single(x => x.Id == userid);
            

            var consulta = //from a in db.Atletas
                      from u in db.Users
                      from a in db.Funcionario_ICODER
                          //where u.Id.Equals(a.Usuario.Id)
                       where u.Id.Equals(a.Usuario.Id) && a.Entrenador.Id.Equals(us.Id)
                      select new
                      {
                          Cedula = u.Cedula,
                          Nombre1 = u.Nombre1,
                          Apellido1 = u.Apellido1,
                          Apellido2 = u.Apellido2
                      };


            var getAtletas = consulta.ToList();
            return Json(getAtletas, JsonRequestBehavior.AllowGet);
        }


        // GET: AtletasAsignados
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexS()
        {
            return View();
        }

        public ActionResult IndexA()
        {
            return View();
        }

        // GET: AtletasAsignados/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AtletasAsignados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AtletasAsignados/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AtletasAsignados/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AtletasAsignados/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AtletasAsignados/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AtletasAsignados/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
