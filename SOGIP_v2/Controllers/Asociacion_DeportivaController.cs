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
    public class Asociacion_DeportivaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Asociacion_Deportiva
        public ActionResult Index()
        {
            return View(db.Asociacion_Deportiva.ToList());
        }

        // GET: Asociacion_Deportiva/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asociacion_Deportiva asociacion_Deportiva = db.Asociacion_Deportiva.Find(id);
            if (asociacion_Deportiva == null)
            {
                return HttpNotFound();
            }
            return View(asociacion_Deportiva);
        }

        // GET: Asociacion_Deportiva/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asociacion_Deportiva/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Asociacion_DeportivaId,Localidad")] Asociacion_Deportiva asociacion_Deportiva)
        {
            if (ModelState.IsValid)
            {
                db.Asociacion_Deportiva.Add(asociacion_Deportiva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asociacion_Deportiva);
        }

        // GET: Asociacion_Deportiva/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asociacion_Deportiva asociacion_Deportiva = db.Asociacion_Deportiva.Find(id);
            if (asociacion_Deportiva == null)
            {
                return HttpNotFound();
            }
            return View(asociacion_Deportiva);
        }

        // POST: Asociacion_Deportiva/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Asociacion_DeportivaId,Localidad")] Asociacion_Deportiva asociacion_Deportiva)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asociacion_Deportiva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asociacion_Deportiva);
        }

        // GET: Asociacion_Deportiva/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asociacion_Deportiva asociacion_Deportiva = db.Asociacion_Deportiva.Find(id);
            if (asociacion_Deportiva == null)
            {
                return HttpNotFound();
            }
            return View(asociacion_Deportiva);
        }

        // POST: Asociacion_Deportiva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asociacion_Deportiva asociacion_Deportiva = db.Asociacion_Deportiva.Find(id);
            db.Asociacion_Deportiva.Remove(asociacion_Deportiva);
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
