using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SOGIP_v2.Models;
using SOGIP_v2.Models.Agrupaciones;
using System;
using System.Collections.Generic;
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

        public ExpedientesFisicosController(){ }

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
            return View();
        }

        public JsonResult ObtenerArchivos(int filtro)
        {

                var consulta = filtro == 0 ?
                from a in db.Archivo
                    select new
                    {
                        Nombre = a.Nombre,
                        Tipo = a.Tipo.Nombre,
                        Usuario = a.Usuario.Cedula + " " + a.Usuario.Nombre1 + " " + a.Usuario.Nombre2 + " " + a.Usuario.Apellido1 + " " + a.Usuario.Apellido2,
                        Id = a.ArchivoId
                    } :
                from a in db.Archivo
                from t in db.Tipos
                where t.TipoId == filtro
                select new
                {
                    Nombre = a.Nombre,
                    Tipo = t.Nombre,
                    Usuario = a.Usuario.Cedula + " " + a.Usuario.Nombre1 + " " + a.Usuario.Nombre2 + " " + a.Usuario.Apellido1 + " " + a.Usuario.Apellido2,
                    Id = a.ArchivoId
                };

            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerUsuarios(int select, string role)
        {
            switch (select)
            {
                case 1:
                    {
                        var list = from u in db.Users
                                   from r in db.Roles
                                   where u.Roles.FirstOrDefault().RoleId.Equals(r.Id)
                                   select new
                                   {
                                       Cedula = u.Cedula,
                                       Nombre1 = u.Nombre1,
                                       Nombre2 = u.Nombre2,
                                       Apellido1 = u.Apellido1,
                                       Apellido2 = u.Apellido2,
                                       Role = r.Name
                                   };
                        
                        return Json(list.ToList(), JsonRequestBehavior.AllowGet);
                    }
                case 2: case 3:
                    {
                        var list = from u in db.Users
                                   from r in db.Roles
                                   where (u.Roles.FirstOrDefault().RoleId == "5" || u.Roles.FirstOrDefault().RoleId == "6" || u.Roles.FirstOrDefault().RoleId == "7") && u.Roles.FirstOrDefault().RoleId.Equals(r.Id)
                                   select new
                                   {
                                       Cedula = u.Cedula,
                                       Nombre1 = u.Nombre1,
                                       Nombre2 = u.Nombre2,
                                       Apellido1 = u.Apellido1,
                                       Apellido2 = u.Apellido2,
                                       Role = r.Name
                                   };
                        return Json(list.ToList(), JsonRequestBehavior.AllowGet);
                    }
                case 4:
                    {
                        if (role == "Entrenador") {
                            var entrenador = from u in db.Users
                                             where u.Id == HttpContext.User.Identity.GetUserId()
                                             select new
                                             {
                                                 Cedula = u.Cedula,
                                                 Nombre1 = u.Nombre1,
                                                 Nombre2 = u.Nombre2,
                                                 Apellido1 = u.Apellido1,
                                                 Apellido2 = u.Apellido2,
                                                 Role = "Entrenador"
                                             };
                            return Json(entrenador, JsonRequestBehavior.AllowGet);
                        }
                        else {
                            var list = from u in db.Users
                                       from r in db.Roles
                                       where u.Roles.FirstOrDefault().RoleId == "4" && u.Roles.FirstOrDefault().RoleId.Equals(r.Id)
                                       select new {
                                           Cedula = u.Cedula,
                                           Nombre1 = u.Nombre1,
                                           Nombre2 = u.Nombre2,
                                           Apellido1 = u.Apellido1,
                                           Apellido2 = u.Apellido2,
                                           Role = r.Name
                                       };
                            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
                        }
                    }

                    /*  ************************** Actividades
                case 5:
                    {
                        var list = db.Users.Where(x => x.Roles.Select(y => y.RoleId == "5" || y.RoleId == "6" || y.RoleId == "7").FirstOrDefault()).ToList();
                        return Json(list, JsonRequestBehavior.AllowGet);
                    }*/
                case 6:
                    {
                        var selex = from u in db.Users
                                    from s in db.Selecciones
                                    where
                                    u.Id.Equals(s.Usuario.Id)
                                    select new
                                    {
                                        Cedula = u.Cedula,
                                        Nombre1 = u.Nombre1,
                                        Nombre2 = u.Nombre2,
                                        Apellido1 = u.Apellido1,
                                        Apellido2 = u.Apellido2,
                                        Entidad = s.Nombre_Seleccion,
                                        Role = "Seleccion/Federacion"
                                    };

                        var asox = from u in db.Users
                                   from a in db.Asociacion_Deportiva
                                   where
                                   u.Id.Equals(a.Usuario.Id)
                                   select new
                                   {
                                       Cedula = u.Cedula,
                                       Nombre1 = u.Nombre1,
                                       Nombre2 = u.Nombre2,
                                       Apellido1 = u.Apellido1,
                                       Apellido2 = u.Apellido2,
                                       Entidad = a.Nombre_DepAso,
                                       Role = "Asociacion/Comite"
                                   };

                        var list = Enumerable.Union(selex, asox).ToList();


                        return Json(list, JsonRequestBehavior.AllowGet);
                    }

                default:
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
            }
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
    }
}