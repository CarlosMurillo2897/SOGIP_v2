using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SOGIP_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    public class AtletasAsignadosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        public AtletasAsignadosController() { }

        public AtletasAsignadosController(ApplicationUserManager userManager)
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

        // GET: AtletasAsignados
        [Authorize(Roles = "Administrador,Entrenador,Seleccion/Federacion,Asociacion/Comite")]
        public ActionResult Index()
        {
            string userid = HttpContext.User.Identity.GetUserId();
            var userRoles = UserManager.GetRoles(userid);
            ViewBag.Role = userRoles.First();
            return View();
        }

        public JsonResult GetAtletasAsociacion(string usuarioId)
        {
            var consulta = (from a in db.Atletas
                           where a.Asociacion_Deportiva.Usuario.Id == usuarioId
                           select new
                           {
                               Cedula = a.Usuario.Cedula,
                               Nombre = a.Usuario.Nombre1 + " " + a.Usuario.Nombre2,
                               Apellido1 = a.Usuario.Apellido1,
                               Apellido2 = a.Usuario.Apellido2,
                               Id = a.Usuario.Id
                           }).ToList();
            var aux = consulta;
            return Json(consulta, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult GetUsuariosSeleccion(string usuarioId)
        {
                var consulta = from a in db.Atletas
                               from sub in db.SubSeleccion
                               from c in db.Categorias
                               where (sub.Seleccion.Usuario.Id == usuarioId && a.SubSeleccion.SubSeleccionId == sub.SubSeleccionId && sub.Categoria_Id.CategoriaId == c.CategoriaId)
                               select new
                               {
                                   Cedula = a.Usuario.Cedula,
                                   Nombre = a.Usuario.Nombre1 + " " + a.Usuario.Nombre2,
                                   Apellido1 = a.Usuario.Apellido1,
                                   Apellido2 = a.Usuario.Apellido2,
                                   Id = a.Usuario.Id,
                                   Seleccion = sub.Seleccion.Nombre_Seleccion,
                                   Rol = "Atleta",
                                   Categoria = c.Descripcion
                               };

            var traineers = from sub in db.SubSeleccion
                           from u in db.Users
                           from c in db.Categorias
                           where 
                           sub.Seleccion.Usuario.Id == usuarioId 
                           && sub.Entrenador != null 
                           && sub.Entrenador.Id == u.Id
                           && sub.Categoria_Id.CategoriaId == c.CategoriaId
                           select new
                           {
                               Cedula = u.Cedula,
                               Nombre = u.Nombre1 + " " + u.Nombre2,
                               Apellido1 = u.Apellido1,
                               Apellido2 = u.Apellido2,
                               Id = u.Id,
                               Seleccion = sub.Seleccion.Nombre_Seleccion,
                               Rol = "Entrenador",
                               Categoria = c.Descripcion
                           };

            var list = Enumerable.Union(consulta, traineers).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsuariosEntrenador(string usuarioId)
        {
                var consulta = from a in db.Atletas
                               from sub in db.SubSeleccion
                               from c in db.Categorias
                               where (a.SubSeleccion.Entrenador.Id == usuarioId && sub.SubSeleccionId == a.SubSeleccion.SubSeleccionId && sub.Categoria_Id.CategoriaId == c.CategoriaId)
                               select new
                               {
                                   Cedula = a.Usuario.Cedula,
                                   Nombre = a.Usuario.Nombre1 + " " + a.Usuario.Nombre2,
                                   Apellido1 = a.Usuario.Apellido1,
                                   Apellido2 = a.Usuario.Apellido2,
                                   Id = a.Usuario.Id,
                                   Seleccion = sub.Seleccion.Nombre_Seleccion,
                                   Categoria = c.Descripcion
                               };
            var aux = consulta.ToList();
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAtletasAdministrador(string usuarioId)
        {
            var consulta = (from f in db.Funcionario_ICODER
                           where f.Entrenador.Id == usuarioId
                           select new
                           {
                               Cedula = f.Usuario.Cedula,
                               Nombre = f.Usuario.Nombre1 + " " + f.Usuario.Nombre2,
                               Apellido1 = f.Usuario.Apellido1,
                               Apellido2 = f.Usuario.Apellido2,
                               Id = f.Usuario.Id
                           }).ToList();
            var aux = consulta;
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }

    }
}
