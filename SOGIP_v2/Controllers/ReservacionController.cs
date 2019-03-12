using Microsoft.AspNet.Identity;
using SOGIP_v2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    public class ReservacionController : Controller

    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Reservacion
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reservacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reservacion/Create
        public ActionResult Create()
        {
            return View();
        }

        //LISTA DE RESERVACIONES
        [HttpPost]
        public JsonResult GetReservaciones()
        {
            var Reservacion = db.Reservacion.Include("UsuarioId").ToList();
            return new JsonResult { Data = Reservacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public void Add(List<string> dias, List<string> horas, ApplicationUser u)
        {
            if (dias.Count > 0)
            {
                TimeSpan t1 = DateTime.Parse(horas[0]).TimeOfDay;
                TimeSpan t2 = DateTime.Parse(horas[1]).TimeOfDay;

                DateTime d1 = DateTime.Parse(dias[0]);
                DateTime d2 = DateTime.Parse(dias[0]);

                d1 = d1.Date + t1;
                d2 = d2.Date + t2;

                Reservacion reservacion = new Reservacion()
                {
                    FechaHoraInicio = d1,
                    FechaHoraFinal= d2,
                    UsuarioId=u
                };
                db.Reservacion.Add(reservacion);

                dias.RemoveAt(0);
                horas.RemoveAt(0);
                horas.RemoveAt(0);
                Add(dias,horas,u);
            }
        }

        //INGRESO DE RESERVACIONES
        [HttpPost] public JsonResult saveReser(string[] dias, string[] horas)
        {
            
            var status = false;
            ApplicationUser User;
            string userid = HttpContext.User.Identity.GetUserId();
            User = db.Users.Single(x=>x.Id==userid);

            List<string> dia = new List<string>(dias);
            List<string> hora = new List<string>(horas);

            Add(dia, hora, User);

            db.SaveChanges();

            return new JsonResult { Data = new { status = status } };
        }






        // POST: Reservacion/Create
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

        // GET: Reservacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reservacion/Edit/5
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

        // GET: Reservacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservacion/Delete/5
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
