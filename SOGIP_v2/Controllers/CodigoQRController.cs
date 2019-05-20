using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOGIP_v2.Models;
using System.IO;
using System.Drawing;
using MessagingToolkit.QRCode.Codec;
using QRCoder;

namespace SOGIP_v2.Controllers
{
    public class CodigoQRController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CodigoQR
        public ActionResult Index()
        {
            //var id = HttpContext.User.Identity.GetUserId();
            //var userRoles = UserManager.GetRoles(id);
            //ViewBag.Role = userRoles.First();
            return View();
        }
        public JsonResult GetMaquinas()
        {
            var consulta = from f in db.Archivo.Where(x=>x.Tipo.TipoId == 7&& x.maquina!=null)
                            select new
                            {
                                Accion = "",
                                Nombre = f.maquina.Nombre,
                                Id = f.ArchivoId
                            };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMaquinas2()
        {
            var consulta = from f in db.Maquina
                           select new
                           {
                               Accion = "",
                               Nombre = f.Nombre,
                               Id = f.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMaquinas3()
        {
            var consulta = from f in db.Ejercicio
                           select new
                           {
                               Accion = "",
                               Nombre = f.Nombre,
                               Id = f.Id
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsuarios()
        {
            var consulta = from f in db.Archivo.Where(x => x.Tipo.TipoId == 7 && x.Usuario != null)
                           select new
                           {
                               Accion = "",
                               Nombre = f.Usuario.Cedula+" "+ f.Usuario.Nombre1 + " " + f.Usuario.Apellido1+" "+f.Usuario.Apellido2,
                               Id = f.ArchivoId
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsuarios2()
        {
            var consulta = from a in db.Atletas
                           from u in db.Users.Where(u => u.Id == a.Usuario.Id)
                           select new
                           {
                               Accion = "",
                               Nombre = u.Cedula + " " + u.Nombre1 + " " + u.Apellido1 + " " + u.Apellido2,
                               Id = u.Cedula
                           };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult generarQr(string txtQRCode,int id)
        {
      
            string myString = id.ToString();
            txtQRCode = txtQRCode+myString;
            Archivo nuevo = new Archivo();
            Maquina maquina = db.Maquina.Single(x => x.Id == id);
            byte[] imageBytes;
            try
            {
                QRCodeEncoder encoder = new QRCodeEncoder();
                Bitmap img = encoder.Encode(txtQRCode);
                System.Drawing.Image QR = (System.Drawing.Image)img;
                using (MemoryStream ms = new MemoryStream())
                {
                    QR.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    imageBytes = ms.ToArray();

                }
                nuevo.Contenido = imageBytes;
                nuevo.maquina = maquina;
                nuevo.Tipo = db.Tipos.Single(x => x.TipoId == 7);
                db.Archivo.Add(nuevo);

            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            db.SaveChanges();
            //String ejemplo = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageBytes));
            return Json(nuevo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult generarQr2(string id)
        {
            //int i = int.Parse(id);
            Archivo nuevo = new Archivo();
            ApplicationUser User = db.Users.Single(x => x.Cedula == id);
            string txtQRCode = User.Cedula;
            byte[] imageBytes;
            try
            {
                QRCodeEncoder encoder = new QRCodeEncoder();
                Bitmap img = encoder.Encode(txtQRCode);
                System.Drawing.Image QR = (System.Drawing.Image)img;
                using (MemoryStream ms = new MemoryStream())
                {
                    QR.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    imageBytes = ms.ToArray();

                }
                nuevo.Nombre = User.Cedula;
                nuevo.Contenido = imageBytes;
                nuevo.Usuario = User;
                nuevo.Tipo = db.Tipos.Single(x => x.TipoId == 7);
                db.Archivo.Add(nuevo);

            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            db.SaveChanges();
            //String ejemplo = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageBytes));
            return Json(nuevo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult generarEjercicio(string txtQRCode, int id)
        {
            Ejercicio ejercicio = db.Ejercicio.Single(x => x.Id == id);
            try
            {
                ejercicio.Descripcion = txtQRCode;
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            db.SaveChanges();
            return Json(ejercicio, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ObtenerUsuarios()
        {

            var consulta = from a in db.Archivo.Where(u => u.Tipo.TipoId == 7 && u.Usuario!=null)
                           select new
                           {
                               Nombre = a.Usuario.Cedula + " " + a.Usuario.Nombre1 + " " + a.Usuario.Nombre2 + " " + a.Usuario.Apellido1 + " " + a.Usuario.Apellido2,
                               Tipo = a.Tipo.Nombre,
                               Id = a.ArchivoId
                           };
   
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerMaquinas()
        {

            var consulta = from a in db.Archivo.Where(u => u.Tipo.TipoId == 7 && u.maquina != null)
                           select new
                           {
                               Nombre = a.maquina.Nombre,
                               Tipo = a.Tipo.Nombre,
                               Id = a.ArchivoId
                           };

            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerEjercicio()
        {

            var consulta = from a in db.Ejercicio.Where(x=>x.Descripcion!=null)
                           select new
                           {
                               Nombre = a.Nombre,
                               Tipo = "URL",
                               Id = a.Id
                           };

            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarArchivo(int id)
        {
            try
            {
                var archivo = db.Archivo.Where(x => x.ArchivoId == id).FirstOrDefault();
                db.Archivo.Remove(archivo);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EliminarEjer(int id)
        {
            try
            {
                Ejercicio ejercicio = db.Ejercicio.Single(x => x.Id == id);
                ejercicio.Descripcion = null;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Descripcion(int id)
        {
            Ejercicio ejercicio = db.Ejercicio.Single(x => x.Id == id);
            var descripcion = ejercicio.Descripcion;

            return Json(descripcion, JsonRequestBehavior.AllowGet);
        }
        public JsonResult obtenerIdEjer(string n)
        {
            var consulta = db.Ejercicio.Where(x => x.Nombre == n).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult obtenerIdMaq(string n)
        {
            var consulta = db.Maquina.Where(x => x.Nombre == n).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

    }
}
