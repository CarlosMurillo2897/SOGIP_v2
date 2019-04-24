using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SOGIP_v2.Models;

namespace SOGIP_v2.Controllers
{
    [Authorize(Roles = "Administrador,Supervisor")]
    public class EstadosPagosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public EstadosPagosController(){ }

        public EstadosPagosController(ApplicationUserManager userManager)
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
            return View();
        }

        public JsonResult GetPagos()
        {
            try
            {
                var query = (from t in db.EstadosPagos
                            group t by new { Mes = t.FechaPago.Month, Año = t.FechaPago.Year } into g
                             select new
                             {
                               Month = g.Key.Mes,
                               Year = g.Key.Año,
                               Count = g.Count()
                             }).ToList();

                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PagoUsuarios(int mes, int year)
        {
            try
            {
                var users = (from u in db.Users
                             from r in db.Roles
                             where u.Roles.FirstOrDefault().RoleId == r.Id
                             select new { Id = u.Id, Nombre = u.Cedula + " - " + u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2,
                             Rol = r.Name }).ToList();

                List<object> payments = new List<object>();

                foreach (var us in users)
                {
                    EstadosPagos pay = db.EstadosPagos
                        .Where(p => p.Usuario.Id == us.Id && p.FechaPago.Month == mes && p.FechaPago.Year == year)
                        .Include("TipoPago")
                        .Include("Estado")
                        .FirstOrDefault();
                    if (pay != null)
                    {
                        object obj = new
                        {
                            Fecha = pay.FechaPago.ToString("yyyy/MM/dd"),
                            Cantidad = pay.Cantidad,
                            Monto = pay.Monto,
                            Concepto = pay.Concepto,
                            Estado = pay.Estado.Descripcion,
                            TipoPago = pay.TipoPago.Descripcion,
                            Usuario = us.Nombre,
                            Rol = us.Rol
                        };
                        payments.Add(obj);
                    }
                    else
                    {
                        object obj = new
                        {
                            Fecha = Convert.ToDateTime(year + "/" + mes + "/01").ToString("yyyy/MM/dd"),
                            Cantidad = 0,
                            Monto = 0,
                            Concepto = "NULO",
                            Estado = "SIN PAGAR",
                            TipoPago = "NULO",
                            Usuario = us.Nombre,
                            Rol = us.Rol
                        };
                        payments.Add(obj);
                    }

                }

                return Json(payments, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PagoRepetido(DateTime fecha, string usr)
        {
            bool existe = !db.EstadosPagos.Any(p => p.Usuario.Cedula == usr && p.FechaPago.Month == fecha.Month && p.FechaPago.Year == fecha.Year);
            return Json(existe, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarPago(EstadosPagos pago, string usr, int TipoPago, int Estado)
        {
            try
            {
                ApplicationUser u = db.Users.Where(n => n.Cedula == usr).FirstOrDefault();
                pago.Usuario = u;
                pago.TipoPago = db.TipoPago.Where(tp => tp.Id == TipoPago).FirstOrDefault();
                pago.Estado = db.Estados.Where(e => e.EstadoId == Estado).FirstOrDefault();
                db.EstadosPagos.Add(pago);

                var userRoles = UserManager.GetRoles(u.Id);

                if (userRoles.FirstOrDefault() == "Seleccion/Federacion" || userRoles.FirstOrDefault() == "Asociacion/Comite") { 
                    float total = 0;
                    List<ApplicationUser> aths = new List<ApplicationUser>();

                    if (pago.Monto != 0 && pago.Cantidad < pago.Monto && pago.Cantidad != 0) { total = pago.Monto / pago.Cantidad; }
                    if (userRoles.FirstOrDefault() == "Seleccion/Federacion")
                    {
                        aths = db.Atletas.Where(a => a.SubSeleccion.Seleccion.Usuario.Cedula == usr).Select(r => r.Usuario).ToList();
                        List<ApplicationUser> train = db.SubSeleccion.Where(s => s.Seleccion.Usuario.Cedula == usr && s.Entrenador != null).Select(h => h.Entrenador).ToList();

                        foreach (var t in train)
                        {
                            if (t != null && !db.EstadosPagos.Any(p => p.Usuario.Cedula == t.Cedula && p.FechaPago.Month == pago.FechaPago.Month && p.FechaPago.Year == pago.FechaPago.Year)) {
                                // Pago individual de Entrenadores
                                EstadosPagos pgI = new EstadosPagos
                                {
                                    Estado = pago.Estado,
                                    Cantidad = 1,
                                    Concepto = pago.Concepto,
                                    FechaPago = pago.FechaPago,
                                    Monto = total,
                                    TipoPago = pago.TipoPago,
                                    Usuario = t
                                };
                                db.EstadosPagos.Add(pgI);
                            }
                        }
                    }
                    if (userRoles.FirstOrDefault() == "Asociacion/Comite")
                    {
                        aths = db.Atletas.Where(a => a.Asociacion_Deportiva.Usuario.Cedula == usr).Select(r => r.Usuario).ToList();
                    }
                    foreach (var x in aths)
                    {
                        if (x != null && !db.EstadosPagos.Any(p => p.Usuario.Cedula == x.Cedula && p.FechaPago.Month == pago.FechaPago.Month && p.FechaPago.Year == pago.FechaPago.Year))
                        {
                            // Pago individual
                            EstadosPagos pgI = new EstadosPagos
                            {
                                Estado = pago.Estado,
                                Cantidad = 1,
                                Concepto = pago.Concepto,
                                FechaPago = pago.FechaPago,
                                Monto = total,
                                TipoPago = pago.TipoPago,
                                Usuario = x
                            };
                            db.EstadosPagos.Add(pgI);
                        }
                    }

                }

                db.SaveChanges();
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return Json(pago, JsonRequestBehavior.AllowGet);
        }

    }
}