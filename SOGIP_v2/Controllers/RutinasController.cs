using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOGIP_v2.Models;
using System.Text.RegularExpressions;
namespace SOGIP_v2.Controllers
{
    public class RutinasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rutinas
        public ActionResult Index()
        {
            var rutina = db.Rutinas.ToList();
            return View(db.Rutinas.Include("Usuario"));
        }
    
        // GET: Rutinas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutina rutina = db.Rutinas.Find(id);
            if (rutina == null)
            {
                return HttpNotFound();
            }
            return View(rutina);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        public JsonResult getUsuariosF()
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
            return Json(getAtletas, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getUsuariosA()
        {

            // Consulta que obtiene la cédula, el primer y segundo nombre y el primer y segundo apellido de los atletas en la BD.
            var consulta = //from a in db.Atletas
                           from u in db.Users
                           from f in db.Atletas
                               //where u.Id.Equals(a.Usuario.Id)
                           where u.Id.Equals(f.Usuario.Id)
                           orderby u.Nombre1 ascending
                           select new
                           {
                               idAtleta = u.Id,
                               cedNomCompleto = u.Cedula + " - " + u.Nombre1 + " " + u.Apellido1 + " " + u.Apellido2
                           };

            var getAtletas = consulta.ToList();
            return Json(getAtletas, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Ejercicio(int? idRutina, string idUsuario)
        {
            if (idRutina != null)
            {
                Rutina rutina = db.Rutinas.Include("Usuario").SingleOrDefault(x=>x.RutinaId ==idRutina);
                int i = rutina.RutinaId;
                string n = i.ToString();
                ViewData["rutina"] = n;
                string nombre = rutina.Usuario.Cedula + " - "+rutina.Usuario.Nombre1 +" "+ rutina.Usuario.Apellido1 + " " + rutina.Usuario.Apellido2; 
                ViewData["nombre"] = nombre;

                var getEjercicio1 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == idRutina && x.DiaEjercicio == "Dia1").ToList();
                var getEjercicio2 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == idRutina && x.DiaEjercicio == "Dia2").ToList();
                var getEjercicio3 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == idRutina && x.DiaEjercicio == "Dia3").ToList();
                var getEjercicio4 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == idRutina && x.DiaEjercicio == "Dia4").ToList();
                var getEjercicio5 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == idRutina && x.DiaEjercicio == "Dia5").ToList();

