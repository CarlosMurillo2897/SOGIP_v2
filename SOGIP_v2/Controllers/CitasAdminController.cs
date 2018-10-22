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
    public class CitasAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CitasAdmin
        public ActionResult Index()
        {
            return View(db.Cita.ToList());
        }

        //Muestra todas las citas en el calendario
        [HttpPost]
        public JsonResult GetEvents()
        {

            var Citas = db.Cita.ToList();
            return new JsonResult { Data = Citas, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        
        //Actualiza o crea citas        
        [HttpPost]
        public JsonResult SaveEvent(Cita e)
        {
            var status = false;
            using (db)
            {
                if (e.CitaId > 0) //si existe dicha cita, solo edito los campos

                {
                    var v = db.Cita.Where(a => a.CitaId == e.CitaId).FirstOrDefault();
                    if (v != null)
                    {
                        v.InBody = e.InBody;
                        v.Otro = e.Otro;
                        v.FechaHoraInicio = e.FechaHoraInicio;
                        v.FechaHoraFinal = e.FechaHoraFinal;
                    }

                }
                else //si la cita no existe en la db, pues la creo
                {

                    ApplicationUser UserA = db.Users.Single(x => x.Email == "agueroruiz.lisandra@hotmail.com");
                    ApplicationUser UserB = db.Users.Single(x => x.Apellido2 == "Sanchez");
                    ApplicationUser User = db.Users.Single(x => x.Cedula == e.UsuarioCedula);
                    Cita nueva = new Cita()
                    {
                    InBody=e.InBody,
                    Otro=e.Otro,
                    UsuarioId_Id = User,
                    UsuarioCedula =e.UsuarioCedula,
                    UsuarioNombre=User.Nombre1,
                    UsuarioApellido1=User.Apellido1,
                    UsuarioApellido2=User.Apellido2,
                    FechaHoraInicio=e.FechaHoraInicio,
                    FechaHoraFinal=e.FechaHoraFinal
                };
                    


                    
                    db.Cita.Add(nueva);
                }
                db.SaveChanges();
                status = true;

            }

            return new JsonResult { Data = new { status = status } };
        }

        //Elimina citas del calendario
        [HttpPost]
        public JsonResult DeleteEvent(int citaId)
        {
            var status = false;
            using (db)
            {
                var v = db.Cita.Where(a => a.CitaId == citaId).FirstOrDefault();
                if (v != null)
                {
                    db.Cita.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }


        // GET: CitasAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: CitasAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CitasAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CitaId,InBody,Otro")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Cita.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cita);
        }

        // GET: CitasAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: CitasAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CitaId,InBody,Otro")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cita);
        }

        // GET: CitasAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: CitasAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.Cita.Find(id);
            db.Cita.Remove(cita);
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
