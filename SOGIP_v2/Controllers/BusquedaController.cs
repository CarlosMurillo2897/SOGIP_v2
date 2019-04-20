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

    }
}