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
            var consulta = //from a in db.Atletas
                           from u in db.Users
                           from f in db.Funcionario_ICODER
                           //where u.Id.Equals(a.Usuario.Id)
                           where u.Id.Equals(f.Usuario.Id)
                           orderby u.Nombre1 ascending
                           select new
                           {
                               idAtleta = u.Id,
                               cedNomCompleto = u.Cedula + " - " + u.Nombre1 + " " + u.Apellido1 + " " + u.Apellido2
                           };
            var getAtletas = consulta.ToList();
            var atletas = db.Users.Select(x => x.Roles.Where( y => y.RoleId == "4") );
            SelectList listaAtletas = new SelectList(getAtletas, "idAtleta", "cedNomCompleto");
            ViewBag.Atletas = listaAtletas;
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase inbody, HttpPostedFileBase pruFu, string SelectedAthlete)
        {
            // Consulta que obtiene la cédula, el primer y segundo nombre y el primer y segundo apellido de los atletas en la BD.
            var consulta = //from a in db.Atletas
                           from u in db.Users
                           from f in db.Funcionario_ICODER
                               //where u.Id.Equals(a.Usuario.Id)
                           where u.Id.Equals(f.Usuario.Id)
                           orderby u.Nombre1 ascending
                           select new
                           {
                               idAtleta = u.Id,
                               cedNomCompleto = u.Cedula + " - " + u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2
                           };

            var getAtletas = consulta.ToList();
            SelectList listaAtletas = new SelectList(getAtletas, "idAtleta", "cedNomCompleto");
            ViewBag.Atletas = listaAtletas;


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
                if (inbody != null)
                {
                    BinaryReader br = new BinaryReader(inbody.InputStream);
                    byte[] buffer = br.ReadBytes(inbody.ContentLength);

                    Archivo file = new Archivo()
                    {
                        Nombre = inbody.FileName,
                        Extension = Path.GetExtension(inbody.FileName),
                        Tipo = inbody.ContentType,
                        Contenido = buffer,
                        Usuario = db.Users.Single(x => x.Id == SelectedAthlete)
                    };
                    db.Archivo.Add(file);
                }

                if (pruFu != null)
                {
                    BinaryReader br = new BinaryReader(pruFu.InputStream);
                    byte[] buffer = br.ReadBytes(pruFu.ContentLength);

                    Archivo pF = new Archivo()
                    {
                        Nombre = pruFu.FileName,
                        Extension = Path.GetExtension(pruFu.FileName),
                        Tipo = pruFu.ContentType,
                        Contenido = buffer,
                        Usuario = db.Users.Single(x => x.Id == SelectedAthlete)
                    };
                    db.Archivo.Add(pF);
                }
                db.SaveChanges();

                return View("Success");
            }
        }

        [HttpPost]
        public ActionResult Regresar()
        {
            return View("Index");
        }
    }
}