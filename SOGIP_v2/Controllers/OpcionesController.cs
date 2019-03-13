using SOGIP_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    public class OpcionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrador,Supervisor")]
        public ActionResult Avanzadas()
        {
            return View();
        }

        public JsonResult GetDeportes()
        {
            var consulta = (from d in db.Deportes
                           from t in db.Tipo_Deporte
                           where d.TipoDeporte.Tipo_DeporteId == t.Tipo_DeporteId
                           select new
                           {
                               Nombre = d.Nombre,
                               Tipo = t.Descripcion
                           }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarDeportes(string Nombre, int Tipo, int Id)
        {
            if (Id != 0)
            {
                
            }
            else {
                if (db.Deportes.Any(x => x.Nombre == Nombre))
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                else
                {
                    try
                    {
                        db.Deportes.Add(new Deporte { Nombre = Nombre, TipoDeporte = db.Tipo_Deporte.Where(x => x.Tipo_DeporteId == Tipo).FirstOrDefault() });
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetTipoDeportes()
        {
            var consulta = (from t in db.Tipo_Deporte select new { Nombre = t.Descripcion }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEstados()
        {
            var consulta = (from e in db.Estados select new { Nombre = e.Descripcion }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEntidades()
        {
            var consulta = (from en in db.Tipo_Entidad select new { Nombre = en.Descripcion }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetCategorias()
        {
            var consulta = (from c in db.Categorias select new { Nombre = c.Descripcion }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTiposArchivos()
        {
            var consulta = (from t in db.Tipos select new { Nombre = t.Nombre }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetColores()
        {
            var consulta = (from c in db.Colores select new { Codigo = c.Codigo, Nombre = c.Nombre, Seleccionado =  c.Seleccionado }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParametros()
        {
            var consulta = (from p in db.Parametros select new { Nombre = p.Nombre, Valor = p.Valor}).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }






    }
}