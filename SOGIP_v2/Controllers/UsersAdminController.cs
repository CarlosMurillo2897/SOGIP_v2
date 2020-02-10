﻿using SOGIP_v2.Models;
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
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace SOGIP_v2.Controllers
{
    //[Authorize(Roles = "Administrador,Supervisor")]
    public class UsersAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersAdminController() {}

        public static string ComposicionPassword(string Nombre1, string Apellido1, string Cedula, DateTime Nacimiento)
        {
            string mes = "";

            if (Nacimiento.Month < 10)
            {
                mes = "0";
            }

            mes = mes + Nacimiento.Month;

            string password = char.ToUpper(Nombre1[0]) + "" +
                              char.ToLower(Nombre1[1]) + "" +
                              char.ToUpper(Apellido1[0]) + "" +
                              char.ToLower(Apellido1[1]) + "" +
                              Cedula.Substring(0, 4) +
                              mes +
                              Nacimiento.Year;
            return password;
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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

        private void SendMailToUser(string correo, string mensaje)
        {
            try
            {
                MailMessage mail = new MailMessage();


                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("sogip.system@gmail.com");
                mail.To.Add(correo);
                mail.Subject = "SOGIP: Usuario creado con éxito";
                mail.Body = mensaje;


                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("sogip.system@gmail.com", "X100@ttW81&");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {

            }
        }


        //
        // GET: /Users/
        [HttpGet]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<ActionResult> Index()
        {
            var usuarios = await UserManager.Users.ToListAsync();

            foreach (var usuario in usuarios)
            {
                var rol = await UserManager.GetRolesAsync(usuario.Id);
                ViewData[usuario.Id] = rol.First();

                if (rol.First() == "Atleta" || rol.First() == "Atleta Becados")
                {
                    /* Usuarios deben pertenecer a una Selección o bien, */
                    var s = db.Atletas.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.SubSeleccion.Seleccion.Nombre_Seleccion).FirstOrDefault();

                    var sub = (from a in db.Atletas
                               from c in db.Categorias
                               where (a.Usuario.Id == usuario.Id && a.SubSeleccion.Categoria_Id.CategoriaId == c.CategoriaId)
                               select c.Descripcion).ToList();

                    /* los Usuarios deben pertenecer a una Asociación. */
                    var aso = db.Atletas.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.Asociacion_Deportiva.Nombre_DepAso).FirstOrDefault();

                    if (aso != null) { ViewData["Asociación" + usuario.Id] = aso; }
                    if (s != null) { ViewData["Selección" + usuario.Id] = s; }
                    if (sub.Count != 0) { ViewData["SubSelección" + usuario.Id] = sub; }

                }
                else if (rol.First() == "Seleccion/Federacion") {

                    var s = db.Selecciones.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.Nombre_Seleccion).FirstOrDefault();

                    var sub = (from sss in db.SubSeleccion
                               from c in db.Categorias
                               where (sss.Seleccion.Usuario.Id == usuario.Id && sss.Categoria_Id.CategoriaId == c.CategoriaId)
                               select c.Descripcion).ToList();

                    if (sub.Count != 0) { ViewData["SubSelección" + usuario.Id] = sub; }
                    if (s != null) { ViewData["Selección" + usuario.Id] = s; }
                }
                else if (rol.First() == "Asociacion/Comite")
                {
                    var aso = db.Asociacion_Deportiva.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.Nombre_DepAso).FirstOrDefault();
                    if (aso != null) { ViewData["Asociación" + usuario.Id] = aso; }
                }
                else if (rol.First() == "Entidades Publicas")
                {
                    var entidad = db.Entidad_Publica.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.Tipo_Entidad.Descripcion).FirstOrDefault();
                    if (entidad != null) { ViewData["Entidad" + usuario.Id] = entidad; }
                }
            }

            return View(usuarios);
        }

        public JsonResult ArchivosUsuario(string usuarioId)
        {
            var archivos = from a in db.Archivo
                           from t in db.Tipos
                           where a.Usuario.Id == usuarioId && a.Tipo.TipoId == t.TipoId
                           select new
                           {
                               ArchivoId = a.ArchivoId,
                               Nombre = a.Nombre,
                               Tipo = t.Nombre
                           };

            return Json(archivos.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InhabilitarUsuario(string usuarioId, bool estado) // estado = true -> usuario.Estado = 0 / estado = false -> usuario.Estado = 1
        {
            var usuario = db.Users.Where(x => x.Id == usuarioId).SingleOrDefault();

            usuario.Estado = (estado) ? false : true;
            db.SaveChanges();

            return Json(usuario.Estado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEntrenador(int tipo)
        {
            try {
                // Si es true es un entrenador, si es false es un administrador.
                string var = tipo == 2 ? "2" : "4";

                // var users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(var)).ToList();
                var users = (from u in db.Users
                             where u.Roles.FirstOrDefault().RoleId == var
                             select new
                             {
                                 Cedula = u.Cedula,
                                 Nombre1 = u.Nombre1,
                                 Apellido1 = u.Apellido1,
                                 Apellido2 = u.Apellido2
                             }).ToList();

                return Json(users, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getEntidades(int tipo)
        {
            try
            {
                var selex = from u in db.Users
                            from s in db.Selecciones
                            where
                            u.Id.Equals(s.Usuario.Id)
                            select new
                            {
                                Cedula = u.Cedula,
                                Nombre1 = u.Nombre1,
                                Nombre2 = u.Nombre2,
                                Apellido1 = u.Apellido1,
                                Apellido2 = u.Apellido2,
                                Entidad = s.Nombre_Seleccion,
                                Role = "Seleccion/Federacion"
                            };

                var asox = from u in db.Users
                           from a in db.Asociacion_Deportiva
                           where
                           u.Id.Equals(a.Usuario.Id)
                           select new
                           {
                               Cedula = u.Cedula,
                               Nombre1 = u.Nombre1,
                               Nombre2 = u.Nombre2,
                               Apellido1 = u.Apellido1,
                               Apellido2 = u.Apellido2,
                               Entidad = a.Nombre_DepAso,
                               Role = "Asociacion/Comite"
                           };

                var list = Enumerable.Union(selex, asox).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: /Users/Create
        //[HttpGet]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<ActionResult> Create()
        {
            ViewBag.Entidades = new SelectList(db.Tipo_Entidad.ToList(), "Tipo_EntidadId", "Descripcion");
            ViewBag.Deportes = new SelectList(db.Deportes.ToList(), "DeporteId", "Nombre");
            ViewBag.Categorias = new SelectList(db.Categorias.ToList(), "CategoriaId", "Descripcion");
            ViewBag.Selecciones = new SelectList(db.Selecciones.ToList(), "SeleccionId", "Nombre_Seleccion");
            ViewBag.Asociaciones = new SelectList(db.Asociacion_Deportiva.ToList(), "Asociacion_DeportivaId", "Nombre_DepAso");
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Id", "Name");

            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, string Atleta_Tipo, string hidef, string hideEntidadS, int? hideCategory, int? selectedS, int? SelectedAsox, int? SelectedEntity, string selectedRoles, int? SelectedCategory, int? SelectedSport, FormCollection form, HttpPostedFileBase CV, string nombre_aso)
        {
            CodigoQRController c = new CodigoQRController();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Cedula,
                    Email = userViewModel.Email,
                    Nombre1 = (userViewModel.Nombre1==null)?null: userViewModel.Nombre1.ToUpper(),
                    Nombre2 = (userViewModel.Nombre2 == null) ? " " : userViewModel.Nombre2.ToUpper(),
                    Apellido1 = (userViewModel.Apellido1 == null) ? null : userViewModel.Apellido1.ToUpper(),
                    Apellido2 = (userViewModel.Apellido2 == null) ? " " : userViewModel.Apellido2.ToUpper(),
                    Cedula = userViewModel.Cedula,
                    Fecha_Nacimiento = userViewModel.Fecha_Nacimiento,
                    Sexo = userViewModel.Sexo,
                    Fecha_Expiracion = DateTime.Now,
                    Estado = true
            };
                var pass = ComposicionPassword(userViewModel.Nombre1, userViewModel.Apellido1, userViewModel.Cedula, userViewModel.Fecha_Nacimiento);
                var adminresult = await UserManager.CreateAsync(user, pass);
                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRoleAsync(user.Id, selectedRoles);
                        
                        if (selectedRoles == "Atleta Becados")
                        {
                            selectedRoles = "Atleta";
                        }

                        switch (selectedRoles)
                        {

                            case "Seleccion/Federacion":
                                {
                                    var en = hidef;
                                    var deporte = db.Deportes.Single(x => x.DeporteId == SelectedSport);
                                    Seleccion seleccion = new Seleccion()
                                    {
                                        Nombre_Seleccion = "SELECCIÓN DE " + deporte.Nombre,
                                        Usuario = db.Users.Single(x => x.Id == user.Id),
                                        Deporte_Id = db.Deportes.Single(x => x.DeporteId == SelectedSport)
                                    };

                                    db.Selecciones.Add(seleccion);
                                    db.SaveChanges();

                                    SubSeleccion subSeleccion = new SubSeleccion()
                                    {
                                        Seleccion = seleccion,
                                        Categoria_Id = db.Categorias.Single(x => x.CategoriaId == SelectedCategory),
                                        Entrenador = db.Users.Where(x => x.Cedula == en).FirstOrDefault()
                                    };
                                    db.SubSeleccion.Add(subSeleccion);
                                    break;
                                }

                            case "Entrenador":
                                {
                                    if (CV != null)
                                    {
                                        BinaryReader br = new BinaryReader(CV.InputStream);
                                        byte[] buffer = br.ReadBytes(CV.ContentLength);

                                        Archivo file = new Archivo() {
                                            Nombre = CV.FileName,
                                            Tipo = db.Tipos.Where(x => x.Nombre == "Curriculum" || x.Nombre == "CV").FirstOrDefault(),
                                            Contenido = buffer,
                                            Usuario = db.Users.Single(x => x.Id == user.Id)
                                        };
                                        db.Archivo.Add(file);
                                    }

                                    break;
                                }

                            case "Atleta":
                                {
                                    Atleta atleta = new Atleta()
                                    {
                                        Usuario = db.Users.Single(x => x.Id == user.Id),
                                        // Localidad = (form["nombre_localidad"].ToString() == null) ? null : form["nombre_localidad"].ToString().ToUpper(),
                                    };
                                    if (Atleta_Tipo == "Seleccion/Federacion")
                                    {
                                        atleta.SubSeleccion = db.SubSeleccion.Single(x => x.Seleccion.Usuario.Cedula == hideEntidadS && x.Categoria_Id.CategoriaId == hideCategory);
                                    }
                                    else if(Atleta_Tipo == "Asociacion/Comite")
                                    {
                                        atleta.Asociacion_Deportiva = db.Asociacion_Deportiva.Single(x => x.Usuario.Cedula == hideEntidadS);
                                    }

                                    db.Atletas.Add(atleta);
                                    c.generarQr2(user.Cedula);//Agregar QR para Atleta
                                    break;
                                }

                            case "Funcionarios ICODER":
                                {
                                    var en = form["hidef"].ToString();
                                    Funcionario_ICODER funcionario = new Funcionario_ICODER()
                                    {
                                        Usuario = db.Users.Single(x => x.Id == user.Id),
                                        Entrenador = db.Users.Single(x => x.Cedula == en)
                                    };

                                    db.Funcionario_ICODER.Add(funcionario);
                                    c.generarQr2(user.Cedula);//Agregar QR para Funcionario Icoder
                                    break;
                                }

                            case "Entidades Publicas":
                                {
                                    Entidad_Publica entPub = new Entidad_Publica()
                                    {
                                        Usuario = db.Users.Single(x => x.Id == user.Id),
                                        Tipo_Entidad = db.Tipo_Entidad.Single(x => x.Tipo_EntidadId == SelectedEntity)
                                    };

                                    db.Entidad_Publica.Add(entPub);
                                    c.generarQr2(user.Cedula);//Agregar QR para Entidad Publica
                                    break;
                                }

                            case "Asociacion/Comite":
                                {
                                    Asociacion_Deportiva asociacion = new Asociacion_Deportiva()
                                    {
                                        Localidad = (form["nombre_localidad"].ToString() == null) ? null : form["nombre_localidad"].ToString().ToUpper(),
                                        Usuario = db.Users.Single(x => x.Id == user.Id),
                                        Nombre_DepAso = (nombre_aso == null) ? null : nombre_aso.ToString().ToUpper()
                                    };

                                    db.Asociacion_Deportiva.Add(asociacion);
                                    break;
                                }
                        }

                        db.SaveChanges();
                        SendMailToUser(user.Email, "SOGIP y el ICODER le da la bienvenida a nuestro sistema.\n"
                        + " A continuación le detallamos su nombre de usuario: " + user.Cedula + " y contraseña creada por defecto " + pass + " (esta contraseña es temporal y puede ser cambiada dentro del sistema)."
                        + "\nCon el cual podrá acceder y utilizar nuestro servicio virtual.");

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");

                            CV = null;

                            return View();
                        }
                    }
                }

                else
                {
                    // Existe algún error con los datos ingresados (datos repetidos).

                    ModelState.AddModelError("", adminresult.Errors.First());

                    ViewBag.Entidades = new SelectList(db.Tipo_Entidad.ToList(), "Tipo_EntidadId", "Descripcion");
                    ViewBag.Deportes = new SelectList(db.Deportes.ToList(), "DeporteId", "Nombre");
                    ViewBag.Categorias = new SelectList(db.Categorias.ToList(), "CategoriaId", "Descripcion");
                    ViewBag.Selecciones = new SelectList(db.Selecciones.ToList(), "SeleccionId", "Nombre_Seleccion");
                    ViewBag.Asociaciones = new SelectList(db.Asociacion_Deportiva.ToList(), "Asociacion_DeportivaId", "Nombre_DepAso");
                    ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Id", "Name");

                    CV = null;

                    return View();

                }
                // If everything it's ok.
                return RedirectToAction("Index");
            }

            ViewBag.Entidades = new SelectList(db.Tipo_Entidad.ToList(), "Tipo_EntidadId", "Descripcion");
            ViewBag.Deportes = new SelectList(db.Deportes.ToList(), "DeporteId", "Nombre");
            ViewBag.Categorias = new SelectList(db.Categorias.ToList(), "CategoriaId", "Descripcion");
            ViewBag.Selecciones = new SelectList(db.Selecciones.ToList(), "SeleccionId", "Nombre_Seleccion");
            ViewBag.Asociaciones = new SelectList(db.Asociacion_Deportiva.ToList(), "Asociacion_DeportivaId", "Nombre_DepAso");
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Id", "Name");

            return View();
        }

        // GET: /Users/Edit/1
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            //var usar = db.Users.Single(x=>x.Id==id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            //Comite/Aso *****
            ViewBag.Asociaciones = new SelectList(db.Asociacion_Deportiva.ToList(), "Asociacion_DeportivaId", "Nombre_DepAso");
            var comite = db.Asociacion_Deportiva.Where(x => x.Usuario.Id == user.Id).FirstOrDefault();
            if (comite != null) { ViewBag.comite = comite.Nombre_DepAso; }


            //Entidad
            ViewBag.Entidades = new SelectList(db.Tipo_Entidad.ToList(), "Tipo_EntidadId", "Descripcion");
            var entidad = db.Entidad_Publica.Where(x => x.Usuario.Id == user.Id).FirstOrDefault();
            if (entidad != null) { ViewBag.entidad = entidad.Tipo_Entidad.Tipo_EntidadId; }

            //Usuario
            ViewBag.genero = user.Sexo;

            //Seleccion/Federación *****
            ViewBag.Selecciones = new SelectList(db.Selecciones.ToList(), "SeleccionId", "Nombre_Seleccion");
            var sele = db.Selecciones.Where(a => a.Usuario.Id == user.Id).FirstOrDefault();
            if (sele != null) { ViewBag.seleccion = sele.Nombre_Seleccion; }

            var atleta = db.Atletas.Where(x => x.Usuario.Id == user.Id).FirstOrDefault();
            if (atleta != null)
            {
                if (atleta.Asociacion_Deportiva != null) { ViewBag.var1 = atleta.Asociacion_Deportiva.Asociacion_DeportivaId; }
                if (atleta.SubSeleccion != null) { ViewBag.var2 = atleta.SubSeleccion.Seleccion.SeleccionId; }
            }

            //Entrenador
            //ViewBag.Archivos = db.Archivo.Where(x => x.Usuario == user).ToList();

            //Roles
            ViewBag.rol = userRoles;

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Cedula = user.Cedula,
                CedulaExtra = user.CedulaExtra,
                Nombre1 = user.Nombre1,
                Nombre2 = user.Nombre2,
                Apellido1 = user.Apellido1,
                Apellido2 = user.Apellido2,
                Fecha_Nacimiento = user.Fecha_Nacimiento,
                Sexo = user.Sexo,
                Estado = user.Estado
            });
        }

        public void Download(int archivoId, bool? masivo)
        {
            try
            {
                var v = masivo != null ?
                db.Archivo.Where(x => x.Tipo.Nombre == "INGRESO MASIVO" && x.Usuario.Cedula == "000000000").Include("Tipo").FirstOrDefault()
                : db.Archivo.Where(x => x.ArchivoId == archivoId).Include("Tipo").FirstOrDefault();
                Response.Clear();
                Response.AddHeader("Content-type", v.Tipo.Nombre);
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + v.Nombre + "\"");
                Response.BinaryWrite(v.Contenido);
                Response.Flush();
                Response.End();
            }
            catch(Exception)
            {

            }

        }

        public JsonResult UpdateUser(ApplicationUser usr, string role, List<int> l1, List<string> l2, List<string> l3,
            int? Categoria_Atleta_Id,
            int? Entidad_Id,
            int? Deporte_Id, string Nombre_Seleccion = "",
            string Nombre_Asociacion = "",
            string Entidad_Atleta = "",
            string Rol_Entidad_Atleta = "",
            string Funcionario_Cedula = ""
            )
        {
            try
            {
                ApplicationUser u = db.Users.Where(x => x.Id == usr.Id).FirstOrDefault();
                u.Cedula = usr.Cedula;
                u.UserName = usr.Cedula;
                u.Nombre1 = usr.Nombre1;
                u.Nombre2 = usr.Nombre2;
                u.Apellido1 = usr.Apellido1;
                u.Apellido2 = usr.Apellido2;
                u.Email = usr.Email;
                u.Sexo = usr.Sexo;
                u.Fecha_Nacimiento = usr.Fecha_Nacimiento;

                if (usr.PasswordHash != null)
                {
                    u.PasswordHash = UserManager.PasswordHasher.HashPassword(ComposicionPassword(u.Nombre1, u.Apellido1, u.Cedula, u.Fecha_Nacimiento));
                    u.Fecha_Expiracion = DateTime.Today;
                }

                if (role == "Seleccion/Federacion")
                {
                    Seleccion s = db.Selecciones.Where(h => h.Usuario.Id == usr.Id).FirstOrDefault();
                    s.Nombre_Seleccion = Nombre_Seleccion;
                    if (Deporte_Id != 0)
                    {
                        s.Deporte_Id = db.Deportes.Find(Deporte_Id);
                    }
                    for (int i = 0; i < l1.Count; i++)
                    {
                        SubSeleccion sub = new SubSeleccion();
                        var subId = l1.ElementAt(i);
                        var cat = l2.ElementAt(i);
                        var train = l3.ElementAt(i);

                        if (subId != 0) { sub = db.SubSeleccion.Where(j => j.SubSeleccionId == subId).FirstOrDefault(); }

                        sub.Seleccion = s;
                        sub.Categoria_Id = db.Categorias.Where(l => l.Descripcion == cat).FirstOrDefault();
                        sub.Entrenador = db.Users.Where(k => k.Cedula == train).FirstOrDefault();

                        if (subId == 0) {
                            db.SubSeleccion.Add(sub);
                        }
                    }
                }
                //// Esto solo Supervisor, Administrador y Selección o Asociación
                if (Categoria_Atleta_Id != 0 && (role == "Atleta" || role == "Atleta Becados"))
                {
                    Atleta ath = db.Atletas.Where(a => a.Usuario.Id == usr.Id).FirstOrDefault();
                    if (ath == null)
                    {
                        ath = new Atleta { Usuario = u };
                        db.Atletas.Add(ath);
                    }

                    if (Rol_Entidad_Atleta == "Asociacion/Comite")
                    {
                        ath.Asociacion_Deportiva = db.Asociacion_Deportiva.Where(i => i.Nombre_DepAso == Entidad_Atleta).FirstOrDefault();

                        ath.SubSeleccion = db.Atletas.Where(k => k.Usuario.Id == usr.Id).Select(l=>l.SubSeleccion).FirstOrDefault();
                        ath.SubSeleccion = null;
                    }
                    else
                    {
                        // Seleccionamos la Selección con el mismo nombre y a la vez que cumpla con ser la Categoría seleccionada.
                        ath.SubSeleccion = db.SubSeleccion.Where(s => s.Seleccion.Nombre_Seleccion == Entidad_Atleta && s.Categoria_Id.CategoriaId == Categoria_Atleta_Id).FirstOrDefault();

                        ath.Asociacion_Deportiva = db.Atletas.Where(i => i.Usuario.Id == usr.Id).Select(km => km.Asociacion_Deportiva).FirstOrDefault();
                        ath.Asociacion_Deportiva = null;
                    }
                }
                
                if (role == "Funcionarios ICODER")
                {
                    Funcionario_ICODER funx = db.Funcionario_ICODER.Where(x => x.Usuario.Id == usr.Id).FirstOrDefault();
                    funx.Entrenador = db.Users.Where(j => j.Cedula == Funcionario_Cedula).FirstOrDefault();
                }
                if (role == "Entidades Publicas" && Entidad_Id != 0)
                {
                    // Cambiar entidad.
                    Entidad_Publica ent = db.Entidad_Publica.Where(x => x.Usuario.Id == usr.Id).FirstOrDefault();
                    ent.Tipo_Entidad = db.Tipo_Entidad.Where(e => e.Tipo_EntidadId == Entidad_Id).FirstOrDefault();
                }
                if (role == "Asociacion/Comite")
                {
                    Asociacion_Deportiva asd = db.Asociacion_Deportiva.Where(a => a.Usuario.Id == usr.Id).FirstOrDefault();
                    asd.Nombre_DepAso = Nombre_Asociacion;
                }

                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult RestaurarContraseñaOriginal(string Id)
        {
            try
            {
                ApplicationUser consulta = db.Users.Where(x => x.Id == Id).FirstOrDefault();
                consulta.PasswordHash = UserManager.PasswordHasher.HashPassword(ComposicionPassword(consulta.Nombre1, consulta.Apellido1, consulta.Cedula, consulta.Fecha_Nacimiento));
                consulta.Fecha_Expiracion = DateTime.Today;
                db.SaveChanges();
            }
            catch (Exception) {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult NombreSeleccionRepetido(string nombre, string Id)
        {
            return Json(!db.Selecciones.Any(x => x.Nombre_Seleccion == nombre && x.Usuario.Id != Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult NombreAsociacionRepetido(string nombre, string Id)
        {
            return Json(!db.Asociacion_Deportiva.Any(x => x.Nombre_DepAso == nombre && x.Usuario.Id != Id), JsonRequestBehavior.AllowGet);
        }
        

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cedula,Nombre1,Nombre2,Apellido1,Apellido2,Fecha_Nacimiento,Sexo,Email")] EditUserViewModel editUser, string Atleta_Tipo, int? selectedS, int? SelectedAsox, int? SelectedEntity, FormCollection form, HttpPostedFileBase CV)
        {
            // Download(Documento);
            if (ModelState.IsValid)
            {
                string rol = form["rolName"].ToString();
                var user = db.Users.Single(x => x.Id == editUser.Id);
                var userRoles = UserManager.GetRolesAsync(user.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                
                user.Email = editUser.Email;
                user.Nombre1 = (editUser.Nombre1 == null) ? null : editUser.Nombre1.ToUpper();
                user.Nombre2 = (editUser.Nombre2 == null) ? " " : editUser.Nombre2.ToUpper();
                user.Apellido1 = (editUser.Apellido1 == null) ? null : editUser.Apellido1.ToUpper();
                user.Apellido2 = (editUser.Apellido2 == null) ? " " : editUser.Apellido2.ToUpper();
                user.Fecha_Nacimiento = editUser.Fecha_Nacimiento;
                user.Sexo = editUser.Sexo;

                if(rol== "Atleta Becados")
                {
                    rol = "Atleta";
                }

                switch (rol)
                {
                    case "Seleccion/Federacion":
                        var sele = db.Selecciones.Single(x => x.Usuario.Id == editUser.Id);
                        sele.Nombre_Seleccion = (form["sele_n"].ToString() == null) ? null : form["sele_n"].ToString().ToUpper();
                        break;
                    case "Asociacion/Comite":
                        var aso = db.Asociacion_Deportiva.Single(x => x.Usuario.Id == editUser.Id);
                        aso.Nombre_DepAso = (form["nombre_aso"].ToString() == null) ? null : form["nombre_aso"].ToString().ToUpper();
                        break;
                    case "Entidades Publicas":
                        var ent = db.Entidad_Publica.Single(x => x.Usuario.Id == editUser.Id);
                        ent.Tipo_Entidad = db.Tipo_Entidad.Single(x => x.Tipo_EntidadId == SelectedEntity);
                        break;

                    case "Entrenador":
                        if (CV != null)
                        {
                            BinaryReader br = new BinaryReader(CV.InputStream);
                            byte[] buffer = br.ReadBytes(CV.ContentLength);

                            Archivo file = new Archivo()
                            {
                                Nombre = CV.FileName,
                                Tipo = db.Tipos.Where(x=>x.TipoId==1).FirstOrDefault(),
                                Contenido = buffer,
                                Usuario = db.Users.Single(x => x.Id == user.Id)
                            };
                            db.Archivo.Add(file);
                        }
                        break;

                    case "Atleta":
                        var atleta = db.Atletas.Single(x=>x.Usuario.Id==editUser.Id);
                        if (Atleta_Tipo == "Selección")
                        {
                            atleta.SubSeleccion = db.SubSeleccion.Single(x => x.Seleccion.SeleccionId == selectedS);
                        }
                        else
                        {
                            atleta.Asociacion_Deportiva = db.Asociacion_Deportiva.Single(x => x.Asociacion_DeportivaId == SelectedAsox);
                        }
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", "Algo falló, favor revisar.");
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador,Supervisor")]
        public ActionResult IndexMasivo()
        {

            return View();
        }

        // CREATE ONE USER
        public JsonResult ObtenerUsuarios()
        {
            var selex = from u in db.Users
                        from s in db.Selecciones
                        where
                        u.Id.Equals(s.Usuario.Id)
                        select new
                        {
                            Cédula = u.Cedula,
                            Nombre = u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2,
                            Entidad = s.Nombre_Seleccion,
                            Rol = "Seleccion/Federacion",
                            Categoria = db.SubSeleccion.Where(x => x.Seleccion.SeleccionId == s.SeleccionId).Select(ss => ss.Categoria_Id).ToList()
                        };

            var sex = selex.ToList();
            List<Categoria> str = new List<Categoria>();

            var asox = from u in db.Users
                       from a in db.Asociacion_Deportiva
                       where
                       u.Id.Equals(a.Usuario.Id)
                       select new
                       {
                           Cédula = u.Cedula,
                           Nombre = u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2,
                           Entidad = a.Nombre_DepAso,
                           Rol = "Asociacion/Comite",
                           Categoria = str
                       };

            var entidades = Enumerable.Union(selex, asox).ToList();

            return Json(entidades, JsonRequestBehavior.AllowGet);
        }

        // CREATE MULTIPLE SUER "INDEX-MASIVO"
        public JsonResult ObtenerUsuariosMasivo(bool asociar)
        {
            List<Categoria> str = new List<Categoria>();
            if (asociar)
            {
                var selex = from u in db.Users
                            from s in db.Selecciones
                            where
                            u.Id.Equals(s.Usuario.Id)
                            select new
                            {
                                Cédula = u.Cedula,
                                Entidad = s.Nombre_Seleccion,
                                Rol = "ATLETA DE SELECCIÓN",
                                Categoria = db.SubSeleccion.Where(x => x.Seleccion.SeleccionId == s.SeleccionId).Select(ss => ss.Categoria_Id).ToList()
                            };

                var sex = selex.ToList();

                var asox = from u in db.Users
                           from a in db.Asociacion_Deportiva
                           where
                           u.Id.Equals(a.Usuario.Id)
                           select new
                           {
                               Cédula = u.Cedula,
                               Entidad = a.Nombre_DepAso,
                               Rol = "ATLETA DE ASOCIACIÓN",
                               Categoria = str
                           };

                var ent = from te in db.Tipo_Entidad
                          select new
                          {
                              Cédula = "N/A",
                              Entidad = te.Descripcion,
                              Rol = "ENTIDADES PUBLICAS",
                              Categoria = str
                          };

                var entidades = Enumerable.Union(selex, asox).ToList();
                entidades = entidades.Union(ent).ToList();
                return Json(entidades, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var roles = from r in db.Roles
                            where r.Name != "Atleta" && r.Name != "Atleta Becados" && r.Name != "Entidades Publicas"
                            select new
                            {
                                Cédula = "N/A",
                                Entidad = r.Name.ToUpper(),
                                Rol = r.Name.ToUpper(),
                                Categoria = str
                            };

                return Json(roles.ToList(), JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult Import(HttpPostedFileBase excelfile, string cedulaUsuario)
        {
            var path = Server.MapPath("~/Content/Registros/" + excelfile.FileName);
            List<object> ls = new List<object>();

            try
            {
                if (excelfile != null)
                {
                    excelfile.SaveAs(path);
                }
                var package = new ExcelPackage(new System.IO.FileInfo(path));

                int startColumn = 1;
                int startRow = 2;

                ExcelWorksheet workSheet = package.Workbook.Worksheets[2];
                
                object ced = null;

                do
                {
                    ced = workSheet.Cells[startRow, startColumn].Value;
                    if (ced == null) { break; }
                    object n1 = workSheet.Cells[startRow, startColumn + 1].Value;
                    object n2 = workSheet.Cells[startRow, startColumn + 2].Value;
                    object a1 = workSheet.Cells[startRow, startColumn + 3].Value;
                    object a2 = workSheet.Cells[startRow, startColumn + 4].Value;
                    object nac = workSheet.Cells[startRow, startColumn + 5].Value;
                    object email = workSheet.Cells[startRow, startColumn + 6].Value;
                    object sexo = workSheet.Cells[startRow, startColumn + 7].Value;

                    var genero = (sexo.ToString() == "Femenino") ? false : true;

                    // En caso de que la fecha de nacimiento sea errónea por completo se dispondrá la fecha actual.
                    var nacimiento = DateTime.Today;

                    if (nac != null)
                    {
                        string terminos = "";
                        try
                        {
                            terminos = Regex.Replace(nac.ToString(), @"[-\\]", "/");
                            nacimiento = Convert.ToDateTime(terminos);
                            nac = nacimiento.ToString("yyyy/MM/dd");
                            
                            if ((nacimiento.Year < (DateTime.Today.Year - 80)) || (nacimiento.Year > (DateTime.Today.Year - 10)))
                            {
                                nacimiento = DateTime.Today;
                            }
                        }
                        
                        catch (Exception)
                        {

                            string[] valores = terminos.Split('/');
                            string date = "";

                            // Formato #1: Si es de formato dd/mm/aaaa ó mm/dd/aaaa
                            string patternDMA = @"(\d\d?)[-.\\/](\d\d?)[-.\\/](\d{4})";


                            // Formato #2: Si es de formato aaaa/mm/dd ó aaaa/dd/mm
                            string patternADM = @"(\d{4})[-.\\/](\d\d?)[-.\\/](\d\d?)";

                            Match matchDMA = Regex.Match(nac.ToString(), patternDMA);
                            Match matchADM = Regex.Match(nac.ToString(), patternADM);

                            // Formato #1 (Día, Mes, Año) ó (Mes, Día, Año).
                            if (matchDMA.Success)
                            {
                                date = (Convert.ToInt16(valores[1]) > 12) ? valores[1] + "/" + valores[0] + "/" + valores[2] : valores[0] + "/" + valores[1] + "/" + valores[2];
                            }

                            // Formato #2 (Año, Mes, Día) ó (Año, Día, Mes).
                            if (matchADM.Success)
                            {
                                date = (Convert.ToInt16(valores[1]) > 12) ? valores[1] + "/" + valores[2] + "/" + valores[0] : valores[2] + "/" + valores[1] + "/" + valores[0];
                            }

                            try
                            {
                                //  Prueba realizada el 24/01/2019.
                                // Si el año del documento Excel es menor que 1939 (2019 - 80 = 1939) ó el año del documento Excel es < que 2009 (2019 - 10 = 2009).
                                if ((Convert.ToDateTime(date).Year < (nacimiento.Year - 80)) || (Convert.ToDateTime(date).Year > (nacimiento.Year - 10)))
                                {
                                    throw;
                                }
                                nacimiento = Convert.ToDateTime(date);
                            }
                            catch (Exception)
                            {
                                nacimiento = DateTime.Today;
                            }
                        }
                    }

                    List<string> msg = new List<string>();
                    ced = (ced == null) ? "" : Regex.Replace(ced.ToString(), @"\-", "");
                    string cedula = Convert.ToString(ced);

                    if (db.Users.Any(x => x.Cedula == cedula))
                    {
                        msg.Add("Usuario repetido, ya se encuentra en el sistema.");
                    }
                    if (n1 == null)
                    {
                        msg.Add("Primer NOMBRE es obligatorio para el registro.");
                    }
                    if (a1 == null)
                    { 
                        msg.Add("Primer APELLIDO es obligatorio para el registro.");
                    }
                    if (email == null)
                    { 
                        msg.Add("E-mail es obligatorio para el registro.");
                    }

                    object usuario = new
                    {
                        Cedula = (ced == null) ? "" : ced.ToString().ToUpper(),
                        Nombre1 = (n1 == null) ? "" : n1.ToString().ToUpper(),
                        Nombre2 = (n2 == null) ? " " : n2.ToString().ToUpper(),
                        Apellido1 = (a1 == null) ? "" : a1.ToString().ToUpper(),
                        Apellido2 = (a2 == null) ? " " : a2.ToString().ToUpper(),
                        Fecha_Nacimiento = nac,
                        Email = (email == null) ? "" : email.ToString(),
                        Sexo = genero,
                        Error = false,
                        Message = msg
                    };

                    startRow++;
                    ls.Add(usuario);

                } while (ced != null);
            }
            catch (Exception) { }

            if (System.IO.File.Exists(path))
            {

                BinaryReader br = new BinaryReader(excelfile.InputStream);
                byte[] buffer = br.ReadBytes(excelfile.ContentLength);

                db.Archivo.Add(new Archivo
                {
                    Nombre = excelfile.FileName,
                    Tipo = db.Tipos.SingleOrDefault( x => x.Nombre == "Ingreso Masivo"),
                    Usuario = db.Users.SingleOrDefault( y => y.Cedula == cedulaUsuario),
                    Contenido = buffer
                });

                db.SaveChanges();

                System.IO.File.Delete(path);
            }

            return Json(ls, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CedulaRepetida(string ced, string Id)
        {
            return Json(!db.Users.Any(x => x.Cedula == ced && x.Id != Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrearMasivo(List<ApplicationUser> users, string rol, string usuario, int value)
        {
            try
            {
                foreach (var item in users)
                {
                    /* 1. GENERATE COMBINED PASSWORD
                     * 2. CREATE ASSOCIATION BETWEEN USER AND SELECTED ROLE.
                     * 3. CREATE SECURITY STAMP FOR USER.
                     * I.E. THIS WILL WORK FOR 
                     *  1. Administrador
                     *  9. Supervisor
                     *  10. Usuario Externo
                     */

                var id = rol == "ATLETA DE ASOCIACIÓN" || rol == "ATLETA DE SELECCIÓN" ? "5" : db.Roles.Where(x => x.Name == rol).Select(y => y.Id).SingleOrDefault();

                var entName = item.PasswordHash == null ? "SIN NOMBRE" : item.PasswordHash.ToUpper();

                string pass = UserManager.PasswordHasher.HashPassword(ComposicionPassword(item.Nombre1, item.Apellido1, item.Cedula, item.Fecha_Nacimiento));
                    item.PasswordHash = pass;
                    item.Roles.Add(
                        new IdentityUserRole
                        {
                            UserId = item.Id,
                            RoleId = id
                        });

                    item.SecurityStamp = Guid.NewGuid().ToString();

                    // SAVE REGULAR USER
                    db.Users.Add(item);
                    db.SaveChanges();

                    //GENERATE QR CODE ACCORDING TO USER CREDENTIAL
                    CodigoQRController c = new CodigoQRController();
                    c.generarQr2(item.Cedula);

                    /* CREATE ROL INFORMATION IF IT'S 
                     *  2. Asociacion/Comite
                     *  3. Atleta
                     *  4. Atleta Becados
                     *  5. Entidades Publicas
                     *  6. Entrenador
                     *  7. Funcionarios ICODER
                     *  8. Seleccion/Federacion
                     */

                    switch (rol) {

                        case "ATLETA DE ASOCIACIÓN":
                            {
                                db.Atletas.Add(new Atleta
                                {
                                    Asociacion_Deportiva = db.Asociacion_Deportiva.SingleOrDefault(x => x.Usuario.Cedula == usuario),
                                    Usuario = db.Users.SingleOrDefault(x => x.Id == item.Id),
                                    SubSeleccion = null
                                });
                                break;
                            }
                        case "ATLETA DE SELECCIÓN":
                            {
                                db.Atletas.Add(new Atleta
                                {
                                    SubSeleccion = db.SubSeleccion.SingleOrDefault(x => x.Seleccion.Usuario.Cedula == usuario && x.Categoria_Id.CategoriaId == value),
                                    Usuario = db.Users.SingleOrDefault(x => x.Id == item.Id),
                                    Asociacion_Deportiva = null
                                });
                                break;
                            }
                        case "ENTIDADES PUBLICAS":
                            {
                                db.Entidad_Publica.Add(new Entidad_Publica
                                {
                                    Tipo_Entidad = db.Tipo_Entidad.Where( ti => ti.Descripcion == usuario).SingleOrDefault(),
                                    Usuario = db.Users.Where( u => u.Id == item.Id ).SingleOrDefault()
                                });
                                break;
                            }
                        case "ASOCIACION/COMITE":
                            {
                                db.Asociacion_Deportiva.Add(new Asociacion_Deportiva
                                {
                                    Nombre_DepAso = entName,
                                    Usuario = db.Users.Where(u => u.Id == item.Id).SingleOrDefault()
                                });
                                break;
                            }
                        case "FUNCIONARIOS ICODER":
                            {
                                db.Funcionario_ICODER.Add(new Funcionario_ICODER
                                {   
                                    Usuario = db.Users.Where(u => u.Id == item.Id).SingleOrDefault()
                                });
                                break;
                            }
                            // Create Selection or Federation without SubSelection
                        case "SELECCION/FEDERACION":
                            {
                                db.Selecciones.Add(new Seleccion
                                {
                                    Nombre_Seleccion = entName,
                                    Deporte_Id = db.Deportes.Where(d => d.DeporteId == value).SingleOrDefault(),
                                    Usuario = db.Users.Where(u => u.Id == item.Id).SingleOrDefault(),
                                });
                                /*
                                db.SaveChanges();
                                db.SubSeleccion.Add(new SubSeleccion{ 
                                    Seleccion = db.Selecciones.Where(s => s.Nombre_Seleccion == usuario).SingleOrDefault(), 
                                    Categoria_Id = db.Categorias.Where(c => c.CategoriaId == idDeCategoria).SingleOrDefault()
                                });
                                */
                                break;
                            }
                        default: 
                            {

                                break;
                            }
                    }

                    db.SaveChanges();
                    SendMailToUser(item.Email, "SOGIP y el ICODER le da la bienvenida a nuestro sistema.\n"
                        + " A continuación le detallamos su nombre de usuario: " + item.Cedula + " y contraseña creada por defecto " + pass + " (esta contraseña es temporal y puede ser cambiada dentro del sistema)."
                        + "\nCon el cual podrá acceder y utilizar nuestro servicio virtual.");
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsuarios(string filtro)
        {
            List<object> lista = new List<object>();
            var usuarios = filtro == "0" ?
                           (from u in db.Users
                           from r in db.Roles
                           where u.Roles.FirstOrDefault().RoleId == r.Id
                           select new{
                               u.Id,
                               u.Cedula,
                               Nom = u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2,
                               u.Email,
                               u.Sexo,
                               u.Fecha_Nacimiento,
                               u.Estado,
                               r.Name
                           }).ToList() : 
                           (from u in db.Users
                            from r in db.Roles
                            where u.Roles.FirstOrDefault().RoleId == r.Id && u.Roles.FirstOrDefault().RoleId == filtro
                            select new
                            {
                                u.Id,
                                u.Cedula,
                                Nom = u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2,
                                u.Email,
                                u.Sexo,
                                u.Fecha_Nacimiento,
                                u.Estado,
                                r.Name
                            }).ToList();

            foreach (var usuario in usuarios)
            {
                var rol = usuario.Name;
                var entidad = "Asociado ICODER";

                if (rol == "Atleta" || rol == "Atleta Becados" || rol == "Entrenador")
                {
                    var seleccion = "";
                    var categoria = "";

                    /* Usuarios deben pertenecer a una Selección o bien, */
                    if (rol == "Entrenador")
                    {
                        seleccion = db.SubSeleccion.Where(x => x.Entrenador.Id == usuario.Id).Select(x => x.Seleccion.Nombre_Seleccion).FirstOrDefault();
                        categoria = (from a in db.SubSeleccion
                                         where a.Entrenador.Id == usuario.Id
                                         select a.Categoria_Id.Descripcion).FirstOrDefault();
                    }
                    else { 
                        seleccion = db.Atletas.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.SubSeleccion.Seleccion.Nombre_Seleccion).FirstOrDefault();
                        categoria = (from a in db.Atletas
                                         from c in db.Categorias
                                         where (a.Usuario.Id == usuario.Id && a.SubSeleccion.Categoria_Id.CategoriaId == c.CategoriaId)
                                         select c.Descripcion).FirstOrDefault();
                    }


                    var selex = "";
                    if (categoria != null) {
                        foreach(var p in seleccion.Split(' '))
                        {
                            if (p == "DE")
                            {
                                selex = selex + " " + categoria;
                            }

                            selex = selex + " " + p;

                        }
                        entidad = selex;//"SELECCIÓN " + categoria + " DE " + seleccion.Substring(28);
                    }

                    /* los Usuarios deben pertenecer a una Asociación. */
                    if (entidad == "Asociado ICODER" && rol != "Entrenador") {
                        entidad = db.Atletas.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.Asociacion_Deportiva.Nombre_DepAso).FirstOrDefault();
                    }

                }
                else if (rol == "Seleccion/Federacion")
                {
                    entidad = db.Selecciones.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.Nombre_Seleccion).FirstOrDefault();
                }
                else if (rol == "Asociacion/Comite")
                {
                    entidad = db.Asociacion_Deportiva.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.Nombre_DepAso).FirstOrDefault();
                }
                else if (rol == "Entidades Publicas")
                {
                    entidad = db.Entidad_Publica.Where(x => x.Usuario.Id == usuario.Id).Select(x => x.Tipo_Entidad.Descripcion).FirstOrDefault();
                }

                object usr = new
                {
                    Cedula = usuario.Cedula,
                    Nombre = usuario.Nom,
                    Sexo = usuario.Sexo,
                    Email = usuario.Email,
                    Nacimiento = usuario.Fecha_Nacimiento,
                    Estado = usuario.Estado,
                    Rol = usuario.Name,
                    Entidad = entidad,
                    Id = usuario.Id
                };
                lista.Add(usr);
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerRoles()
        {
            var roles = from r in db.Roles select new { Id = r.Id, Rol = r.Name };
            var rol = roles.ToList();
            return Json(roles.ToList(), JsonRequestBehavior.AllowGet);
        }
    }

}