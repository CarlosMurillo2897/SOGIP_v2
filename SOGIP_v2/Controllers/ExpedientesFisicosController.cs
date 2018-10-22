using SOGIP_v2.Models;
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

        // GET: ExpedientesFisicos
        public ActionResult Index()
        {
            // Consulta que obtiene la cédula, el primer y segundo nombre y el primer y segundo apellido de los atletas en la BD.
            var consulta = from a in db.Atletas
                           from u in db.Users
                           where u.Id.Equals(a.Usuario.Id)
                           orderby u.Nombre1 ascending
                           select new
                           {
                               idAtleta = a.AtletaId,
                               cedNomCompleto = u.Cedula + " - " + u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2
                           };
            var getAtletas = consulta.ToList();
            SelectList listaAtletas = new SelectList(getAtletas, "idAtleta", "cedNomCompleto");
            ViewBag.Atletas = listaAtletas;
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase inbody, HttpPostedFileBase pruFu, int SelectedAthlete)
        {
            // Consulta que obtiene la cédula, el primer y segundo nombre y el primer y segundo apellido de los atletas en la BD.
            var consulta = from a in db.Atletas
                           from u in db.Users
                           where u.Id.Equals(a.Usuario.Id)
                           orderby u.Nombre1 ascending
                           select new
                           {
                               idAtleta = a.AtletaId,
                               cedNomCompleto = u.Cedula + " - " + u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2
                           };
            var getAtletas = consulta.ToList();
            SelectList listaAtletas = new SelectList(getAtletas, "idAtleta", "cedNomCompleto");
            ViewBag.Atletas = listaAtletas;

            // var id = db.Atletas.Single(x => x.Usuario.Id==SelectedAthlete);

            if (inbody == null || inbody.ContentLength == 0)
            {
                ViewBag.Error1 = "No se seleccionó ningún archivo o el archivo InBody está vacío<br/>";
                if (pruFu == null || pruFu.ContentLength == 0)
                {
                    ViewBag.Error2 = "No se seleccionó ningún archivo o el archivo de la Prueba de Fuerza está vacío<br/>";
                    return View();
                }
                else
                {
                    ViewBag.Error2 = "No se puede subir la Prueba de Fuerza si el archivo InBody no se encuentra<br/>";
                    return View();
                }
            }
            else
            {
                if (inbody.FileName.EndsWith("pdf"))
                {
                    int fin;
                    string terminacion;

                    fin = inbody.FileName.Length - 4;
                    terminacion = ").pdf";

                    string name = inbody.FileName.Substring(0, fin);
                    string path = Server.MapPath("~/Content/Registros/InBodys/" + name +
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

                    if (pruFu == null || pruFu.ContentLength == 0)
                    {
                        // Se instancia un objeto de la clase ExpedienteFisico.
                        ExpedienteFisico expFis = new ExpedienteFisico();

                        // Se le asigna un valor al campo InBody del Expediente Físico. Mientras que, el campo PruebaFuerza, queda nulo, pues no se importó ningún archivo o el archivo importado está vacío.
                        byte[] tempFile = new byte[inbody.ContentLength];
                        inbody.InputStream.Read(tempFile, 0, inbody.ContentLength);
                        expFis.InBody = tempFile;
                        expFis.PruebaFuerza = null;

                        // Se le asigna un valor al campo Atleta del Expediente Físico
                        expFis.Atleta = db.Atletas.Single(x => x.AtletaId == SelectedAthlete);

                        db.Expedientes_Fisicos.Add(expFis);
                        db.SaveChanges();

                        return View("Success");
                    }
                    else
                    {
                        if (pruFu.FileName.EndsWith("docx"))
                        {
                            fin = pruFu.FileName.Length - 5;
                            terminacion = ").docx";

                            name = pruFu.FileName.Substring(0, fin);
                            path = Server.MapPath("~/Content/Registros/Pruebas de Fuerza/" + name +
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

                            // Se instancia un objeto de la clase ExpedienteFisico.
                            ExpedienteFisico expFis = new ExpedienteFisico();

                            // Se le asigna un valor al campo InBody y PruebaFuerza del Expediente Físico.
                            byte[] tempFile1 = new byte[inbody.ContentLength];
                            inbody.InputStream.Read(tempFile1, 0, inbody.ContentLength);
                            expFis.InBody = tempFile1;

                            byte[] tempFile2 = new byte[pruFu.ContentLength];
                            pruFu.InputStream.Read(tempFile2, 0, pruFu.ContentLength);
                            expFis.PruebaFuerza = tempFile2;

                            // Se le asigna un valor al campo Atleta del Expediente Físico
                            expFis.Atleta = db.Atletas.Single(x => x.AtletaId == SelectedAthlete);

                            db.Expedientes_Fisicos.Add(expFis);
                            db.SaveChanges();

                            return View("Success2");
                        }
                        else
                        {
                            ViewBag.Error = "El archivo InBody es correcto, pero el formato del archivo de la Prueba de Fuerza es incorrecto. Por favor, inténtelo de nuevo<br/>";
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Error1 = "El formato del archivo importado es incorrecto<br/>";
                    ViewBag.Error2 = "No se puede subir la Prueba de Fuerza si el archivo InBody es incorrecto<br/>";
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Regresar()
        {
            return View("Index");
        }
    }
}