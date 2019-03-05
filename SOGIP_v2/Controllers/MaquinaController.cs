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
    public class MaquinaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TipoMaquina()
        {
            return View();
        }
        public ActionResult ListaEjercicios(int? id)
        {
            Maquina maquina = db.Maquina.SingleOrDefault(x => x.Id == id);
            if (maquina != null)
            {
                string idn = id.ToString();
                ViewData["id"] = idn;
                string nombre = maquina.Descripcion;
                ViewData["nombre"] = nombre;
            }
                return View();
        }
        public JsonResult SaveMaquinaEjercicio(string nom, int ejer)
        {
            int n = int.Parse(nom);
            Ejercicio ejercicio = db.Ejercicio.Single(x => x.Id == ejer);
            Maquina maquina = db.Maquina.Single(x => x.Id == n);
            MaquinaEjercicio maquinaEjercicio = new MaquinaEjercicio();
            try
            {
                if(ejercicio != null || maquina != null)
                {
                    maquinaEjercicio.Maquina = maquina;
                    maquinaEjercicio.Ejercicio = ejercicio;
                    db.MaquinaEjercicio.Add(maquinaEjercicio);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(maquinaEjercicio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveMaquina(string Nombre,int Tipo)
        {
            Maquina maquina = db.Maquina.Single(x => x.Id == Tipo);
            Maquina maquina2 = new Maquina();
            try
            {
                if (maquina != null)
                {
                    maquina2.MaquinaId = maquina.Id;
                    maquina2.Descripcion = Nombre;
                    db.Maquina.Add(maquina2);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(maquina2, JsonRequestBehavior.AllowGet);
        }
      public JsonResult SaveMaquinaTipo(string nombre)
        {
            Maquina maquina = new Maquina();
            try
            {
                if (nombre != null)
                {
                    maquina.Descripcion = nombre;
                    db.Maquina.Add(maquina);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(maquina, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMaquinaTipo(int id)
        {
            var status = false;
            var v = db.Maquina.Where(a => a.Id == id).FirstOrDefault();
            if (v != null)
            {
                var tipos = db.Maquina.Where(x => x.MaquinaId == id).ToList();
                foreach(var n in tipos)
                {
                    int i = n.Id;
                    Maquina tipo = db.Maquina.Find(i);
                    db.Maquina.Remove(tipo);
                }
                db.Maquina.Remove(v);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };

        }
        public JsonResult DeleteMaquina(int id)
        {
            var status = false;
            var v = db.Maquina.Where(a => a.Id == id).FirstOrDefault();
            if (v != null)
            {
                db.Maquina.Remove(v);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };

        }
        public JsonResult GetMaquinas()
        {

            var data = from a in db.Maquina
                       where a.MaquinaId !=0
                       select new
                       {
                           Tipo = db.Maquina.Where(x => x.Id==a.MaquinaId).Select(y => y.Descripcion).FirstOrDefault(),
                           Nombre = a.Descripcion,
                           Id = a.Id

                       };
            var Maquina = data.ToList();           
            return Json(Maquina, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetTipos()
        {
            var data = from a in db.Maquina
                       where a.MaquinaId == 0
                       select new
                       {
                           Descripcion = a.Descripcion,
                           Id = a.Id
                       };
            var Tipo = data.ToList();
            return Json(Tipo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerTipo(int id)
        {
           
            var maquina = db.Maquina.Where(a => a.Id == id).FirstOrDefault();
           
            return Json(maquina, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerMaquina(int id)
        {
            var maquina = db.Maquina.Where(a => a.Id == id).FirstOrDefault();
            var nom = maquina.Descripcion;
            var tipo = db.Maquina.Where(x => x.Id == maquina.MaquinaId).Select(y => y.Descripcion).FirstOrDefault();
            var data =  new {Nombre = nom, Tipo = tipo };
           
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditTipo(int id, string nombre)
        {
            Maquina maquina = db.Maquina.Single(x => x.Id == id);
            try
            {
                if (maquina != null)
                {
                    maquina.Descripcion= nombre;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(maquina, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTipo()
        {
            var data = from a in db.Maquina
                       where a.MaquinaId == 0
                       select new
                       {
                           Accion ="",
                           Descripcion = a.Descripcion,
                            Id = a.Id
                       };
            var maquinas = data.ToList();
            return Json(maquinas, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetEjercicios()
        {
            var data = from a in db.Ejercicio
                       where a.EjercicioId != 0
                       select new
                       {
                           Accion = "",
                           Tipo = db.Ejercicio.Where(x => x.Id == a.EjercicioId).Select(y => y.Nombre).FirstOrDefault(),
                           Nombre = a.Nombre,
                           Id = a.Id

                       };
            var ejercicio = data.ToList();
            return Json(ejercicio, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEjerciciosA()
        {

            var data = from a in db.MaquinaEjercicio.Include("Ejercicio")
                       select new
                       {
                           Tipo = db.Ejercicio.Where(x => x.Id == a.Ejercicio.EjercicioId).Select(y => y.Nombre).FirstOrDefault(),
                           Nombre = a.Ejercicio.Nombre,
                           Id = a.Id

                       };
            var Maquina = data.ToList();
            return Json(Maquina, JsonRequestBehavior.AllowGet);

        }
        public JsonResult EliminarMaquEjer(int id)
        {
            var status = false;
            var v = db.MaquinaEjercicio.Where(a => a.Id == id).FirstOrDefault();
            if (v != null)
            {
                db.MaquinaEjercicio.Remove(v);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
        public JsonResult GetEjerciciosB(string nom)
        {

            int n = int.Parse(nom);
            var data = from a in db.MaquinaEjercicio.Include("Ejercicio")
                       where a.Maquina.Id == n
                       select new
                       {
                           Tipo = db.Ejercicio.Where(x => x.Id == a.Ejercicio.EjercicioId).Select(y => y.Nombre).FirstOrDefault(),
                           Nombre = a.Ejercicio.Nombre,
                           Id = a.Id

                       };
            var Maquina = data.ToList();
            return Json(Maquina, JsonRequestBehavior.AllowGet);
        }
       
    }
}