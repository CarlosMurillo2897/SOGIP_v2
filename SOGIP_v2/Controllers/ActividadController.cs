using SOGIP_v2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    
    public class ActividadController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Actividad
        public ActionResult Index()
        {
            return View();
        }

        // GET: Actividad/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Actividad/Create/ nos muestra la vista principal
        public ActionResult Create()
        {
            return View();
        }

        //Enviar Actividades
        [HttpPost]
        public JsonResult GetAct()
        {
            var Actividades= from a in db.Actividad
                                            from t in db.Horario
                                            where t.IdActividad==a
                                            select new
                                            {              
                                                titulo= a.Titulo,
                                                lugar= a.Lugar,
                                                descripcion= a.Descripcion,
                                                Inicio=t.FechaHoraInicio,
                                                Final=t.FechaHoraFinal
                                            };
            return new JsonResult { Data = Actividades.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //Método para ingresar la actividad al sistema
        [HttpPost]
        public JsonResult saveActividad(string Titulo, string Descripcion, string Lugar,DateTime FechaHoraInicio,
            DateTime FechaHoraFinal, HttpPostedFileBase archivo)
        {
            var status = false;

            Actividad nueva = new Actividad()
            {
                Titulo=Titulo,
                Descripcion=Descripcion,
                Lugar=Lugar
            };
            db.Actividad.Add(nueva);
            db.SaveChanges();

            //cambiar esta forma de obtener la actividad
            //agregar algún campo compuesto a la tabla actividad
            int ultimo = db.Actividad.Max(a=>a.Id);
            Actividad act = db.Actividad.Where(b => b.Id == ultimo).FirstOrDefault();

            Horario nuevo = new Horario()
            {
                IdActividad = act,
                FechaHoraInicio = FechaHoraInicio,
                FechaHoraFinal = FechaHoraFinal

            };

            db.Horario.Add(nuevo);
            db.SaveChanges();

            int val = 0;
            SubirArchivo(ultimo,archivo,val);

            return new JsonResult { Data = new { status = status } };
        }

        //Asignar imagen a la actividad con el método de Carlos pero adaptado a actividades
        public JsonResult SubirArchivo(int idAct, HttpPostedFileBase archivo, int ArchivoId)
        {
            Archivo a = null;
            if (ArchivoId == 0)
            {
                try
                {
                    BinaryReader br = new BinaryReader(archivo.InputStream);
                    byte[] buffer = br.ReadBytes(archivo.ContentLength);

                    a = new Archivo
                    {
                        actividad = db.Actividad.Where(b => b.Id == idAct).FirstOrDefault(),
                        Nombre = archivo.FileName,
                        Contenido = buffer,
                        Tipo = db.Tipos.Where(x=>x.Nombre=="Fotos de Actividades").FirstOrDefault()
                    };

                    db.Archivo.Add(a);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                try
                {
                    var Archivo = db.Archivo.SingleOrDefault(x => x.ArchivoId == ArchivoId);
                    //if (select != 0 && Archivo.Tipo.TipoId != select)
                    //{
                    //    Archivo.Tipo = db.Tipos.Where(x => x.TipoId == select).FirstOrDefault();
                    //}
                    //if (id != "" && Archivo.Usuario.Cedula != id)
                    //{
                    //    Archivo.Usuario = db.Users.Where(x => x.Cedula == id).FirstOrDefault();
                    //}
                    if (archivo != null)
                    {
                        BinaryReader br = new BinaryReader(archivo.InputStream);
                        byte[] buffer = br.ReadBytes(archivo.ContentLength);
                        Archivo.Contenido = buffer;
                    }
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(a, JsonRequestBehavior.AllowGet);
        }
        //ENVIAR IMAGENES AL INDEX
        public JsonResult images()
        {

            var imagenes =
                db.Archivo
				.Where(x => x.actividad != null && 
				db.Horario.Where(y => y.IdActividad.Id == x.actividad.Id).FirstOrDefault().FechaHoraFinal >= DateTime.Today)
				.OrderByDescending(x => x.ArchivoId) //-->ordeno de atrás hacia delante
                .Select(x => new
                {
                    x.Contenido,
                    x.Nombre,
                    x.ArchivoId,
                    x.actividad
                })
               .AsEnumerable() //--> fué la única solución que econtré para poder usar string.Format(...)
               .Select(a=>new { //
                   Contenido = string.Format("data:image/"+ a.Nombre.Substring(a.Nombre.Length - 3) + ";base64,{0}", Convert.ToBase64String(a.Contenido)),
                   Id =a.ArchivoId,
                   Actividad=a.actividad.Id //--> lo necesito porque tengo pensado que salga un modal (info actividad) al dar click en la imagen del carusel
               }).Take(3).ToList(); //--> agarro los ultimos tres

             return new JsonResult { Data = imagenes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }





       // POST: Actividad/Create
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

        // GET: Actividad/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Actividad/Edit/5
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

        // GET: Actividad/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Actividad/Delete/5
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
