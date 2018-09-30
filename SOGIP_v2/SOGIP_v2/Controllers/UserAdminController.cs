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

namespace SOGIP_v2.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UsersAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersAdminController()
        {
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
            SelectList listE = new SelectList(getEntidad, "Tipo_EntidadId", "Descripcion");
            ViewBag.Entidades = listE;

            //Sport List
            var getDeporte = db.Deportes.ToList();
            SelectList listD = new SelectList(getDeporte, "DeporteId", "Nombre");
            ViewBag.Deportes = listD;

            //Category List
            var getCategoria = db.Categorias.ToList();
            SelectList listC = new SelectList(getCategoria, "CategoriaId", "Descripcion");
            ViewBag.Categorias = listC;

            //Seleccion List
            var getSeleccion = db.Selecciones.ToList();
            SelectList listS = new SelectList(getSeleccion, "SeleccionId", "Nombre_Seleccion");
            ViewBag.Selecciones = listS;

            //Get the list of Roles
            var getRoles = db.Roles.ToList();
            SelectList listR = new SelectList(getRoles, "Id", "Name");
            ViewBag.RoleId = listR;


            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Id", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, string selectedRoles, int SelectedCategory, int SelectedSport, FormCollection form)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser { UserName = userViewModel.Cedula, Email = userViewModel.Email, Nombre1 = userViewModel.Nombre1,
                                                 Nombre2 = userViewModel.Nombre2, Apellido1 = userViewModel.Apellido1, Apellido2 = userViewModel.Apellido2,
                                                 Cedula = userViewModel.Cedula, Fecha_Nacimiento = DateTime.Now, Fecha_Expiracion = DateTime.Now,
                                                 Sexo = true };

                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);             
                
                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);


                        switch (selectedRoles) {

                            case "Entrenador":
                                Entrenador entrenador = new Entrenador()
                                 {
                                     Usuario_Id = db.Users.Single(x => x.Id == user.Id)
                                };
                                 db.Entrenadores.Add(entrenador);
                                break;


                            case "Seleccion":
                                Seleccion seleccion = new Seleccion()
                                {
                                    Nombre_Seleccion = "Seleccion de",
                                  Usuario=db.Users.Single(x=>x.Id==user.Id),
                                  Deporte_Id= db.Deportes.Single(x=>x.DeporteId==SelectedSport), 
                                  Categoria_Id= db.Categorias.Single(x=>x.CategoriaId==SelectedCategory),

                                };
                                db.Selecciones.Add(seleccion);
                                break;

                            case "Asociacion":
                                Asociacion_Deportiva asociacion = new Asociacion_Deportiva()
                                {
                                    Localidad=form["nombre_localidad"].ToString(),
                                    Usuario_Id= db.Users.Single(x => x.Id == user.Id)

                                };
                                db.Asociacion_Deportiva.Add(asociacion);
                                break;

                        
                    }
                        db.SaveChanges();

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
                return RedirectToAction("Index");
            }
            //Sport List
            var getDeporte = db.Deportes.ToList();
            SelectList listD = new SelectList(getDeporte, "DeporteId", "Nombre");
            ViewBag.Deportes = listD;

            //Category List
            var getCategoria = db.Categorias.ToList();
            SelectList listC = new SelectList(getCategoria, "CategoriaId", "Descripcion");
            ViewBag.Categorias = listC;

            //Seleccion List
            var getSeleccion = db.Selecciones.ToList();
            SelectList listS = new SelectList(getSeleccion, "SeleccionId", "Nombre_Seleccion");
            ViewBag.Selecciones = listS;
            //Entities List

            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");

            return View();
        }

        

        //
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
    }
}