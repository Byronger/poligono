using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using poligonotiro.Models;
using poligonotiro.Models.ViewModels;

namespace poligonotiro.Controllers
{
    [Authorize]
    public class SupervisoresController : Controller
    {
        // GET: Supervisores
        public ActionResult Index()
        {
            List<ListaSupervisores> lst;
            using (poligonoEntities db = new poligonoEntities())
            {
                lst = (from d in db.supervisor
                       select new ListaSupervisores
                       {
                           Idsupervisor = d.id_supervisor,
                           nombre = d.nombre_supervisor,
                       }).ToList();
            }

            return View(lst);
        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(ListaSupervisores model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        //string ePass = Encrypt.GetSHA256(contrasena);
                        var oSupervisor = new supervisor();
                        oSupervisor.nombre_supervisor = model.nombre;

                        db.supervisor.Add(oSupervisor);
                        db.SaveChanges();
                    }
                    return Redirect("~/Supervisores/");
                }
                return View(model);
                //return Redirect("/Rol");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public ActionResult Editar(int Idsupervisor)
        {
            ListaSupervisores model = new ListaSupervisores();

            using (poligonoEntities db = new poligonoEntities())
            {
                var oSupervisor = db.supervisor.Find(Idsupervisor);
                model.nombre = oSupervisor.nombre_supervisor;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(ListaSupervisores model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        var oSupervisor = db.supervisor.Find(model.Idsupervisor);
                        oSupervisor.nombre_supervisor = model.nombre;

                        db.Entry(oSupervisor).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Supervisores/");
                }
                return View(model);
                //return Redirect("/Rol");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int idsupervisor)
        {
            using (poligonoEntities db = new poligonoEntities())
            {
                var oSupervisor = db.supervisor.Find(idsupervisor);
                db.supervisor.Remove(oSupervisor);
                db.SaveChanges();
            }
            return Redirect("~/Supervisores/");
        }
    }
}