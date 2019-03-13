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
                                   Categoria = c.Descripcion
                                   // Categoria = sub.Categoria_Id.Descripcion
                               };
            var aux = consulta.ToList();
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsuariosEntrenador(string usuarioId)
        {
                var consulta = from a in db.Atletas
                               where (a.SubSeleccion.Entrenador.Id == usuarioId)
                               select new
                               {
                                   Cedula = a.Usuario.Cedula,
                                   Nombre = a.Usuario.Nombre1 + " " + a.Usuario.Nombre2,
                                   Apellido1 = a.Usuario.Apellido1,
                                   Apellido2 = a.Usuario.Apellido2,
                                   Id = a.Usuario.Id
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
