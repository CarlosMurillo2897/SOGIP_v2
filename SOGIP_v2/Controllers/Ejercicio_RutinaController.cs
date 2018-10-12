using SOGIP_v2.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
namespace SOGIP_v2.Controllers
{
    public class Ejercicio_RutinaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Create()
        {
            var getAtletas = db.Users.ToList();
            SelectList listaAtletas = new SelectList(getAtletas, "Id", "Nombre1");
            ViewBag.Atletas = listaAtletas;

            return View();
        }

        [HttpPost]
        public ActionResult Create(string Atleta_Proveniente_Del_Html_Y_Ya_Seleccionado, Rutina rutina_Proveniente_De_Los_Datos_Del_Html)
        {

            /* Vamos a crear un Usuario nuevo en blanco, el cual reemplazaremos con un Usuario extraído de la base de datos */
            ApplicationUser user = new ApplicationUser();

            /* Vamos a buscar al usuario en la base de datos, a través del Id que trajimos desde el .html */
            // db = Base de datos
            // Users = Tabla de donde lo extrae.
            // Single = El primero que encuentra (solo existe alguien con ese Id de por sí jaja)
            // x => x.Id == Atleta_Proveniente_Del_Html_Y_Ya_Seleccionado);
            // Esto es un lambda *Investigar sobre Lambda*, es como decir 
            // Encuentreme un x tal que el Id de esa x sea igual al número de cédula de Adriana.

            user = db.Users.Single(x => x.Id == Atleta_Proveniente_Del_Html_Y_Ya_Seleccionado);

            // Si se encuentra el Usuario
            if (user != null)
            {
                // Declaramos una nueva Rutina, a la cual le vamos a pasar los datos provenientes del .html y se la entregamos al Modelo directamente.
                // Ubicamos en Create.cshtml la siguiente línea:
                // @Html.TextBoxFor(model => model.RutinaObservaciones, new { @class = "control-label" })
                // Significa que estamos metiendo en el modelo en la RutinaObservaciones lo que sea que digite el Usuario en este campo.

                Rutina rutina = new Rutina()
                {
                    Usuario = user,

                    // Por lo que, cuando llamamos al rutina_Proveniente_De_Los_Datos_Del_Html y le ponemos el .RutinaObservaciones estamos
                    // pidiendo los datos que ya se ingresaron por Adriana, Carlos, Jean Pierre o Lisandra en el .html
                    RutinaFecha = rutina_Proveniente_De_Los_Datos_Del_Html.RutinaFecha,

                    // Es una facilidad de Razor, tomar los datos que establecemos del modelo y pasarlos al Controller directamente.
                    RutinaObservaciones = rutina_Proveniente_De_Los_Datos_Del_Html.RutinaObservaciones
                };

                // Agregamos la rutina.
                db.Rutinas.Add(rutina);
                db.SaveChanges();

            }

            /* Como volveremos a cargar la misma página tenemos que enviar nuevamente los usuarios para no provocar un error. */
            var getAtletas = db.Users.ToList();
            SelectList listaAtletas = new SelectList(getAtletas, "Id", "Nombre1");
            ViewBag.Atletas = listaAtletas;

            // Se asume que todo sucedió bien y nos devuelve a la misma página, en caso contrario nos tiraría un error.
            return View();
        }
    }
}