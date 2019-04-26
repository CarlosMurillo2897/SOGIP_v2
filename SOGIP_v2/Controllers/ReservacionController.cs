using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            string userid = HttpContext.User.Identity.GetUserId();
            ViewBag.idUsuario = userid;
            ApplicationUser User = db.Users.Single(x => x.Id == userid);
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            var rol = userManager.GetRoles(User.Id);
            ViewBag.role = rol.First();
            return View();
           
        }
     
        //SOLICITUDES DE RESERVACIONES
        [HttpPost]
        public JsonResult getSolicitud() //obtener los padres
        {
            var consulta = (from u in db.Reservacion
                            from r in db.Roles
                            where (u.Estado.Descripcion=="EN PROCESO")
                            group u by u.UsuarioId.Id into g
                            select new
                            {
                                Cedula = g.Select(x => x.UsuarioId.Cedula).FirstOrDefault(),
                                Nombre = g.Select(x => x.UsuarioId.Nombre1).FirstOrDefault(),
                                Apellido1 = g.Select(x => x.UsuarioId.Apellido1).FirstOrDefault(),
                                Apellido2 = g.Select(x => x.UsuarioId.Apellido2).FirstOrDefault(),
                                Id = g.Select(x => x.UsuarioId.Cedula).FirstOrDefault()
                           }).ToList();
            return new JsonResult {Data=consulta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getListaSolicitudes(string userced)//Obtener los hijos
        {

            var solicitudes = db.Reservacion.Where(x => x.UsuarioId.Cedula == userced && x.Estado.Descripcion=="EN PROCESO")
                .Select(x => new
                {
                    x.FechaHoraInicio,
                    x.FechaHoraFinal
                })
                .AsEnumerable()
                .Select(r => new
                {
                    Dia = r.FechaHoraInicio.ToString("ddd"),
                    Fecha = r.FechaHoraInicio.ToString("d"),
                    HoraI = r.FechaHoraInicio.ToString("t"),
                    HoraF = r.FechaHoraFinal.ToString("t")
                }).ToList();

            return new JsonResult { Data = solicitudes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult Aprobar(string ced)
        {
            var status = false;

            var es = db.Estados.Where(x=>x.Descripcion=="APROBADO").FirstOrDefault();
            db.Reservacion
            .Where(x => x.UsuarioId.Cedula == ced && x.Estado.Descripcion == "EN PROCESO")
            .ToList()
            .ForEach(x=>x.Estado=es);
             db.SaveChanges();

            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult Rechazar(string ced)
        {
            var status = false;

            var lista=db.Reservacion
            .Where(x => x.UsuarioId.Cedula == ced && x.Estado.Descripcion == "EN PROCESO")
            .ToList();

            foreach (var reser in lista)
            {
                db.Reservacion.Remove(reser);
            }

            db.SaveChanges();

            return new JsonResult { Data = new { status = status } };
        }

        //CÉDULA
        [HttpPost]
        public JsonResult GetCed()
        {
            string userid = HttpContext.User.Identity.GetUserId();
            ApplicationUser User = db.Users.Single(x => x.Id == userid);
            return new JsonResult { Data = User.Cedula, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //LISTA DE RESERVACIONES
        [HttpPost]
        public JsonResult GetReservaciones()
        {
            var Reservacion = db.Reservacion.Include("UsuarioId")
                                 .Include("Estado")
                                 .ToList();
            return new JsonResult { Data = Reservacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public void Add(List<string> dias, List<string> horas, ApplicationUser u, int cantidad)
        {
            if (dias.Count > 0)
            {
                TimeSpan t1 = DateTime.Parse(horas[0]).TimeOfDay;
                TimeSpan t2 = DateTime.Parse(horas[1]).TimeOfDay;

                DateTime d1 = DateTime.Parse(dias[0]);
                DateTime d2 = DateTime.Parse(dias[0]);

                d1 = d1.Date + t1;
                d2 = d2.Date + t2;
                Estado est = db.Estados.Where(x=>x.Descripcion=="EN PROCESO").FirstOrDefault();
                Reservacion reservacion = new Reservacion()
                {
                    FechaHoraInicio = d1,
                    FechaHoraFinal= d2,
                    Cantidad=cantidad,
                    Estado=est,
                    UsuarioId=u
                };
                db.Reservacion.Add(reservacion);

                dias.RemoveAt(0);
                horas.RemoveAt(0);
                horas.RemoveAt(0);
                Add(dias,horas,u, cantidad);
            }
        }

        //INGRESO DE RESERVACIONES
        [HttpPost] public JsonResult saveReser(string[] dias, string[] horas, int cantidad)
        {
            
            var status = false;
            ApplicationUser User;
            string userid = HttpContext.User.Identity.GetUserId();
            User = db.Users.Single(x=>x.Id==userid);

            List<string> dia = new List<string>(dias);
            List<string> hora = new List<string>(horas);

            Add(dia, hora, User, cantidad);
            //Holi :3
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
