﻿using System;
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
    public class RutinasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rutinas
        public ActionResult Index()
        {
            return View(db.Rutinas.ToList());
        }

        // GET: Rutinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutina rutina = db.Rutinas.Find(id);
            if (rutina == null)
            {
                return HttpNotFound();
            }
            return View(rutina);
        }

        public ActionResult Create()
        {
            var getAtletas = db.Users.ToList();
            SelectList listaAtletas = new SelectList(getAtletas, "Id", "Cedula");
            ViewBag.Atletas = listaAtletas;
            return View();
        }
        public ActionResult Ejercicio(int? idRutina)
        {
            Rutina rutina = db.Rutinas.Find(idRutina);
            int i = rutina.RutinaId;
            string n = i.ToString();
            ViewData["rutina"] = n;

            var getRutina = db.Rutinas.ToList();
            SelectList listaRutinas = new SelectList(getRutina, "RutinaId", "RutinaId");
            ViewBag.Rutinas = listaRutinas;

            return View();

        }
        [HttpPost]
        public ActionResult Ejercicio(int ? rutinaSeleccionada, Conjunto_Ejercicio ejercicio)
        {
            
             Rutina rutina = new Rutina();
             rutina = db.Rutinas.Single(x => x.RutinaId == rutinaSeleccionada);
            if (rutina != null)
            {
                Conjunto_Ejercicio conjunto = new Conjunto_Ejercicio()
                {
                    ConjuntoEjercicioRutina = rutina,
                    NombreEjercicio = ejercicio.NombreEjercicio,
                    Serie1 = ejercicio.Serie1,
                    Repeticion1 = ejercicio.Repeticion1,
                    Peso1 = ejercicio.Peso1,
                    Serie2 = ejercicio.Serie2,
                    Repeticion2 = ejercicio.Repeticion2,
                    Peso2 = ejercicio.Peso2,
                    Serie3 = ejercicio.Serie3,
                    Repeticion3 = ejercicio.Repeticion3,
                    Peso3 = ejercicio.Peso3
                };
                db.Conjunto_Ejercicios.Add(conjunto);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(ejercicio);
        }
        public ActionResult ListaEjercicio(int ? idRutina)
        {
            var getEjercicio = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == idRutina).ToList();
  
            return View(getEjercicio);
        }
        public ActionResult DetailsEjercicio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conjunto_Ejercicio conjunto_Ejercicio = db.Conjunto_Ejercicios.Find(id);
            if (conjunto_Ejercicio == null)
            {
                return HttpNotFound();
            }
            return View(conjunto_Ejercicio);
        }
        public ActionResult EditEjercicio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conjunto_Ejercicio conjunto_Ejercicio = db.Conjunto_Ejercicios.Find(id);
            if (conjunto_Ejercicio == null)
            {
                return HttpNotFound();
            }
            return View(conjunto_Ejercicio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEjercicio([Bind(Include = "Conjunto_EjercicioId,NombreEjercicio,Serie1,Repeticion1,Peso1,Serie2,Repeticion2,Peso2,Serie3,Repeticion3,Peso3")] Conjunto_Ejercicio conjunto_Ejercicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conjunto_Ejercicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conjunto_Ejercicio);
        }

        // GET: Conjunto_Ejercicio/Delete/5
        public ActionResult DeleteEjercicio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conjunto_Ejercicio conjunto_Ejercicio = db.Conjunto_Ejercicios.Find(id);
            if (conjunto_Ejercicio == null)
            {
                return HttpNotFound();
            }
            return View(conjunto_Ejercicio);
        }

        // POST: Conjunto_Ejercicio/Delete/5
        [HttpPost, ActionName("DeleteEjercicio")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedEjercicio(int id)
        {
            Conjunto_Ejercicio conjunto_Ejercicio = db.Conjunto_Ejercicios.Find(id);
            db.Conjunto_Ejercicios.Remove(conjunto_Ejercicio);
            db.SaveChanges();
            return RedirectToAction("ListaEjercicio");
        }

    [HttpPost]
        public ActionResult Create(string atletaSeleccionado, Rutina rutinaCreate)
        {

            ApplicationUser user = new ApplicationUser();

          

            user = db.Users.Single(x => x.Id == atletaSeleccionado);

            if (user != null)
            {
               

                Rutina rutina = new Rutina()
                {
                    Usuario = user,
                    
                    RutinaFecha = rutinaCreate.RutinaFecha,

                    RutinaObservaciones = rutinaCreate.RutinaObservaciones
                };
          
             
                db.Rutinas.Add(rutina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rutinaCreate);
        }


    // GET: Rutinas/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutina rutina = db.Rutinas.Find(id);
            if (rutina == null)
            {
                return HttpNotFound();
            }
            return View(rutina);
        }

        // POST: Rutinas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RutinaId,RutinaFecha,RutinaObservaciones")] Rutina rutina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rutina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rutina);
        }

        // GET: Rutinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutina rutina = db.Rutinas.Find(id);
            if (rutina == null)
            {
                return HttpNotFound();
            }
            return View(rutina);
        }

        // POST: Rutinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rutina rutina = db.Rutinas.Find(id);
            db.Rutinas.Remove(rutina);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}