using SOGIP_v2.Models;
using SOGIP_v2.Models.Agrupaciones;
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

        // ******************************************* AGREGAR O EDITAR DATOS *******************************************

        public JsonResult AgregarCategoria(string Nombre, int id)
        {
            Categoria c = new Categoria();// { Descripcion = Nombre };
            try {
                if (Nombre == "")
                {
                    throw new ArgumentNullException("El nombre no puede ser nulo.", nameof(Nombre)); ;
                }
                if (id != 0) {
                    c = db.Categorias.SingleOrDefault(x => x.CategoriaId == id);
                }

                c.Descripcion = Nombre;

                if (id == 0) {
                    db.Categorias.Add(c);
                }
                    db.SaveChanges();
            }
            catch (Exception e) { Response.StatusCode = (int)HttpStatusCode.InternalServerError; }

            return Json(c, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarColor(string Nombre, string Codigo, bool Selected, int id)
        {
            Color c = new Color();// { Descripcion = Nombre };
            try {
                if (Nombre == "" || Codigo == "")
                {
                    throw new ArgumentNullException("El nombre, ni el Codigo pueden ser nulos.", nameof(Nombre)); ;
                }
                if (id != 0) {
                    c = db.Colores.SingleOrDefault(x => x.ColorId == id);
                }

                c.Nombre = Nombre;
                c.Codigo = Codigo;
                c.Seleccionado = Selected;

                if (id == 0) {
                    db.Colores.Add(c);
                }
                    db.SaveChanges();
            }
            catch (Exception e) { Response.StatusCode = (int)HttpStatusCode.InternalServerError; }

            return Json(c, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarDeportes(string Nombre, string Tipo, int id)
        {
            Deporte d = new Deporte();
            try
            {
                // Validamos que nada venga vacío.
                if (Nombre == "" || Tipo == "") { throw new ArgumentNullException("El nombre no puede ser nulo.", nameof(Nombre)); }

                // Si el id no es igual a cero es porque se está editando un deporte ya creado, con este id traeremos ese Deporte para editarlo.
                if (id != 0) { d = db.Deportes.SingleOrDefault(x => x.DeporteId == id); }
                // En caso de que el id sea diferente de 0 ya lo estaríamos editando en las próximas dos líneas, en caso contrario estamos creando un Deporte nuevo.
                d.Nombre = Nombre;
                d.TipoDeporte = db.Tipo_Deporte.Where(x => x.Descripcion == Tipo).FirstOrDefault();
                // Si el id es igual a cero es nuevo por lo que hay que agregarlo a la DB.
                if (id == 0) { db.Deportes.Add(d); }

                db.SaveChanges();
            }
            // Si algo sale mal, enviamos una mala respuesta y 
            catch (Exception) { Response.StatusCode = (int)HttpStatusCode.InternalServerError; }

            return Json(d, JsonRequestBehavior.AllowGet);

        }

       public JsonResult AgregarTipoEntidad(string Nombre, int id)
        {
            Tipo_Entidad te = new Tipo_Entidad();
            try
            {
                // Validamos que nada venga vacío.
                if (Nombre == "") { throw new ArgumentNullException("El nombre no puede ser nulo.", nameof(Nombre)); }

                // Si el id no es igual a cero es porque se está editando un deporte ya creado, con este id traeremos ese Deporte para editarlo.
                if (id != 0) { te = db.Tipo_Entidad.SingleOrDefault(x => x.Tipo_EntidadId == id); }
                // En caso de que el id sea diferente de 0 ya lo estaríamos editando en las próximas dos líneas, en caso contrario estamos creando un Deporte nuevo.
                te.Descripcion = Nombre;
                // Si el id es igual a cero es nuevo por lo que hay que agregarlo a la DB.
                if (id == 0) { db.Tipo_Entidad.Add(te); }

                db.SaveChanges();
            }
            // Si algo sale mal, enviamos una mala respuesta y 
            catch (Exception) { Response.StatusCode = (int)HttpStatusCode.InternalServerError; }

            return Json(te, JsonRequestBehavior.AllowGet);

        }

       public JsonResult AgregarTipoArchivo(string Nombre, int id)
        {
            Tipo t = new Tipo();
            try
            {
                // Validamos que nada venga vacío.
                if (Nombre == "") { throw new ArgumentNullException("El nombre no puede ser nulo.", nameof(Nombre)); }

                // Si el id no es igual a cero es porque se está editando un deporte ya creado, con este id traeremos ese Deporte para editarlo.
                if (id != 0) { t = db.Tipos.SingleOrDefault(x => x.TipoId == id); }
                // En caso de que el id sea diferente de 0 ya lo estaríamos editando en las próximas dos líneas, en caso contrario estamos creando un Deporte nuevo.
                t.Nombre = Nombre;
                // Si el id es igual a cero es nuevo por lo que hay que agregarlo a la DB.
                if (id == 0) { db.Tipos.Add(t); }

                db.SaveChanges();
            }
            // Si algo sale mal, enviamos una mala respuesta y 
            catch (Exception) { Response.StatusCode = (int)HttpStatusCode.InternalServerError; }

            return Json(t, JsonRequestBehavior.AllowGet);

        }

       public JsonResult AgregarTipoDeporte(string Nombre, int id)
        {
            Tipo_Deporte td = new Tipo_Deporte();
            try
            {
                // Validamos que nada venga vacío.
                if (Nombre == "") { throw new ArgumentNullException("El nombre no puede ser nulo.", nameof(Nombre)); }

                // Si el id no es igual a cero es porque se está editando un deporte ya creado, con este id traeremos ese Deporte para editarlo.
                if (id != 0) { td = db.Tipo_Deporte.SingleOrDefault(x => x.Tipo_DeporteId == id); }
                // En caso de que el id sea diferente de 0 ya lo estaríamos editando en las próximas dos líneas, en caso contrario estamos creando un Deporte nuevo.
                td.Descripcion = Nombre;
                // Si el id es igual a cero es nuevo por lo que hay que agregarlo a la DB.
                if (id == 0) { db.Tipo_Deporte.Add(td); }

                db.SaveChanges();
            }
            // Si algo sale mal, enviamos una mala respuesta y 
            catch (Exception) { Response.StatusCode = (int)HttpStatusCode.InternalServerError; }

            return Json(td, JsonRequestBehavior.AllowGet);

        }


        // ******************************************* OBTENER DATOS *******************************************

        public JsonResult GetCategorias()
        {
            var consulta = (from c in db.Categorias select new { c.Descripcion, c.CategoriaId }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetColores()
        {
            var consulta = (from c in db.Colores select new { c.Codigo, c.Nombre, c.Seleccionado, c.ColorId }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportes()
        {
            var consulta = (from d in db.Deportes
                            from t in db.Tipo_Deporte
                            where d.TipoDeporte.Tipo_DeporteId == t.Tipo_DeporteId
                            select new { d.Nombre, t.Descripcion, d.DeporteId
                            }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEntidades()
        {
            var consulta = (from en in db.Tipo_Entidad select new { en.Descripcion, en.Tipo_EntidadId }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEstados()
        {
            var consulta = (from e in db.Estados select new { e.Descripcion, e.EstadoId }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParametros()
        {
            var consulta = (from p in db.Parametros select new { p.Nombre, p.Valor, p.ParametroId }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTiposArchivos()
        {
            var consulta = (from t in db.Tipos select new { t.Nombre, t.TipoId }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTipoDeportes()
        {
            var consulta = (from t in db.Tipo_Deporte select new { t.Descripcion, t.Tipo_DeporteId }).ToList();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        // ******************************************* CAMPOS REPETIDOS *******************************************
        public JsonResult CategoriaRepetida(string nombre)
        {
            return Json(!db.Categorias.Any(x => x.Descripcion == nombre), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ColorRepetido(string nombre, int original)
        {
            bool aux = true;
            
                aux = !db.Colores.Any(x => x.Nombre == nombre && x.ColorId != original);
            
                return Json(aux, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult CodigoColorRepetido(string codigo, int original)
        {
            return Json(!db.Colores.Any(x => x.Codigo == codigo && x.ColorId != original), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeporteRepetido(string nombre, int original)
        {
            return Json(!db.Deportes.Any(x => x.Nombre == nombre && x.DeporteId != original), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TipoEntidadPublicaRepetida(string nombre)
        {
            return Json(!db.Tipo_Entidad.Any(x => x.Descripcion == nombre), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EstadoRepetido(string nombre)
        {
            return Json(!db.Estados.Any(x => x.Descripcion == nombre), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ParametroRepetido(string nombre)
        {
            return Json(!db.Parametros.Any(x => x.Nombre == nombre), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TipoArchivoRepetido(string nombre)
        {
            return Json(!db.Tipos.Any(x => x.Nombre == nombre), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TipoDeportRepetido(string nombre)
        {
            return Json(!db.Tipo_Deporte.Any(x => x.Descripcion == nombre), JsonRequestBehavior.AllowGet);
        }

    }
}