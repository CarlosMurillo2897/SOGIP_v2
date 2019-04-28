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
    public class ControlIngresoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<int> listF = new List<int>();
        private List<int> listM = new List<int>();
        // GET: ControlIngreso
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ingreso(string id)
        {
            var consulta = db.Archivo.Where(x => x.Usuario.Cedula == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveIngreso(string id)
        {
            ApplicationUser User = db.Users.Single(x => x.Cedula == id);
            ControlIngreso  nueva = new ControlIngreso();
            DateTime fecha = DateTime.Now;
            try
            {
                if (User != null)
                {
                    nueva.Usuario = User;
                    nueva.Fecha = fecha;
                    db.ControlIngreso.Add(nueva);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        
            return Json(nueva, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Estadistica()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Selecciones() //------->selecciones
        {
            var lista = (
                from s in db.Selecciones
                select new
                {
                    Id = s.SeleccionId,
                    Nombre = s.Nombre_Seleccion
                }
                ).ToList();
                

            return new JsonResult { Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult Asociaciones() //------->asociaciones
        {
            var lista = (
                from a in db.Asociacion_Deportiva
                select new
                {
                    Id =a.Asociacion_DeportivaId,
                    Nombre = a.Nombre_DepAso
                }
                ).ToList();


            return new JsonResult { Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult Entidades() //------->entidades públicas
        {
            var lista = (
                from e in db.Tipo_Entidad
                select new
                {
                    Id = e.Tipo_EntidadId,
                    Nombre = e.Descripcion
                }
                ).ToList();


            return new JsonResult { Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public void addFem(List<string> mes, int sele)
        {
            if (mes.Count>0) {

              int m = Int32.Parse(mes[0]);
               var lista = (
               from c in db.ControlIngreso
               from a in db.Atletas
               where c.Fecha.Month == m && c.Usuario.Sexo == false && c.Usuario == a.Usuario && a.SubSeleccion.SubSeleccionId == sele
               select c.Usuario
               ).Count();
                listF.Add(lista);
                mes.RemoveAt(0);
                addFem(mes,sele);
            }
            

        }

        public void addMasc(List<string> mes, int sele)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Month == m && c.Usuario.Sexo == true && c.Usuario == a.Usuario && a.SubSeleccion.SubSeleccionId == sele
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addMasc(mes, sele);
            }


        }


        [HttpPost]
        public JsonResult PorMesSeleF(string[] mes, int seleccion)
        {
            listF.Clear();
            List<string> m = new List<string>(mes);
            addFem(m,seleccion);

            return new JsonResult { Data = listF, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult PorMesSeleM(string[] mes, int seleccion)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addMasc(m, seleccion);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


    }
}