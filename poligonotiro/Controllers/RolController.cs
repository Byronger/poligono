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
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            List<ListaRoles> lst;
            using (poligonoEntities db = new poligonoEntities())
            {
                lst = (from d in db.rol
                       select new ListaRoles
                       {
                           Id = d.idrol,
                           descripcion = d.descripcion
                       }).ToList();
            }

            return View(lst);
        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(InsertRol model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        var oRol = new rol();
                        oRol.descripcion = model.descripcion;

                        db.rol.Add(oRol);
                        db.SaveChanges();
                    }
                    return Redirect("~/Rol/");
                }
                return View(model);
                //return Redirect("/Rol");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}