                ViewBag.Conjunto_Ejercicios1 = (getEjercicio1.Count > 0) ? getEjercicio1 : null;
                ViewBag.Conjunto_Ejercicios2 = (getEjercicio2.Count > 0) ? getEjercicio2 : null;
                ViewBag.Conjunto_Ejercicios3 = (getEjercicio3.Count > 0) ? getEjercicio3 : null;
                ViewBag.Conjunto_Ejercicios4 = (getEjercicio4.Count > 0) ? getEjercicio4 : null;
                ViewBag.Conjunto_Ejercicios5 = (getEjercicio5.Count > 0) ? getEjercicio5 : null;

            }

            if (idUsuario != null)
            {
                Rutina rutina = db.Rutinas.FirstOrDefault(x => x.Usuario.Id == idUsuario);
                int i = rutina.RutinaId;
                string n = i.ToString();
                ViewData["rutina"] = n;

                var getEjercicio1 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.Usuario.Id == idUsuario && x.DiaEjercicio == "Dia1").ToList();
                var getEjercicio2 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.Usuario.Id == idUsuario && x.DiaEjercicio == "Dia2").ToList();
                var getEjercicio3 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.Usuario.Id == idUsuario && x.DiaEjercicio == "Dia3").ToList();
                var getEjercicio4 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.Usuario.Id == idUsuario && x.DiaEjercicio == "Dia4").ToList();
                var getEjercicio5 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.Usuario.Id == idUsuario && x.DiaEjercicio == "Dia5").ToList();

                ViewBag.Conjunto_Ejercicios1 = (getEjercicio1.Count > 0)? getEjercicio1 : null;
                ViewBag.Conjunto_Ejercicios2 = (getEjercicio2.Count > 0)? getEjercicio2 : null;
                ViewBag.Conjunto_Ejercicios3 = (getEjercicio3.Count > 0)? getEjercicio3 : null;
                ViewBag.Conjunto_Ejercicios4 = (getEjercicio4.Count > 0)? getEjercicio4 : null;
                ViewBag.Conjunto_Ejercicios5 = (getEjercicio5.Count > 0)? getEjercicio5 : null;

            }
            

            return View();

        }
        //[HttpPost]
        //public ActionResult Ejercicio(string data, Conjunto_Ejercicio ejercicio)
        //{


        //    int d = int.Parse(data);
        //    Rutina rutina = new Rutina();
        //     rutina = db.Rutinas.Single(x => x.RutinaId == d);

        //    if (rutina != null)
        //    {
        //        Conjunto_Ejercicio conjunto = new Conjunto_Ejercicio()
        //        {
        //            ConjuntoEjercicioRutina = rutina,
        //            NombreEjercicio = ejercicio.NombreEjercicio,
        //            Serie1 = ejercicio.Serie1,
        //            Repeticion1 = ejercicio.Repeticion1,
        //            Peso1 = ejercicio.Peso1,
        //            Serie2 = ejercicio.Serie2,
        //            Repeticion2 = ejercicio.Repeticion2,
        //            Peso2 = ejercicio.Peso2,
        //            Serie3 = ejercicio.Serie3,
        //            Repeticion3 = ejercicio.Repeticion3,
        //            Peso3 = ejercicio.Peso3,
        //            ColorEjercicio = ejercicio.ColorEjercicio,
        //            diaEjercicio = ejercicio.diaEjercicio

        //        };
        //        db.Conjunto_Ejercicios.Add(conjunto);
        //        db.SaveChanges();
        //        var getEjercicio = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == d).ToList();
        //        ViewBag.Conjunto_Ejercicios = getEjercicio;
        //        return Ejercicio(d);

        //    }

        //    return View(ejercicio);
        //}
        public bool estaCorrecto(List<Conjunto_Ejercicio> ejercicios)
        {

            string expresion, expresionNumerica;
            expresionNumerica = "^[0-9,+,=,/]+$";
            expresion = @"(^[a-zA-Z'.\s])";
            System.Text.RegularExpressions.Regex automata = new Regex(expresion);
            System.Text.RegularExpressions.Regex automataNumerico = new Regex(expresionNumerica);
            for (var i = 0; i < ejercicios.Count; i++)
            {
                if (ejercicios[i].NombreEjercicio == null || !automata.IsMatch(ejercicios[i].NombreEjercicio)||
                    ejercicios[i].Serie1 == null || !automataNumerico.IsMatch(ejercicios[i].Serie1)||
                    ejercicios[i].Repeticion1 == null || !automataNumerico.IsMatch(ejercicios[i].Repeticion1) ||
                    ejercicios[i].Peso1 == null || !automataNumerico.IsMatch(ejercicios[i].Peso1) ||
                    ejercicios[i].Serie2 == null || !automataNumerico.IsMatch(ejercicios[i].Serie2) ||
                    ejercicios[i].Repeticion2 == null || !automataNumerico.IsMatch(ejercicios[i].Repeticion2) ||
                    ejercicios[i].Peso2 == null || !automataNumerico.IsMatch(ejercicios[i].Peso2) ||
                    ejercicios[i].Serie3 == null || !automataNumerico.IsMatch(ejercicios[i].Serie3) ||
                    ejercicios[i].Repeticion3 == null || !automataNumerico.IsMatch(ejercicios[i].Repeticion3) ||
                   ejercicios[i].Peso3 == null || !automataNumerico.IsMatch(ejercicios[i].Peso3) ||
                    ejercicios[i].ColorEjercicio == null||ejercicios[i].DiaEjercicio == null)
                {
                    return false;
                }
            }
                return true;
        }
        [HttpPost]
        public JsonResult Ejercicio(string data, List<Conjunto_Ejercicio> ejercicios) //AGREGAR EL ID DE LA RUTINA
        {
            var status = false;
            //Busco el id de la rutina.
            //int d = 5;
            int d = int.Parse(data);
            Rutina rutina = new Rutina();
            rutina = db.Rutinas.Single(x => x.RutinaId == d);
            //Asigno ejercicios a la rutina
            if (rutina != null)
            {
                    for (int i = 0; i < ejercicios.Count; i++)
                    {
                        Conjunto_Ejercicio conjunto = new Conjunto_Ejercicio()
                        {
                            ConjuntoEjercicioRutina = rutina,
                            NombreEjercicio = ejercicios[i].NombreEjercicio,
                            Serie1 = ejercicios[i].Serie1,
                            Repeticion1 = ejercicios[i].Repeticion1,
                            Peso1 = ejercicios[i].Peso1,
                            Serie2 = ejercicios[i].Serie2,
                            Repeticion2 = ejercicios[i].Repeticion2,
                            Peso2 = ejercicios[i].Peso2,
                            Serie3 = ejercicios[i].Serie3,
                            Repeticion3 = ejercicios[i].Repeticion3,
                            Peso3 = ejercicios[i].Peso3,
                            ColorEjercicio = ejercicios[i].ColorEjercicio,
                            DiaEjercicio = ejercicios[i].DiaEjercicio
                        };
                        db.Conjunto_Ejercicios.Add(conjunto);
                    }
                    db.SaveChanges();
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult ListaEjercicio(int ? id, string idUsuario)
        {
          
            if (id != null)
            {
                Rutina rutina = db.Rutinas.Find(id);
                int i = rutina.RutinaId;
                string n = i.ToString();
                ViewData["rutina"] = n;

                var getEjercicio1 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == id && x.DiaEjercicio == "Dia1").ToList();
                var getEjercicio2 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == id && x.DiaEjercicio == "Dia2").ToList();
                var getEjercicio3 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == id && x.DiaEjercicio == "Dia3").ToList();
                var getEjercicio4 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == id && x.DiaEjercicio == "Dia4").ToList();
                var getEjercicio5 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == id && x.DiaEjercicio == "Dia5").ToList();

                ViewBag.Conjunto_Ejercicios1 = (getEjercicio1.Count > 0) ? getEjercicio1 : null;
                ViewBag.Conjunto_Ejercicios2 = (getEjercicio2.Count > 0) ? getEjercicio2 : null;
                ViewBag.Conjunto_Ejercicios3 = (getEjercicio3.Count > 0) ? getEjercicio3 : null;
                ViewBag.Conjunto_Ejercicios4 = (getEjercicio4.Count > 0) ? getEjercicio4 : null;
                ViewBag.Conjunto_Ejercicios5 = (getEjercicio5.Count > 0) ? getEjercicio5 : null;

            }

            if (idUsuario != null)
            {
                Rutina rutina = db.Rutinas.FirstOrDefault(x => x.Usuario.Id == idUsuario);
                if (rutina != null)
                {
                    int i = rutina.RutinaId;
                    string n = i.ToString();
                    ViewData["rutina"] = n;

                    var getEjercicio1 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == i && x.DiaEjercicio == "Dia1").ToList();
                    var getEjercicio2 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == i && x.DiaEjercicio == "Dia2").ToList();
                    var getEjercicio3 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == i && x.DiaEjercicio == "Dia3").ToList();
                    var getEjercicio4 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == i && x.DiaEjercicio == "Dia4").ToList();
                    var getEjercicio5 = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == i && x.DiaEjercicio == "Dia5").ToList();

                    ViewBag.Conjunto_Ejercicios1 = (getEjercicio1.Count > 0) ? getEjercicio1 : null;
                    ViewBag.Conjunto_Ejercicios2 = (getEjercicio2.Count > 0) ? getEjercicio2 : null;
                    ViewBag.Conjunto_Ejercicios3 = (getEjercicio3.Count > 0) ? getEjercicio3 : null;
                    ViewBag.Conjunto_Ejercicios4 = (getEjercicio4.Count > 0) ? getEjercicio4 : null;
                    ViewBag.Conjunto_Ejercicios5 = (getEjercicio5.Count > 0) ? getEjercicio5 : null;
                }
                else
                {
                    string men = "No tiene rutina";
                    ViewData["mensaje"] = men;
                    return View();
                }
            }

            return View();

        }

        public ActionResult DetailsEjercicio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Conjunto_Ejercicio conjunto_Ejercicio = db.Conjunto_Ejercicios.Find(id);
            if (conjunto_Ejercicio == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditEjercicio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conjunto_Ejercicio conjunto_Ejercicio = db.Conjunto_Ejercicios.Include("ConjuntoEjercicioRutina").SingleOrDefault(x => x.Conjunto_EjercicioId == id);
            int n = conjunto_Ejercicio.ConjuntoEjercicioRutina.RutinaId;
            Rutina rutina = db.Rutinas.Include("Usuario").SingleOrDefault(x => x.RutinaId == n);
            if (conjunto_Ejercicio == null)
            {
                return HttpNotFound();
            }

            string nombre = rutina.Usuario.Cedula + " - " + rutina.Usuario.Nombre1 + " " + rutina.Usuario.Apellido1 + " " + rutina.Usuario.Apellido2;

            ViewData["nombre"] = nombre;
            ViewData["idRutina"] = rutina.RutinaId;

            return View(conjunto_Ejercicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEjercicio([Bind(Include = "Conjunto_EjercicioId,NombreEjercicio,Serie1,Repeticion1,Peso1,Serie2,Repeticion2,Peso2,Serie3,Repeticion3,Peso3,ColorEjercicio, diaEjercicio")] Conjunto_Ejercicio conjunto_Ejercicio, int idRutina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conjunto_Ejercicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Ejercicio", new { idRutina} );
            }
            return View(conjunto_Ejercicio);
        }

        // GET: Conjunto_Ejercicio/Delete/5
        public ActionResult DeleteEjercicio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conjunto_Ejercicio conjunto_Ejercicio = db.Conjunto_Ejercicios.Include("ConjuntoEjercicioRutina").SingleOrDefault(x => x.Conjunto_EjercicioId == id);
            int n = conjunto_Ejercicio.ConjuntoEjercicioRutina.RutinaId;
            Rutina rutina = db.Rutinas.Include("Usuario").SingleOrDefault(x => x.RutinaId == n);
            if (conjunto_Ejercicio == null)
            {
                return HttpNotFound();
            }
            string nombre = rutina.Usuario.Cedula + " - " + rutina.Usuario.Nombre1 + " " + rutina.Usuario.Apellido1 + " " + rutina.Usuario.Apellido2;
            ViewData["nombre"] = nombre;
            return View(conjunto_Ejercicio);
        }

        // POST: Conjunto_Ejercicio/Delete/5
        [HttpPost, ActionName("DeleteEjercicio")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedEjercicio(int id)
        {
            
            Conjunto_Ejercicio conjunto_Ejercicio = db.Conjunto_Ejercicios.Include("ConjuntoEjercicioRutina").SingleOrDefault(x => x.Conjunto_EjercicioId == id); ;
            int idRutina = conjunto_Ejercicio.ConjuntoEjercicioRutina.RutinaId;
            db.Conjunto_Ejercicios.Remove(conjunto_Ejercicio);
            db.SaveChanges();
            return RedirectToAction("Ejercicio", new { idRutina });
        }

    [HttpPost]
        public ActionResult Create(string usuarioDropdown,Rutina rutinaCreate)
        {

            ApplicationUser user = new ApplicationUser();

          

            user = db.Users.Single(x => x.Id == usuarioDropdown);

            if (user != null)
            {
               

                Rutina rutina = new Rutina()
                {
                    Usuario = user,
                    
                    RutinaFecha = rutinaCreate.RutinaFecha,

                    RutinaObservaciones = rutinaCreate.RutinaObservaciones
                };
          
             
                db.Rutinas.Add(rutina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rutinaCreate);
        }


    // GET: Rutinas/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutina rutina = db.Rutinas.Find(id);
            if (rutina == null)
            {
                return HttpNotFound();
            }
            return View(rutina);
        }

        // POST: Rutinas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RutinaId,RutinaFecha,RutinaObservaciones")] Rutina rutina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rutina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rutina);
        }

        // GET: Rutinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutina rutina = db.Rutinas.Find(id);
            if (rutina == null)
            {
                return HttpNotFound();
            }
            return View(rutina);
        }

        // POST: Rutinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var getEjercicio = db.Conjunto_Ejercicios.Where(x => x.ConjuntoEjercicioRutina.RutinaId == id).ToList();
            foreach (var n in getEjercicio)
            {
                int i = n.Conjunto_EjercicioId;
                Conjunto_Ejercicio conjunto = db.Conjunto_Ejercicios.Find(i);
                db.Conjunto_Ejercicios.Remove(conjunto);
            }
            Rutina rutina = db.Rutinas.Find(id);
            db.Rutinas.Remove(rutina);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
