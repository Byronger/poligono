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
    public class LineaTiroController : Controller
    {
        // GET: LineaTiro
        public ActionResult Index()
        {
            List<ListaLineaTiro> lst;
            using (poligonoEntities db = new poligonoEntities())
            {
                lst = (from d in db.linea_tiro
                       select new ListaLineaTiro
                       {
                           Idlineatiro = d.id_linea_tiro,
                           nombre = d.nombre_linea_tiro,
                       }).ToList();
            }

            return View(lst);
        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(ListaLineaTiro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        var oLineaTiro = new linea_tiro();
                        oLineaTiro.nombre_linea_tiro = model.nombre;

                        db.linea_tiro.Add(oLineaTiro);
                        db.SaveChanges();
                    }
                    return Redirect("~/LineaTiro/");
                }
                return View(model);
                //return Redirect("/Rol");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public ActionResult Editar(int Idlineatiro)
        {
            ListaLineaTiro model = new ListaLineaTiro();

            using (poligonoEntities db = new poligonoEntities())
            {
                var oLineaTiro = db.linea_tiro.Find(Idlineatiro);
                model.nombre = oLineaTiro.nombre_linea_tiro;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(ListaLineaTiro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        var oLineaTiro = db.linea_tiro.Find(model.Idlineatiro);
                        oLineaTiro.nombre_linea_tiro = model.nombre;

                        db.Entry(oLineaTiro).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/LineaTiro/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int idlineatiro)
        {
            using (poligonoEntities db = new poligonoEntities())
            {
                var oLineaTiro = db.linea_tiro.Find(idlineatiro);
                db.linea_tiro.Remove(oLineaTiro);
                db.SaveChanges();
            }
            return Redirect("~/LineaTiro/");
        }
    }
}