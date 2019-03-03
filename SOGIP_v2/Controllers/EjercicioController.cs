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
    public class EjercicioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TipoEjercicio()
        {
            return View();
        }
        public JsonResult ObtenerTipo(int id)
        {

            var ejercicio = db.Ejercicio.Where(a => a.Id == id).FirstOrDefault();

            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEjercicioTipo(int id)
        {
            var status = false;
            var v = db.Ejercicio.Where(a => a.Id == id).FirstOrDefault();
            if (v != null)
            {
                var tipos = db.Ejercicio.Where(x => x.EjercicioId == id).ToList();
                foreach (var n in tipos)
                {
                    int i = n.Id;
                    Ejercicio tipo = db.Ejercicio.Find(i);
                    db.Ejercicio.Remove(tipo);
                }
                db.Ejercicio.Remove(v);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };

        }
        public JsonResult EditTipo(int id, string nombre)
        {
            Ejercicio ejercicio = db.Ejercicio.Single(x => x.Id == id);
            try
            {
                if (ejercicio != null)
                {
                    ejercicio.Nombre = nombre;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveEjercicioTipo(string nombre)
        {
            Ejercicio ejercicio= new Ejercicio();
            try
            {
                if (nombre != null)
                {
                    ejercicio.Nombre= nombre;
                    db.Ejercicio.Add(ejercicio);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTipos()
        {
            var data = from a in db.Ejercicio
                       where a.EjercicioId == 0
                       select new
                       {
                           Descripcion = a.Nombre,
                           Id = a.Id
                       };
            var Tipo = data.ToList();
            return Json(Tipo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTipo()
        {
            var data = from a in db.Ejercicio
                       where a.EjercicioId == 0
                       select new
                       {
                           Accion = "",
                           Descripcion = a.Nombre,
                           Id = a.Id
                       };
            var maquinas = data.ToList();
            return Json(maquinas, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ObtenerEjercicio(int id)
        {
            var ejercicio = db.Ejercicio.Where(a => a.Id == id).FirstOrDefault();
            var nom = ejercicio.Nombre;
            var tipo = db.Ejercicio.Where(x => x.Id == ejercicio.EjercicioId).Select(y => y.Nombre).FirstOrDefault();
            var data = new { Nombre = nom, Tipo = tipo };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEjercicio(int id)
        {
            var status = false;
            var v = db.Ejercicio.Where(a => a.Id == id).FirstOrDefault();
            if (v != null)
            {
                db.Ejercicio.Remove(v);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };

        }
        public JsonResult SaveEjercicio(string Nombre, int Tipo)
        {
            Ejercicio ejercicio = db.Ejercicio.Single(x => x.Id == Tipo);
            Ejercicio ejercicio2 = new Ejercicio();
            try
            {
                if (ejercicio != null)
                {
                    ejercicio2.EjercicioId = ejercicio.Id;
                    ejercicio2.Nombre = Nombre;
                    db.Ejercicio.Add(ejercicio2);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(ejercicio2, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEjercicios()
        {

            var data = from a in db.Ejercicio
                       where a.EjercicioId != 0
                       select new
                       {
                           Tipo = db.Ejercicio.Where(x => x.Id == a.EjercicioId).Select(y => y.Nombre).FirstOrDefault(),
                           Nombre = a.Nombre,
                           Id = a.Id

                       };
            var Maquina = data.ToList();
            return Json(Maquina, JsonRequestBehavior.AllowGet);

        }
    }
}