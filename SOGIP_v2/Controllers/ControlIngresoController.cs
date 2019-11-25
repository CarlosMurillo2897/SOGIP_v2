using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOGIP_v2.Models;

namespace SOGIP_v2.Controllers
{
    //[Authorize(Roles = "Administrador,Supervisor")]
    public class ControlIngresoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<int> listF = new List<int>();
        private List<int> listM = new List<int>();
        // GET: ControlIngreso
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ingreso(string id)
        {
            var consulta = db.Archivo.Where(x => x.Usuario.Cedula == id).FirstOrDefault();
            return Json(consulta, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveIngreso(string id)
        {
            ApplicationUser User = db.Users.Single(x => x.Cedula == id);
            ControlIngreso nueva = new ControlIngreso();
            DateTime fecha = DateTime.Now;
            try
            {
                if (User != null)
                {
                    nueva.Usuario = User;
                    nueva.Fecha = fecha;
                    db.ControlIngreso.Add(nueva);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(nueva, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador,Supervisor")]
        public ActionResult Estadistica()
        {
            return View();
        }

        //------> Girl, I travel round the world and even sail the seven seas.
        //------> Across the universe I go to other galaxies ♪ ♫


        [HttpPost]
        public JsonResult years() //------->selecciones
        {
            var lista = db.ControlIngreso.GroupBy(x => x.Fecha.Year)
                .Where(g => g.Count() > 1 || g.Count() == 1)
                .Select(g => g.Key)
                .ToList();

            lista.Reverse();
            return new JsonResult { Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult getUsuariosA()
        {
            var consulta =
                  from u in db.Users
                  from r in db.Roles
                  where (u.Roles.FirstOrDefault().RoleId == "5" || u.Roles.FirstOrDefault().RoleId == "6" || u.Roles.FirstOrDefault().RoleId == "7" || u.Roles.FirstOrDefault().RoleId == "8" || u.Roles.FirstOrDefault().RoleId == "9")
                  && u.Roles.FirstOrDefault().RoleId.Equals(r.Id)
                  select new
                  {
                      Accion = "",
                      Cedula = u.Cedula,
                      Nombre = u.Nombre1,
                      Apellido1 = u.Apellido1,
                      Apellido2 = u.Apellido2,
                      Rol = r.Name
                  };
            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Selecciones() //------->selecciones
        {
            var lista = (
                from s in db.Selecciones
                select new
                {
                    Id = s.SeleccionId,
                    Nombre = s.Nombre_Seleccion
                }
                ).ToList();


            return new JsonResult { Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public JsonResult Asociaciones() //------->asociaciones
        {
            var lista = (
                from a in db.Asociacion_Deportiva
                select new
                {
                    Id = a.Asociacion_DeportivaId,
                    Nombre = a.Nombre_DepAso
                }
                ).ToList();


            return new JsonResult { Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult Entidades() //------->entidades públicas
        {
            var lista = (
                from e in db.Tipo_Entidad
                select new
                {
                    Id = e.Tipo_EntidadId,
                    Nombre = e.Descripcion
                }
                ).ToList();


            return new JsonResult { Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //-------------------------------------------------------> seleccciones
        public void addFemS(List<string> mes, int sele, int an)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Sexo == false && c.Usuario == a.Usuario && a.SubSeleccion.SubSeleccionId == sele
                select c.Usuario
                ).Count();
                listF.Add(lista);
                mes.RemoveAt(0);
                addFemS(mes, sele, an);
            }


        }

        public void addMascS(List<string> mes, int sele, int an)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Sexo == true && c.Usuario == a.Usuario && a.SubSeleccion.SubSeleccionId == sele
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addMascS(mes, sele, an);
            }


        }
        //--------------------------------------> Asociaciones
        public void addFemA(List<string> mes, int aso, int an)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Sexo == false && c.Usuario == a.Usuario && a.Asociacion_Deportiva.Asociacion_DeportivaId == aso
                select c.Usuario
                ).Count();
                listF.Add(lista);
                mes.RemoveAt(0);
                addFemA(mes, aso, an);
            }


        }

        public void addMascA(List<string> mes, int aso, int an)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Sexo == true && c.Usuario == a.Usuario && a.Asociacion_Deportiva.Asociacion_DeportivaId == aso
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addMascA(mes, aso, an);
            }


        }
        //--------------------------------------> Entidades
        public void addFemE(List<string> mes, int aso, int an)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from e in db.Entidad_Publica
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Sexo == false && c.Usuario == e.Usuario && e.Tipo_Entidad.Tipo_EntidadId == aso
                select c.Usuario
                ).Count();
                listF.Add(lista);
                mes.RemoveAt(0);
                addFemE(mes, aso, an);
            }


        }

        public void addMascE(List<string> mes, int aso, int an)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from e in db.Entidad_Publica
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Sexo == true && c.Usuario == e.Usuario && e.Tipo_Entidad.Tipo_EntidadId == aso
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addMascE(mes, aso, an);
            }


        }
        //--------------------------------------> Funcionarios 

