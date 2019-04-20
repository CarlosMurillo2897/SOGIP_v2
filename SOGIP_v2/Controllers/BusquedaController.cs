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
        public async Task<ActionResult> Index()
        {
            var usuarios = await UserManager.Users.ToListAsync();
            var usuarios2 = (from u in db.Users
                             orderby u.Fecha_Expiracion descending
                             select u
                             ).Take(10).ToList();

            foreach (var usuario in usuarios2)
            {
                var rol = await UserManager.GetRolesAsync(usuario.Id);
                ViewData[usuario.Id] = rol.First();
            }

            return View(usuarios2);
        }

        public JsonResult Buscar(string SearchBy, string SearchValue)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            if (SearchBy == "0")
            {
                try
                {
                    // int Id = Convert.ToInt32(SearchValue);
                    users = db.Users.Where(x => x.UserName == SearchValue || SearchValue == null).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} No es una cédula");
                }
                return Json(users, JsonRequestBehavior.AllowGet);
            }
            if(SearchBy == "1")
            {
                users = db.Users.Where(x => x.Nombre1.Contains(SearchValue) || SearchValue == null).ToList();
                return Json(users, JsonRequestBehavior.AllowGet);
            }
            else
            {
                users = db.Users.Where(x => x.Apellido1.Contains(SearchValue) || SearchValue == null).ToList();
                return Json(users, JsonRequestBehavior.AllowGet);
            }
        }
    }
}