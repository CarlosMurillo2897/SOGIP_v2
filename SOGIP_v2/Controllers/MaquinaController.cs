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
    }
}