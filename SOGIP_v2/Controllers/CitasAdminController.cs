﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        [HttpPost]
        public JsonResult GetCed()
        {
            string userid = HttpContext.User.Identity.GetUserId();
            ApplicationUser User= db.Users.Single(x => x.Id == userid);
            return new JsonResult { Data=User.Cedula, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Muestra todas las citas en el calendario
        [HttpPost]
        public JsonResult GetEvents()
        {

            var Citas = db.Cita.Include("UsuarioId_Id").ToList();
            return new JsonResult { Data = Citas, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult getUsuariosA()
        {
            var consulta1 =
                           from f in db.Funcionario_ICODER
                           from u in db.Users.Where(u => u.Id == f.Usuario.Id)
                           select new
                           {
                               Accion = "",
                               Cedula = u.Cedula,
                               Nombre = u.Nombre1,
                               Apellido1 = u.Apellido1,
                               Apellido2 = u.Apellido2,
                               Rol = "Funcionario"
                           };

            var consulta = 
                           from a in db.Atletas
                           from u in db.Users.Where(u=>u.Id==a.Usuario.Id) 
                           select new
                           {
                               Accion = "",
                               Cedula = u.Cedula,
                               Nombre = u.Nombre1,
                               Apellido1 = u.Apellido1,
                               Apellido2 = u.Apellido2,
                               Rol = "Atleta"
                           };

            var enume=Enumerable.Union(consulta1,consulta);
            var usuarios = enume.ToList();
            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }


        //Actualiza o crea citas        
        [HttpPost]
        public JsonResult SaveEvent(Cita e, string cedu)
        {
            
            var status = false;
            using (db)
            {
                if (e.CitaId > 0) //si existe dicha cita, solo edito los campos

                {
                    var v = db.Cita.Where(a => a.CitaId == e.CitaId).FirstOrDefault();
                    if (v != null)
                    {

                        var check = db.Cita.Where(b => b.FechaHoraInicio == e.FechaHoraInicio).FirstOrDefault();
                        var check2 = db.Cita.Where(x => x.FechaHoraFinal == e.FechaHoraInicio).FirstOrDefault();


                        if (check == null && check2 == null)
                        {
                            v.InBody = e.InBody;
                            v.Otro = e.Otro;
                            v.FechaHoraInicio = e.FechaHoraInicio;
                            v.FechaHoraFinal = e.FechaHoraFinal;
                        }
                        else if (e.FechaHoraInicio == v.FechaHoraInicio)
                        {
                            v.InBody = e.InBody;
                            v.Otro = e.Otro;
                            v.FechaHoraFinal = e.FechaHoraFinal;
                        }
                        else
                        {
                            return new JsonResult { Data = new { status = false } };
                        }

                    }

                }
                else //si la cita no existe en la db, pues la creo
                {
                    ApplicationUser User;
                    string userid = HttpContext.User.Identity.GetUserId();
                    var ceduser= cedu;
                    bool role = HttpContext.User.IsInRole("Administrador");
                    
                    User = (role) ? db.Users.Single(x => x.Cedula == ceduser) : db.Users.Single(x => x.Id == userid);


                        var check = db.Cita.Where(b => b.FechaHoraInicio == e.FechaHoraInicio).FirstOrDefault();
                        var check2 = db.Cita.Where(x => x.FechaHoraFinal == e.FechaHoraInicio).FirstOrDefault();
                        if (check == null && check2 == null) //hora de inicio diferente a la de otra cita, y diferente de la finalización de otra.
                        {
                            Cita nueva = new Cita()
                            {
                                InBody = e.InBody,
                                Otro = e.Otro,
                                UsuarioId_Id = User,
                                FechaHoraInicio = e.FechaHoraInicio,
                                FechaHoraFinal = e.FechaHoraFinal
                            };
                            db.Cita.Add(nueva);
                        }
                        else
                        {
                            return new JsonResult { Data = new { status = false } };
                        }
                    
             
  
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



        // GET: CitasAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        //Citas Generales
        public ActionResult Create2()
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
