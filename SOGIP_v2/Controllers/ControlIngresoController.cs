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

        public ActionResult Estadistica()
        {
            return View();
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
        public void addFemS(List<string> mes, int sele)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Month == m && c.Usuario.Sexo == false && c.Usuario == a.Usuario && a.SubSeleccion.SubSeleccionId == sele
                select c.Usuario
                ).Count();
                listF.Add(lista);
                mes.RemoveAt(0);
                addFemS(mes, sele);
            }


        }

        public void addMascS(List<string> mes, int sele)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Month == m && c.Usuario.Sexo == true && c.Usuario == a.Usuario && a.SubSeleccion.SubSeleccionId == sele
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addMascS(mes, sele);
            }


        }
        //--------------------------------------> Asociaciones
        public void addFemA(List<string> mes, int aso)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Month == m && c.Usuario.Sexo == false && c.Usuario == a.Usuario && a.Asociacion_Deportiva.Asociacion_DeportivaId == aso
                select c.Usuario
                ).Count();
                listF.Add(lista);
                mes.RemoveAt(0);
                addFemA(mes, aso);
            }


        }

        public void addMascA(List<string> mes, int aso)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Month == m && c.Usuario.Sexo == true && c.Usuario == a.Usuario && a.Asociacion_Deportiva.Asociacion_DeportivaId == aso
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addMascA(mes, aso);
            }


        }
        //--------------------------------------> Entidades
        public void addFemE(List<string> mes, int aso)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from e in db.Entidad_Publica
                where c.Fecha.Month == m && c.Usuario.Sexo == false && c.Usuario == e.Usuario && e.Tipo_Entidad.Tipo_EntidadId == aso
                select c.Usuario
                ).Count();
                listF.Add(lista);
                mes.RemoveAt(0);
                addFemE(mes, aso);
            }


        }

        public void addMascE(List<string> mes, int aso)
        {
            if (mes.Count > 0)
            {

                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from e in db.Entidad_Publica
                where c.Fecha.Month == m && c.Usuario.Sexo == true && c.Usuario == e.Usuario && e.Tipo_Entidad.Tipo_EntidadId == aso
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addMascE(mes, aso);
            }


        }
        //--------------------------------------> Funcionarios 

        public void addFemF(List<string> mes)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from f in db.Funcionario_ICODER
                where c.Fecha.Month == m && c.Usuario.Sexo == false && c.Usuario == f.Usuario
                select c.Usuario
                ).Count();
                listF.Add(lista);
                mes.RemoveAt(0);
                addFemF(mes);
            }
        }
        public void addMascF(List<string> mes)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from f in db.Funcionario_ICODER
                where c.Fecha.Month == m && c.Usuario.Sexo == true && c.Usuario == f.Usuario
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addMascF(mes);
            }
        }
        //---------------------------------------------------> Usuario
        public void addUs(List<string> mes, string ced)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Month == m && c.Usuario.Cedula == ced
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addUs(mes, ced);
            }
        }





        //--------------------------------------> Principal
        [HttpPost]
        public JsonResult PorMesAtletasF(string[] mes, int aso, int id)
        {
            listF.Clear();
            List<string> m = new List<string>(mes);

            switch (id)
            {
                case 1: { addFemS(m, aso); break; }
                case 2: { addFemA(m, aso); break; }
                case 3: { addFemE(m, aso); break; }
            }

            return new JsonResult { Data = listF, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult PorMesAtletasM(string[] mes, int aso, int id)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            switch (id)
            {
                case 1: { addMascS(m, aso); break; }
                case 2: { addMascA(m, aso); break; }
                case 3: { addMascE(m, aso); break; }
            }

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //------------------------------------------------------> Principal Funcionarios
        [HttpPost]
        public JsonResult PorMesFFunc(string[] mes)
        {
            listF.Clear();
            List<string> m = new List<string>(mes);
            addFemF(m);
            return new JsonResult { Data = listF, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public JsonResult PorMesMFunc(string[] mes)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addMascF(m);
            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //----------------------------------------------------> Usuario en específico
        [HttpPost]
        public JsonResult PorMesUsuario(string[] mes, string cedu)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addUs(m, cedu);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //--------------------->GRÁFICO LINEAL<---------------------
        //--------------------->Por mes todos los grupos<---------------------


        public void sele_all(List<string> mes)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Month == m && c.Usuario==a.Usuario && a.SubSeleccion!=null
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                sele_all(mes);
            }
        }
        public void aso_all(List<string> mes)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                from a in db.Atletas
                where c.Fecha.Month == m && c.Usuario == a.Usuario && a.Asociacion_Deportiva!=null
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                aso_all(mes);
            }
        }
        public void gub_all(List<string> mes)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Month == m && c.Usuario.Roles.FirstOrDefault().RoleId == "8"
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                gub_all(mes);
            }
        }

        public void fun_all(List<string> mes)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Month == m && c.Usuario.Roles.FirstOrDefault().RoleId == "7"
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                fun_all(mes);
            }
        }


        [HttpPost]
        public JsonResult allsele(string[] mes)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            sele_all(m);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult allaso(string[] mes)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            aso_all(m);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult allguber(string[] mes)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            gub_all(m);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult allfunc(string[] mes)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            fun_all(m);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }




        //--------------------->Por mes hombres, mujeres, todos<---------------------
        public void addallgen(List<string> mes, bool gen)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Month == m && c.Usuario.Sexo == gen
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addallgen(mes, gen);
            }
        }


        public void addallusers(List<string> mes)
        {
            if (mes.Count > 0)
            {
                int m = Int32.Parse(mes[0]);
                var lista = (
                from c in db.ControlIngreso
                where c.Fecha.Month == m
                select c.Usuario
                ).Count();
                listM.Add(lista);
                mes.RemoveAt(0);
                addallusers(mes);
            }
        }


        [HttpPost]
        public JsonResult allusers(string[] mes)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addallusers(m);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult allwomen(string[] mes)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addallgen(m,false);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult allmen(string[] mes)
        {
            listM.Clear();
            List<string> m = new List<string>(mes);
            addallgen(m, true);

            return new JsonResult { Data = listM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }




}