using SOGIP_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    public class ActividadController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Actividad
        public ActionResult Index()
        {
            return View();
        }

        // GET: Actividad/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Actividad/Create/ nos muestra la vista principal
        public ActionResult Create()
        {
            return View();
        }

        //Enviar Actividades
        [HttpPost]
        public JsonResult GetAct()
        {
            var Actividades= from a in db.Actividad
                                            from t in db.Horario
                                            where t.IdActividad==a
                                            select new
                                            {              
                                                titulo= a.Titulo,
                                                lugar= a.Lugar,
                                                descripcion= a.Descripcion,
                                                Inicio=t.FechaHoraInicio,
                                                Final=t.FechaHoraFinal
                                            };
            return new JsonResult { Data = Actividades.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //Método para ingresar la actividad al sistema
        [HttpPost]
        public JsonResult saveActividad(Actividad actividad, Horario horario)
        {
            var status = false;

            Actividad nueva = new Actividad()
            {
                Titulo=actividad.Titulo,
                Descripcion=actividad.Descripcion,
                Lugar=actividad.Lugar
            };
            db.Actividad.Add(nueva);
            db.SaveChanges();

            //cambiar esta forma de obtener la actividad
            //agregar algún campo compuesto a la tabla actividad
            int ultimo = db.Actividad.Max(a=>a.Id);
            Actividad act = db.Actividad.Where(b => b.Id == ultimo).FirstOrDefault();

            Horario nuevo = new Horario()
            {
                IdActividad = act,
                FechaHoraInicio = horario.FechaHoraInicio,
                FechaHoraFinal = horario.FechaHoraFinal

            };

            db.Horario.Add(nuevo);
            db.SaveChanges();

            return new JsonResult { Data = new { status = status } };
        }



        // POST: Actividad/Create
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

        // GET: Actividad/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Actividad/Edit/5
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

        // GET: Actividad/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Actividad/Delete/5
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
