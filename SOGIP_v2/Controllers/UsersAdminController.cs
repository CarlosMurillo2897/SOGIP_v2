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

namespace SOGIP_v2.Controllers
{
    //[Authorize(Roles = "Administrador,Supervisor")]
    public class UsersAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersAdminController(){}

        public static string composicionPassword(string Nombre1, string Apellido1, string Cedula, DateTime Nacimiento)
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

        //
        // GET: /Users/
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var usuarios = await UserManager.Users.ToListAsync();

            foreach (var usuario in usuarios)
            {
                var rol = await UserManager.GetRolesAsync(usuario.Id);
                ViewData[usuario.Id] = rol.First();
            }

            return View(usuarios);
        }

        public JsonResult ArchivosUsuario(string usuarioId)
        {
            List<Archivo> archivos = db.Archivo.Where(x => x.Usuario.Id == usuarioId).ToList();
            return Json(archivos, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult InhabilitarUsuario(string usuarioId, bool estado) // estado = true -> usuario.Estado = 0 / estado = false -> usuario.Estado = 1
        {
            var usuario = db.Users.Where(x => x.Id == usuarioId).SingleOrDefault();
                
                usuario.Estado = (estado)? false : true;
                db.SaveChanges();

            return Json(usuario.Estado, JsonRequestBehavior.AllowGet);
        }

       

        //
        // GET: /Users/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }
        [HttpPost]
        public JsonResult getEntrenador(bool rolDeUsuario)
        {
            // Si es true es un entrenador, si es false es un administrador.
            string var = rolDeUsuario ? "4" : "2";
            var users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(var)).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getEntrenador2()
        {

            var data = new ApplicationDbContext();
            var users = data.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("2")).ToList();

            return Json(users, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Users/Create
        //[HttpGet]
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
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, string Atleta_Tipo, int? selectedS, int? SelectedAsox, int? SelectedEntity, string selectedRoles, int? SelectedCategory, int? SelectedSport, FormCollection form, HttpPostedFileBase CV)
        {
            
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
                
                var adminresult = await UserManager.CreateAsync(user, composicionPassword(userViewModel.Nombre1, userViewModel.Apellido1, userViewModel.Cedula, userViewModel.Fecha_Nacimiento));
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
                                    var en = form["hidef"].ToString();
                                    var deporte = db.Deportes.Single(x => x.DeporteId == SelectedSport);
                                    var cat = db.Categorias.Single(x => x.CategoriaId == SelectedCategory);
                                    Seleccion seleccion = new Seleccion()
                                    {
                                        Nombre_Seleccion = "SELECCIÓN" + " " + cat.Descripcion + " " + "DE" + " " + deporte.Nombre,
                                        Usuario = db.Users.Single(x => x.Id == user.Id),
                                        Deporte_Id = db.Deportes.Single(x => x.DeporteId == SelectedSport),
                                        Categoria_Id = db.Categorias.Single(x => x.CategoriaId == SelectedCategory),
                                        Entrenador_Id = db.Users.Where(x => x.Cedula == en).FirstOrDefault()
                                    };

                                    db.Selecciones.Add(seleccion);
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
                                            Tipo = db.Tipos.Where(x=>x.TipoId==1).FirstOrDefault(),
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
                                        Localidad = (form["nombre_localidad"].ToString() == null) ? null : form["nombre_localidad"].ToString().ToUpper(),
                                    };
                                    if (Atleta_Tipo == "Selección")
                                    {
                                        atleta.Seleccion = db.Selecciones.Single(x => x.SeleccionId == selectedS);
                                    }
                                    else
                                    {
                                        atleta.Asociacion_Deportiva = db.Asociacion_Deportiva.Single(x => x.Asociacion_DeportivaId == SelectedAsox);
                                    }

                                    db.Atletas.Add(atleta);
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
                                    break;
                                }

                            case "Asociacion/Comite":
                                {
                                    Asociacion_Deportiva asociacion = new Asociacion_Deportiva()
                                    {
                                        Localidad = (form["nombre_localidad"].ToString() == null) ? null : form["nombre_localidad"].ToString().ToUpper(),
                                        Nombre_DepAso = (form["nombre_aso"].ToString() == null) ? null : form["nombre_aso"].ToString().ToUpper(),
                                        Usuario = db.Users.Single(x => x.Id == user.Id)
                                    };

                                    db.Asociacion_Deportiva.Add(asociacion);
                                    break;
                                }
                        }

                        db.SaveChanges();

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

        public async void func()
        {
            await UserManager.UpdateSecurityStampAsync("8d30625f-4e2e-4c3b-b588-6ccbcfcc0a67");
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
                if (atleta.Seleccion != null) { ViewBag.var2 = atleta.Seleccion.SeleccionId; }
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

            

        public void Download(int archivoId)
        {
                var v = db.Archivo.Where(x => x.ArchivoId == archivoId).Include("Tipo").FirstOrDefault();
                Response.Clear();
                Response.AddHeader("Content-type", v.Tipo.Nombre);
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + v.Nombre + "\"");
                Response.BinaryWrite(v.Contenido);
                Response.Flush();
                Response.End();
        }

        /*
        [HttpPost]
        public void Download(int Documento)
        {
            var v = db.Archivo.Where( x => x.ArchivoId == Documento).Include("Tipo").FirstOrDefault();

            if (v != null)
            {
                byte[] fileData = v.Contenido;
                Response.AddHeader("Content-type", v.Tipo.Nombre);
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + v.Nombre + "\"");

                byte[] dataBlock = new byte[0x1000];
                long fileSize;
                int bytesRead;
                long totalsBytesRead = 0;

                using (Stream st = new MemoryStream(fileData))
                {
                    fileSize = st.Length;
                    while (totalsBytesRead < fileSize)
                    {
                        if (Response.IsClientConnected)
                        {
                            bytesRead = st.Read(dataBlock, 0, dataBlock.Length);
                            Response.OutputStream.Write(dataBlock, 0, bytesRead);

                            Response.Flush();

                            totalsBytesRead += bytesRead;
                        }

                    }
                }
                Response.End();
            }
        }
        */

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
                            atleta.Seleccion = db.Selecciones.Single(x => x.SeleccionId == selectedS);
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

        public ActionResult IndexMasivo()
        {
            return View();
        }

        public JsonResult ObtenerUsuarios()
        {
            var selex = from u in db.Users
                       from s in db.Selecciones
                       where
                       u.Id.Equals(s.Usuario.Id)
                       select new
                       {
                           Acción = "",
                           Cédula = u.Cedula,
                           Nombre = u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2,
                           Entidad = s.Nombre_Seleccion,
                           Rol = "Seleccion/Federacion"
                       };

            var asox = from u in db.Users
                       from a in db.Asociacion_Deportiva
                       where
                       u.Id.Equals(a.Usuario.Id)
                       select new
                       {
                           Acción = "",
                           Cédula = u.Cedula,
                           Nombre = u.Nombre1 + " " + u.Nombre2 + " " + u.Apellido1 + " " + u.Apellido2,
                           Entidad = a.Nombre_DepAso,
                           Rol = "Asociacion/Comite"
                       };

            var entidades = Enumerable.Union(selex, asox).ToList();

            return Json(entidades, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Import(HttpPostedFileBase excelfile)
        {
            var path = Server.MapPath("~/Content/Registros/" + excelfile.FileName);
            List<ApplicationUser> ls = new List<ApplicationUser>();
            // List<object> lista = new List<object>();

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

                    ApplicationUser user = new ApplicationUser()
                    {
                        Cedula = (ced == null) ? "" : ced.ToString().ToUpper(),
                        Nombre1 = (n1 == null) ? "" : n1.ToString().ToUpper(),
                        Nombre2 = (n2 == null) ? " " : n2.ToString().ToUpper(),
                        Apellido1 = (a1 == null) ? "" : a1.ToString().ToUpper(),
                        Apellido2 = (a2 == null) ? " " : a2.ToString().ToUpper(),
                        Email = (email == null) ? "" : email.ToString(),
                        Sexo = genero
                    };

                    // En caso de que la fecha de nacimiento sea errónea por completo se dispondrá la fecha actual.
                    var nacimiento = DateTime.Today;

                    if (nac != null)
                    {
                        string terminos = "";
                        try
                        {
                            terminos = Regex.Replace(nac.ToString(), @"[-\\]", "/");
                            nacimiento = Convert.ToDateTime(terminos);
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

                    //string[] msg = new string[5];
                    //if (db.Users.Any(x => x.Cedula == (string)ced))
                    //{
                    //    msg[msg.Length] = "Usuario repetido, ya se encuentra en el sistema.";
                    //}
                    //else if (n1 == null)
                    //{
                    //    msg[msg.Length] = "Primer NOMBRE es obligatorio para el registro.";
                    //}
                    //else if (a1 == null)
                    //{
                    //    msg[msg.Length] = "Primer APELLIDO es obligatorio para el registro.";
                    //}

                    //object usuario = new
                    //{
                    //    Cedula = (ced == null) ? "" : ced.ToString().ToUpper(),
                    //    Nombre1 = (n1 == null) ? "" : n1.ToString().ToUpper(),
                    //    Nombre2 = (n2 == null) ? " " : n2.ToString().ToUpper(),
                    //    Apellido1 = (a1 == null) ? "" : a1.ToString().ToUpper(),
                    //    Apellido2 = (a2 == null) ? " " : a2.ToString().ToUpper(),
                    //    Fecha_Nacimiento = nacimiento,
                    //    Email = (email == null) ? "" : email.ToString(),
                    //    Sexo = genero,
                    //    Error = false,
                    //    Message = msg
                    //};

                    user.Fecha_Nacimiento = nacimiento;
                    startRow++;

                    ls.Add(user);
                    // lista.Add(usuario);

                } while (ced != null);

            }
            catch (Exception) { }

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return Json(ls, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrearMasivo(List<ApplicationUser> users, string usuario, string rol)
        {
            try
            {
                foreach (var item in users)
                {
                    string pass = UserManager.PasswordHasher.HashPassword(composicionPassword(item.Nombre1, item.Apellido1, item.Cedula, item.Fecha_Nacimiento));
                    item.PasswordHash = pass;
                    item.Roles.Add(new IdentityUserRole { UserId = item.Id, RoleId = "5" });
                    item.SecurityStamp = Guid.NewGuid().ToString();

                    db.Users.Add(item);
                    db.SaveChanges();

                    if (rol == "Asociacion/Comite")
                    {
                        db.Atletas.Add(new Atleta
                        {
                            Asociacion_Deportiva = db.Asociacion_Deportiva.SingleOrDefault(x => x.Usuario.Cedula == usuario),
                            Usuario = db.Users.SingleOrDefault(x => x.Id == item.Id),
                            Localidad = " ",
                            Seleccion = null
                        });
                    }
                    else if (rol == "Seleccion/Federacion")
                    {
                        db.Atletas.Add(new Atleta
                        {
                            Seleccion = db.Selecciones.SingleOrDefault(x => x.Usuario.Cedula == usuario),
                            Usuario = db.Users.SingleOrDefault(x => x.Id == item.Id),
                            Localidad = " ",
                            Asociacion_Deportiva = null
                        });
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }

}
