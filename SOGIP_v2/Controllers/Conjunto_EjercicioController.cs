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
    public class Conjunto_EjercicioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Conjunto_Ejercicio
        public ActionResult Index()
        {
            return View(db.Conjunto_Ejercicios.ToList());
        }

        // GET: Conjunto_Ejercicio/Details/5
        public ActionResult Details(int? id)
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

        // GET: Conjunto_Ejercicio/Create
        public ActionResult Create(int? id)
        {
            //var getRutina = db.Rutinas.ToList();
            //SelectList listaRutinas = new SelectList(getRutina, "RutinaId", "RutinaId");
            //ViewBag.Rutinas = listaRutinas;
            Rutina rutina = db.Rutinas.Find(id);
            int i = rutina.RutinaId;
            string n = i.ToString();
            ViewData["rutina"] = n;
            return View();

        }

        // POST: Conjunto_Ejercicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(int? idRutina, Conjunto_Ejercicio ejercicio)
        {
            Rutina rutina = new Rutina();
            rutina = db.Rutinas.Single(x => x.RutinaId == idRutina);
            if (rutina != null)
            {
                Conjunto_Ejercicio conjunto = new Conjunto_Ejercicio()
                {
                    ConjuntoEjercicioRutina = rutina,
                    NombreEjercicio = ejercicio.NombreEjercicio,
                    Serie1 = ejercicio.Serie1,
                    Repeticion1 = ejercicio.Repeticion1,
                    Peso1 =ejercicio.Peso1,
                    Serie2 = ejercicio.Serie2,
                    Repeticion2 = ejercicio.Repeticion2,
                    Peso2 = ejercicio.Peso2,
                    Serie3 = ejercicio.Serie3,
                    Repeticion3 = ejercicio.Repeticion3,
                    Peso3 = ejercicio.Peso3
                }; 
                db.Conjunto_Ejercicios.Add(conjunto);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(ejercicio);
        }

        // GET: Conjunto_Ejercicio/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Conjunto_Ejercicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Conjunto_EjercicioId,NombreEjercicio,Serie1,Repeticion1,Peso1,Serie2,Repeticion2,Peso2,Serie3,Repeticion3,Peso3")] Conjunto_Ejercicio conjunto_Ejercicio)
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
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Conjunto_Ejercicio conjunto_Ejercicio = db.Conjunto_Ejercicios.Find(id);
            db.Conjunto_Ejercicios.Remove(conjunto_Ejercicio);
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
