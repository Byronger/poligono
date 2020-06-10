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
    public class ArmasController : Controller
    {
        // GET: Armas
        public ActionResult Index()
        {
            List<ListaArmas> lst;
            using (poligonoEntities db = new poligonoEntities())
            {
                lst = (from d in db.arma
                       select new ListaArmas
                       {
                           Idarma = d.id_arma,
                           nombrearma = d.nombre_arma,
                           modelo = d.modelo,
                           foto = d.foto,
                           numeroserie = d.numero_serie,
                       }).ToList();
            }

            return View(lst);
        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(ListaArmas model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        var oArma = new arma();
                        oArma.nombre_arma = model.nombrearma;
                        oArma.modelo = model.modelo;
                        oArma.foto = model.foto;
                        oArma.numero_serie = model.numeroserie;

                        db.arma.Add(oArma);
                        db.SaveChanges();
                    }
                    return Redirect("~/Armas/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public ActionResult Editar(int Idarma)
        {
            ListaArmas model = new ListaArmas();

            using (poligonoEntities db = new poligonoEntities())
            {
                var oArma = db.arma.Find(Idarma);
                model.nombrearma = oArma.nombre_arma;
                model.modelo = oArma.modelo;
                model.foto = oArma.foto;
                model.numeroserie = oArma.numero_serie;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(ListaArmas model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        var oArma = db.arma.Find(model.Idarma);
                        oArma.nombre_arma = model.nombrearma;
                        oArma.modelo = model.modelo;
                        oArma.foto = model.foto;
                        oArma.numero_serie = model.numeroserie;

                        db.Entry(oArma).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Armas/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int idarma)
        {
            using (poligonoEntities db = new poligonoEntities())
            {
                var oArma = db.arma.Find(idarma);
                db.arma.Remove(oArma);
                db.SaveChanges();
            }
            return Redirect("~/Armas/");
        }
    }
}