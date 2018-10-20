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

        /*
        [HttpPost]
        public ActionResult Add(HttpPostedFileBase uploadFile, int UserID)
        {
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {

                //instance the user.. i.e "User1" 

                byte[] tempFile = new byte[uploadFile.ContentLength];
                uploadFile.InputStream.Read(tempFile, 0, uploadFile.ContentLength);

                User1.file.Content = tempFile;
                User1.file.Save();

            }

            return RedirectToAction("Index");
        }
        */

        [HttpPost]
        public ActionResult Regresar()
        {
            return View("Index");
        }
    }
}