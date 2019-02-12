using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SOGIP_v2.Models;
using SOGIP_v2.Models.Agrupaciones;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    public class ExpedientesFisicosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ExpedientesFisicosController()
        {

        }

        public ExpedientesFisicosController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.GetUserId();
            var userRoles = UserManager.GetRoles(id);
            ViewBag.Role = userRoles.First();
            var list = db.Archivo.Include("Usuario").Include("Tipo").ToList();
            return View(list);
        }

        public JsonResult ObtenerUsuarios()
        {
            var list = db.Users.Where(x => x.Roles.Select(y => y.RoleId == "5" || y.RoleId == "6" || y.RoleId == "7").FirstOrDefault()).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerTipos(string role)
        {
            var tipos = (role == "Entrenador") ? db.Tipos.Where(x => x.Nombre == "CV" || x.Nombre == "Prueba de Fuerza" || x.Nombre == "InBody").ToList() : db.Tipos.ToList();
            return Json(tipos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubirArchivo(string id, int select, HttpPostedFileBase archivo, int ArchivoId)
        {
            Archivo a = null;
            if (ArchivoId == 0)
            {
                try
                {
                    BinaryReader br = new BinaryReader(archivo.InputStream);
                    byte[] buffer = br.ReadBytes(archivo.ContentLength);
                    // archivo.SaveAs(Server.MapPath("~/Content/Registros/" + archivo.FileName));

                    a = new Archivo
                    {
                        Tipo = db.Tipos.Where(x => x.TipoId == select).FirstOrDefault(),
                        Usuario = db.Users.Where(x => x.Cedula == id).FirstOrDefault(),
                        Nombre = archivo.FileName,
                        Contenido = buffer
                    };

                    db.Archivo.Add(a);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/Registros/" + archivo.FileName)))
                    {
                        // System.IO.File.Delete(Server.MapPath("~/Content/Registros/" + archivo.FileName));
                    }
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                try
                {
                    var Archivo = db.Archivo.SingleOrDefault(x => x.ArchivoId == ArchivoId);
                    if (select != 0 && Archivo.Tipo.TipoId != select)
                    {
                        Archivo.Tipo = db.Tipos.Where(x => x.TipoId == select).FirstOrDefault();
                    }
                    if (id != "" && Archivo.Usuario.Cedula != id)
                    {
                        Archivo.Usuario = db.Users.Where(x => x.Cedula == id).FirstOrDefault();
                    }
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
            
            // System.IO.File.Delete(Server.MapPath("~/Content/Registros/" + archivo.FileName));
            
            return Json(a, JsonRequestBehavior.AllowGet);
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

        //public JsonResult EditarArchivo(int id, string cedula, )
        //{
        //    try
        //    {
        //        var archivo = db.Archivo.Single(x => x.ArchivoId == id);
        //        archivo.Usuario = db.Users.Single(y => y.Cedula == cedula);

        //        db.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(true, JsonRequestBehavior.AllowGet);

        //}
    }
}