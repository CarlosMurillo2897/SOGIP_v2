﻿using SOGIP_v2.Models;
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

namespace SOGIP_v2.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UsersAdminController : Controller
    {
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
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
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
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }


        public ActionResult CreateMasive()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> CreateMasive(HttpPostedFileBase excelfile, RegisterViewModel userViewModel)
        //{

        //    if (excelfile == null || excelfile.ContentLength == 0)
        //    {
        //        ViewBag.Error = "Seleccione un archivo Excel para cargar los datos<br>";
        //        return View();
        //    }
        //    else
        //    {
        //        if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
        //        {
        //            int fin;
        //            string terminacion;

        //            if (excelfile.FileName.EndsWith("xlsx"))
        //            {
        //                fin = excelfile.FileName.Length - 5;
        //                terminacion = ").xlsx";
        //            }
        //            else
        //            {
        //                fin = excelfile.FileName.Length - 4;
        //                terminacion = ").xls";
        //            }

        //            string name = excelfile.FileName.Substring(0, fin);

        //            string path = Server.MapPath("~/Content/Registros/" + name +
        //                                         "(" + DateTime.Now.Year.ToString() + "-"
        //                                         + DateTime.Now.Month.ToString() + "-"
        //                                         + DateTime.Now.Day.ToString() + ")-("
        //                                         + DateTime.Now.Hour.ToString() + "-"
        //                                         + DateTime.Now.Minute.ToString() + "-"
        //                                         + DateTime.Now.Second.ToString() + terminacion);

        //            if (System.IO.File.Exists(path))
        //                System.IO.File.Delete(path);
        //            excelfile.SaveAs(path);

        //            Excel.Application application = new Excel.Application();
        //            Excel.Workbook workbook = application.Workbooks.Open(path);
        //            Excel.Worksheet worksheet = workbook.ActiveSheet;
        //            Excel.Range range = worksheet.UsedRange;

        //            List<ApplicationUser> usuarios = new List<ApplicationUser>();

        //            for (int row = 2; row <= range.Rows.Count; row++)
        //            {
        //                ApplicationUser user = new ApplicationUser();

        //                user.
        //                a.usuario = u.id;

        //                u.cedula = ((Excel.Range)range.Cells[row, 1]).Text;

        //                a.nombre = ((Excel.Range)range.Cells[row, 2]).Text;
        //                a.apellido1 = ((Excel.Range)range.Cells[row, 4]).Text;
        //                a.apellido2 = ((Excel.Range)range.Cells[row, 5]).Text;

        //                u.contrasena = "P@ssw0rd";

        //                a.correo = ((Excel.Range)range.Cells[row, 11]).Text;
        //                a.telefono = ((Excel.Range)range.Cells[row, 12]).Text;
        //                a.genero = (((Excel.Range)range.Cells[row, 13]).Text == "M") ? Byte.MaxValue : Byte.MinValue;

        //                string fecha = ((Excel.Range)range.Cells[row, 14]).Text;
        //                string[] campos = fecha.Split('/');
        //                a.fecha_nacimiento = System.DateTime.Parse(campos[1] + "/" + campos[0] + "/" + campos[2]);

        //                u.fecha_expiracion = System.DateTime.Now;

        //                usrs.Add(u);
        //                db.SOGIP_Usuario.Add(u);
        //                db.SaveChanges();

        //                aths.Add(a);
        //                db.SOGIP_Atleta.Add(a);
        //                db.SaveChanges();

        //            }

        //            ViewBag.ListUsrs = usrs;
        //            ViewBag.ListAths = aths;

        //            workbook.Close();

        //            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
        //            foreach (System.Diagnostics.Process p in process)
        //            {
        //                if (!string.IsNullOrEmpty(p.ProcessName))
        //                {
        //                    try { p.Kill(); }
        //                    catch { }
        //                }
        //            }

        //            return View();
        //        }
        //        else
        //        {
        //            ViewBag.Error = "El tipo de archivo no es aceptado. <br>";
        //            return View();
        //        }
        //    }



        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = userViewModel.Cedula,
        //            Email = userViewModel.Email,
        //            Nombre1 = userViewModel.Nombre1,
        //            Nombre2 = userViewModel.Nombre2,
        //            Apellido1 = userViewModel.Apellido1,
        //            Apellido2 = userViewModel.Apellido2,
        //            Cedula = userViewModel.Cedula,
        //            Fecha_Nacimiento = DateTime.Now,
        //            Fecha_Expiracion = DateTime.Now,
        //            Sexo = true
        //        };

        //        var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

        //        if (adminresult.Succeeded)
        //        {
        //            /*var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
        //            if (!result.Succeeded)
        //            {
        //                ModelState.AddModelError("", result.Errors.First());
        //                ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
        //                return View();
        //            }*/
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", adminresult.Errors.First());
        //            return View();

        //        }
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}


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