using SOGIP_v2.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOGIP_v2.Controllers
{
    public class ExpedientesFisicosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExpedientesFisicos
        public ActionResult Index()
        {
            // ***********************
            // De momento, es con los usuarios, pero hay que cambiarlo a solo los atletas.
            var getAtletas = db.Users.ToList();
            SelectList listaAtletas = new SelectList(getAtletas, "Id", "Nombre1");
            ViewBag.Atletas = listaAtletas;
            // ***********************
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase inbody, HttpPostedFileBase pruFu, string SelectedAthlete)
        {
            // ***********************
            // De momento, es con los usuarios, pero hay que cambiarlo a solo los atletas.
            var getAtletas = db.Users.ToList();
            SelectList listaAtletas = new SelectList(getAtletas, "Id", "Nombre1");
            ViewBag.Atletas = listaAtletas;
            // ***********************

            // var id = db.Atletas.Single(x => x.Usuario.Id==SelectedAthlete);

            if (inbody == null || inbody.ContentLength == 0)
            {
                ViewBag.Error1 = "Seleccione un archivo InBody para cargar los datos<br/>";
                return View();
            }
            if (pruFu == null || pruFu.ContentLength == 0)
            {
                ViewBag.Error2 = "Seleccione un archivo de Prueba de Fuerza para cargar los datos<br/>";
                return View();
            }
            if (inbody.FileName.EndsWith("pdf"))
            {

                int fin;
                string terminacion;

                fin = inbody.FileName.Length - 4;
                terminacion = ").pdf";

                string name = inbody.FileName.Substring(0, fin);
                string path = Server.MapPath("~/Content/Registros/InBody/" + name +
                                             "(" + DateTime.Now.Year.ToString() + "-"
                                             + DateTime.Now.Month.ToString() + "-"
                                             + DateTime.Now.Day.ToString() + ")-("
                                             + DateTime.Now.Hour.ToString() + "-"
                                             + DateTime.Now.Minute.ToString() + "-"
                                             + DateTime.Now.Second.ToString() + terminacion);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                inbody.SaveAs(path);

                return View("Success");
            }
            if (pruFu.FileName.EndsWith("pdf"))
            {

                int fin;
                string terminacion;

                fin = pruFu.FileName.Length - 4;
                terminacion = ").pdf";

                string name = pruFu.FileName.Substring(0, fin);
                string path = Server.MapPath("~/Content/Registros/PF/" + name +
                                             "(" + DateTime.Now.Year.ToString() + "-"
                                             + DateTime.Now.Month.ToString() + "-"
                                             + DateTime.Now.Day.ToString() + ")-("
                                             + DateTime.Now.Hour.ToString() + "-"
                                             + DateTime.Now.Minute.ToString() + "-"
                                             + DateTime.Now.Second.ToString() + terminacion);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                pruFu.SaveAs(path);

                return View("Success");
            }
            else
            {            
                    ViewBag.Error = "El tipo de archivo no es aceptado. <br>";
                    return View("Index");
            }
        }
    

        [HttpPost]
        public ActionResult Regresar()
        {
            return View("Index");
        }
    }
}