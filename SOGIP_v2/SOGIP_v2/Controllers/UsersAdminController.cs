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

namespace SOGIP_v2.Controllers
{
    //[Authorize(Roles = "Supervisor")]
    [Authorize(Roles = "Administrador,Supervisor")]
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
            return View(await UserManager.Users.ToListAsync());
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
           
            //Entities List
            var getEntidad = db.Tipo_Entidad.ToList();
            SelectList listaEntidades = new SelectList(getEntidad, "Tipo_EntidadId", "Descripcion");
            ViewBag.Entidades = listaEntidades;

            //Sport List
            var getDeporte = db.Deportes.ToList();
            SelectList listaDeportes = new SelectList(getDeporte, "DeporteId", "Nombre");
            ViewBag.Deportes = listaDeportes;

            //Category List
            var getCategoria = db.Categorias.ToList();
            SelectList listCategorías = new SelectList(getCategoria, "CategoriaId", "Descripcion");
            ViewBag.Categorias = listCategorías;

            //Seleccion List
            var getSeleccion = db.Selecciones.ToList();
            SelectList listaSelecciones = new SelectList(getSeleccion, "SeleccionId", "Nombre_Seleccion");
            ViewBag.Selecciones = listaSelecciones;

            //Aso List
            var getAsociaciones = db.Asociacion_Deportiva.ToList();
            SelectList listAsociaciones = new SelectList(getAsociaciones, "Asociacion_DeportivaId", "Nombre_DepAso");
            ViewBag.Asociaciones = listAsociaciones;

            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Id", "Name");

            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, string Atleta_Tipo, int? selectedS, int? SelectedAsox, int? SelectedEntity, string selectedRoles, int? SelectedCategory, int? SelectedSport, FormCollection form)
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
                    Fecha_Expiracion = DateTime.Now,
                    Sexo = userViewModel.Sexo
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

                            //no sé por que diablos, pero cuando concateno el nombre
                            //de selección, se crean espacios y separa mucho los nombres
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
                            return View();
                        }
                    }
                }

                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());


                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }

                // If everything it's ok.
                return RedirectToAction("Index");
            }

            //Entities List
            var getEntidad = db.Tipo_Entidad.ToList();
            SelectList listaEntidades = new SelectList(getEntidad, "Tipo_EntidadId", "Descripcion");
            ViewBag.Entidades = listaEntidades;

            //Sport List
            var getDeporte = db.Deportes.ToList();
            SelectList listaDeportes = new SelectList(getDeporte, "DeporteId", "Nombre");
            ViewBag.Deportes = listaDeportes;

            //Category List
            var getCategoria = db.Categorias.ToList();
            SelectList listCategorías = new SelectList(getCategoria, "CategoriaId", "Descripcion");
            ViewBag.Categorias = listCategorías;

            //Seleccion List
            var getSeleccion = db.Selecciones.ToList();
            SelectList listaSelecciones = new SelectList(getSeleccion, "SeleccionId", "Nombre_Seleccion");
            ViewBag.Selecciones = listaSelecciones;

            //Aso List
            var getAsociaciones = db.Asociacion_Deportiva.ToList();
            SelectList listAsociaciones = new SelectList(getAsociaciones, "Asociacion_DeportivaId", "Nombre_DepAso");
            ViewBag.Asociaciones = listAsociaciones;

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
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Cedula = user.Cedula,
                Nombre1 = user.Nombre1,
                Nombre2 = user.Nombre2,
                Apellido1 = user.Apellido1,
                Apellido2 = user.Apellido2,
                Fecha_Nacimiento = user.Fecha_Nacimiento,
                //Sexo = user.Sexo,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id,UserName,Nombre1,Nombre2,Apellido1,Apellido2")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.UserName;
                user.Cedula = editUser.UserName;
                user.Email = editUser.Email;
                user.Nombre1 = editUser.Nombre1;
                user.Nombre2 = editUser.Nombre2;
                user.Apellido1 = editUser.Apellido1;
                user.Apellido2 = editUser.Apellido2;
                //user.Fecha_Nacimiento = editUser.Fecha_Nacimiento;
                //user.Sexo = edirtUser.Sexo;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
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
                    string path = Server.MapPath("~/Content/Registros/" + name +
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