        public void addFemF(List<string> mes, int an)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from f in db.Funcionario_ICODER
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Sexo == false && c.Usuario == f.Usuario
                select c.Usuario
                ).Count();
                listF.Add(lista);
                mes.RemoveAt(0);
                addFemF(mes, an);
            }
        }
        public void addMascF(List<string> mes, int an)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from f in db.Funcionario_ICODER
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Sexo == true && c.Usuario == f.Usuario
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addMascF(mes, an);
            }
        }
        //---------------------------------------------------> Usuario
        public void addUs(List<string> mes, string ced, int an)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Cedula == ced
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addUs(mes, ced, an);
            }
        }





        //--------------------------------------> Principal
        [HttpPost]
        public JsonResult PorMesAtletasF(string[] mes, int aso, int id, int an)
        {
            listF.Clear();
            List<string> m = new List<string>(mes);

            switch (id)
            {
                case 1: { addFemS(m, aso, an); break; }
                case 2: { addFemA(m, aso, an); break; }
                case 3: { addFemE(m, aso, an); break; }
            }

            return new JsonResult { Data = listF, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult PorMesAtletasM(string[] mes, int aso, int id, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            switch (id)
            {
                case 1: { addMascS(m, aso, an); break; }
                case 2: { addMascA(m, aso, an); break; }
                case 3: { addMascE(m, aso, an); break; }
            }

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //------------------------------------------------------> Principal Funcionarios
        [HttpPost]
        public JsonResult PorMesFFunc(string[] mes, int an)
        {
            listF.Clear();
            List<string> m = new List<string>(mes);
            addFemF(m, an);
            return new JsonResult { Data = listF, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult PorMesMFunc(string[] mes, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addMascF(m, an);
            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //----------------------------------------------------> Usuario en específico
        [HttpPost]
        public JsonResult PorMesUsuario(string[] mes, string cedu, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addUs(m, cedu, an);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //--------------------->GRÁFICO LINEAL<---------------------
        //--------------------->Por mes todos los grupos<---------------------


        public void sele_all(List<string> mes, int an)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario==a.Usuario && a.SubSeleccion!=null
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                sele_all(mes, an);
            }
        }
        public void aso_all(List<string> mes, int an)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario == a.Usuario && a.Asociacion_Deportiva!=null
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                aso_all(mes, an);
            }
        }
        public void gub_all(List<string> mes, int an)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Roles.FirstOrDefault().RoleId == "8"
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                gub_all(mes, an);
            }
        }

        public void fun_all(List<string> mes, int an)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Year==an && c.Fecha.Month == m && c.Usuario.Roles.FirstOrDefault().RoleId == "7"
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                fun_all(mes, an);
            }
        }


        [HttpPost]
        public JsonResult allsele(string[] mes, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            sele_all(m, an);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult allaso(string[] mes, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            aso_all(m, an);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult allguber(string[] mes, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            gub_all(m, an);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult allfunc(string[] mes, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            fun_all(m, an);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }




        //--------------------->Por mes hombres, mujeres, todos<---------------------
        public void addallgen(List<string> mes, bool gen, int an)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Year == an && c.Fecha.Month == m && c.Usuario.Sexo == gen
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addallgen(mes, gen, an);
            }
        }


        public void addallusers(List<string> mes, int an)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Year == an && c.Fecha.Month == m
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addallusers(mes, an);
            }
        }


        [HttpPost]
        public JsonResult allusers(string[] mes, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addallusers(m, an);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult allwomen(string[] mes, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addallgen(m,false, an);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult allmen(string[] mes, int an)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addallgen(m, true, an);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }




}