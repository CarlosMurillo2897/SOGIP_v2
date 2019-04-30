using Microsoft.AspNet.Identity.Owin;
using SOGIP_v2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    [Authorize(Roles = "Administrador,Supervisor")]
    public class BusquedaController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public BusquedaController()
        {
        }

        public BusquedaController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
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
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;

            }
        }

        // GET: Busqueda
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getActividades()
        {
            var list = from a in db.Actividad
                       from t in db.Horario
                       from c in db.Archivo
                       where t.IdActividad == a
                       && c.actividad.Id == a.Id
                       select new
                       {
                           a.Titulo,
                           a.Lugar,
                           a.Descripcion,
                           t.FechaHoraInicio,
                           t.FechaHoraFinal,
                           c.ArchivoId
                       };

            return Json(list.ToList(), JsonRequestBehavior.AllowGet);

        public JsonResult getActividades()
        {
            var list = from a in db.Actividad
                              from t in db.Horario
                              where t.IdActividad == a
                              select new
                              {
                                  a.Titulo,
                                  a.Lugar,
                                  a.Descripcion,
                                  t.FechaHoraInicio,
                                  t.FechaHoraFinal
                              };

            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}