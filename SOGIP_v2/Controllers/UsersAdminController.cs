using SOGIP_v2.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;

namespace SOGIP_v2.Controllers
{
    //[Authorize(Roles = "Administrador,Supervisor")]
    public class UsersAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersAdminController()
        {
        }

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
                    Nombre1 = userViewModel.Nombre1,
                    Nombre2 = userViewModel.Nombre2,
                    Apellido1 = userViewModel.Apellido1,
                    Apellido2 = userViewModel.Apellido2,
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
                                    Seleccion seleccion = new Seleccion()
                                    {
                                        //Nombre_Seleccion = "Seleccion" + form["sele_n"].ToString() + "de" + form["sele_m"].ToString(),
                                        Nombre_Seleccion = form["sele_n"].ToString(),
                                        Usuario = db.Users.Single(x => x.Id == user.Id),
                                        Deporte_Id = db.Deportes.Single(x => x.DeporteId == SelectedSport),
                                        Categoria_Id = db.Categorias.Single(x => x.CategoriaId == SelectedCategory),
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
                                            Extension = Path.GetExtension(CV.FileName),
                                            Tipo = CV.ContentType,
                                            Contenido = buffer,
                                            Usuario = db.Users.Single(x => x.Id == user.Id)
                                        };
                                        db.Archivo.Add(file);
                                    }

                                    Entrenador entrenador = new Entrenador()
                                    {
                                        Usuario = db.Users.Single(x => x.Id == user.Id)
                                    };

                                    db.Entrenadores.Add(entrenador);

                                    break;
                                }

                            case "Atleta":
                                {
                                    Atleta atleta = new Atleta()
                                    {
                                        Usuario = db.Users.Single(x => x.Id == user.Id),
                                        Localidad = form["nombre_localidad"].ToString()
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
                                    Funcionario_ICODER funcionario = new Funcionario_ICODER()
                                    {
                                        Usuario = db.Users.Single(x => x.Id == user.Id),
                                        Entrenador = db.Users.Single(x => x.Cedula == "114070986") // Cédula de Josafat, esto es momentáneo.
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
                                        Localidad = form["nombre_localidad"].ToString(),
                                        Nombre_DepAso = form["nombre_aso"].ToString(),
                                        Usuario = db.Users.Single(x => x.Id == user.Id)
                                    };

                                    db.Asociacion_Deportiva.Add(asociacion);
                                    break;
                                }
                        }

                        db.SaveChanges();

                        //ViewBag.Message = "El usuario " + user.Cedula + " se ha registrado correctamente";
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

        [HttpPost]
        public void Download(int Documento)
        {
             var v = db.Archivo.Where( x => x.ArchivoId == Documento).FirstOrDefault();

            if (v != null)
            {
                byte[] fileData = v.Contenido;
                Response.AddHeader("Content-type", v.Tipo);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + v.Nombre);

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
        //
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
                user.Nombre1 = editUser.Nombre1;
                user.Nombre2 = editUser.Nombre2;
                user.Apellido1 = editUser.Apellido1;
                user.Apellido2 = editUser.Apellido2;
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
                        sele.Nombre_Seleccion = form["sele_n"].ToString();
                        break;
                    case "Asociacion/Comite":
                        var aso = db.Asociacion_Deportiva.Single(x => x.Usuario.Id == editUser.Id);
                        aso.Nombre_DepAso = form["nombre_aso"].ToString();
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
                                Extension = Path.GetExtension(CV.FileName),
                                Tipo = CV.ContentType,
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
                ModelState.AddModelError("", "Something failed.");
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //
        // GET: /Users/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult IndexMasivo()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Import(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Seleccione un archivo Excel para cargar los datos<br/>";
                return View("IndexMasivo");
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {

                    int fin;
                    string terminacion;

                    if (excelfile.FileName.EndsWith("xlsx"))
                    {
                        fin = excelfile.FileName.Length - 5;
                        terminacion = ").xlsx";
                    }
                    else
                    {
                        fin = excelfile.FileName.Length - 4;
                        terminacion = ").xls";
                    }

                    string name = excelfile.FileName.Substring(0, fin);
                    string path = Server.MapPath("~/Content/Registros/Excel/" + name +
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
                    excelfile.SaveAs(path);

                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;

                    List<ApplicationUser> listUsrs = new List<ApplicationUser>();

                    for (int row = 2; row <= range.Rows.Count; row++)
                    {

                        var user = new ApplicationUser
                        {
                            UserName = ((Excel.Range)range.Cells[row, 1]).Text,
                            Nombre1 = ((Excel.Range)range.Cells[row, 2]).Text,
                            Nombre2 = ((Excel.Range)range.Cells[row, 3]).Text,
                            Apellido1 = ((Excel.Range)range.Cells[row, 4]).Text,
                            Apellido2 = ((Excel.Range)range.Cells[row, 5]).Text,
                            Sexo = (((Excel.Range)range.Cells[row, 8]).Text == "M") ? true : false,
                            Email = ((Excel.Range)range.Cells[row, 11]).Text,
                            PhoneNumber = ((Excel.Range)range.Cells[row, 12]).Text,
                            Fecha_Expiracion = DateTime.Now
                        };

                        string fecha = ((Excel.Range)range.Cells[row, 14]).Text;
                        string[] campos = fecha.Split('/');

                        user.Fecha_Nacimiento = DateTime.Parse(campos[1] + "/" + campos[0] + "/" + campos[2]);
                        user.Cedula = user.UserName;

                        var compPass = composicionPassword(user.Nombre1, user.Apellido1, user.Cedula, user.Fecha_Nacimiento);

                        var adminresult = await UserManager.CreateAsync(user, compPass);

                        if (adminresult.Succeeded) {
                            var result = await UserManager.AddToRoleAsync(user.Id, "Atleta");
                        }

                        listUsrs.Add(user);

                    }

                    ViewBag.ListUsrs = listUsrs;

                    workbook.Close();

                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                    foreach (System.Diagnostics.Process p in process)
                    {
                        if (!string.IsNullOrEmpty(p.ProcessName))
                        {
                            try { p.Kill(); }
                            catch { }
                        }
                    }

                    return View("Success");
                }

                else
                {
                    ViewBag.Error = "El tipo de archivo no es aceptado. <br>";
                    return View("IndexMasivo");
                }

            }

        }

        [HttpPost]
        public ActionResult Guardar()
        {
            return View("IndexMasivo");
        }
    }
}