using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOGIP_v2.Models;

namespace SOGIP_v2.Controllers
{
    public class EstadosPagosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TipoPago()
        {
            return View();
        }
        public JsonResult SaveTipo(string descripcion)
        {
            TipoPago tipo = new TipoPago();
            try
            {
                if (tipo != null)
                {
                    tipo.Descripcion = descripcion;
                    db.TipoPago.Add(tipo);

                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(tipo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveEstado(string usuario, DateTime fecha,int cantidad,int monto,int mensualidad)
        {
            EstadosPagos nueva = new EstadosPagos();
            try
            {
                ApplicationUser User = db.Users.Single(x => x.Id == usuario);
                TipoPago Tipo = db.TipoPago.Single(x => x.Id == mensualidad);
                if (User != null || Tipo != null)
                {
                    nueva.Usuario = User;
                    nueva.FechaPago = fecha;
                    nueva.Cantidad = cantidad;
                    nueva.Monto = monto;
                    nueva.Total = cantidad * monto;
                    nueva.Estado = "Pendiente";
                    nueva.IdPago = Tipo;
                    nueva.FechaProxima = fecha;
                    db.EstadosPagos.Add(nueva);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(nueva, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRoles()
        {
            var roles = (from f in db.Roles
                         where f.Id == "3"
                         select new
                            {
                                Id = f.Id,
                                Name = f.Name
                            }).ToList();
            var aux = roles;
            return Json(aux, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEstados()
        {
            var roles = (from f in db.EstadosPagos
                         from u in db.Selecciones
                         where(f.Usuario.Id == u.Usuario.Id)
                         select new
                         {
                           Usuario = u.Nombre_Seleccion,
                           Fecha = f.FechaPago,
                           Cantidad = f.Cantidad,
                           Monto = f.Monto,
                           Total = f.Total,
                           Estado = f.Estado,
                           TipoPago = f.IdPago.Descripcion,
                           Id = f.Id

                         }).ToList();
            var aux = roles;
            return Json(aux, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMensualidad()
        {
            var roles = (from f in db.TipoPago
                         select new
                         {
                             Id = f.Id,
                             Name = f.Descripcion
                         }).ToList();
            var aux = roles;
            return Json(aux, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsuarios(string n)
        {
            var consulta = (from f in db.Selecciones
                            select new
                            {
                                Id = f.Usuario.Id,
                                Name = f.Nombre_Seleccion
                            }).ToList();

            //switch (n)
            //{
            //    case "3":
            //        consulta = (from f in db.Selecciones
            //                        select new
            //                        {
            //                            Id = f.Usuario.Id,
            //                            Name = f.Nombre_Seleccion
            //                        }).ToList();
            //        break;
            //    case "9":
            //         consulta = (from f in db.Asociacion_Deportiva
            //                    select new
            //                    {
            //                        Id = f.Usuario.Id,
            //                        Name = f.Nombre_DepAso
            //                    }).ToList();
            //        break;
            //    case "5":
            //        consulta = (from f in db.Atletas
            //                    select new
            //                    {
            //                        Id = f.Usuario.Id,
            //                        Name = f.Usuario.Nombre1 + " " +f.Usuario.Apellido1 +" "+f.Usuario.Apellido2
            //                    }).ToList();
            //        break;
            //    case "8":
            //        consulta = (from f in db.Entidad_Publica
            //                    select new
            //                    {
            //                        Id = f.Usuario.Id,
            //                        Name = f.Usuario.Nombre1 + " " + f.Usuario.Apellido1 + " " + f.Usuario.Apellido2
            //                    }).ToList();
            //        break;
            //}
            var aux = consulta;
            return Json(aux, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Pagos(int? id)
        {
            EstadosPagos estados = db.EstadosPagos.Include("Usuario").SingleOrDefault(x => x.Id == id);
            Seleccion sel = db.Selecciones.Include("Usuario").SingleOrDefault(x=>x.Usuario.Id == estados.Usuario.Id);
            if (estados != null)
            {
                string nombre = sel.Nombre_Seleccion;
                ViewData["nombre"] = nombre;
                string n = id.ToString();
                ViewData["pago"] = n;

            }

            return View();

        }
        public JsonResult fechaProxima(string n)
        {
            int id = int.Parse(n);
            EstadosPagos estados = db.EstadosPagos.Include("Usuario").SingleOrDefault(x => x.Id == id);
            var fecha = estados.FechaProxima;
            return Json(fecha, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveListaPagos(string n,string Fecha)
        {
            int id = int.Parse(n);
            EstadosPagos estados = db.EstadosPagos.Include("Usuario").SingleOrDefault(x => x.Id == id);
            ListaPagos lista = new ListaPagos();
            lista.IdEsPago = estados;
            lista.Fecha = Convert.ToDateTime(Fecha);
            estados.FechaProxima = estados.FechaProxima.AddMonths(1);
            db.ListaPagos.Add(lista);
            db.SaveChanges();
            
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListas(string n)
        {
            int id = int.Parse(n);
            var lista = (from f in db.ListaPagos
                         where f.IdEsPago.Id == id
                         select new
                         {
                             Fecha = f.Fecha
                         }).ToList();
            var aux = lista;
            return Json(aux, JsonRequestBehavior.AllowGet);

        }
    }
}