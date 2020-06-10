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
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            List<ListaUsuarios> lst;
            using (poligonoEntities db = new poligonoEntities())
            {
                lst = (from d in db.usuario
                       select new ListaUsuarios
                       {
                           Id = d.id_usuario,
                           usuario = d.nombre_usuario,
                           correo = d.email,
                           password = d.contrasena,
                           rol = d.id_rol
                       }).ToList();
            }


            //DROPDOWN DE ROLES

            List<ListaRoles> lst1;
            using (poligonoEntities db1 = new poligonoEntities())
            {
                lst1 = (from d1 in db1.rol
                       select new ListaRoles
                       {
                           Id = d1.idrol,
                           descripcion = d1.descripcion
                       }).ToList();
            }

            List<SelectListItem> items = lst1.ConvertAll(d1 =>
            {
                return new SelectListItem()
                {
                    Text = d1.descripcion.ToString(),
                    Value = d1.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = items;

            return View(lst);
        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(InsertUsuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        //string ePass = Encrypt.GetSHA256(contrasena);
                        var oUsuario = new usuario();
                        oUsuario.nombre_usuario = model.nombre;
                        oUsuario.email = model.correo;
                        oUsuario.contrasena = Encrypt.GetSHA256(model.contrasena);
                        oUsuario.id_rol = model.idrol;

                        db.usuario.Add(oUsuario);
                        db.SaveChanges();
                    }
                    return Redirect("~/Usuarios/");
                }
                return View(model);
                //return Redirect("/Rol");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public ActionResult Editar(int idactualiza)
        {
            InsertUsuario model = new InsertUsuario();

            using (poligonoEntities db = new poligonoEntities())
            {
                var oUsuario = db.usuario.Find(idactualiza);
                model.nombre = oUsuario.nombre_usuario;
                model.correo = oUsuario.email;
                model.contrasena = oUsuario.contrasena;
                model.idrolact = oUsuario.id_rol;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(InsertUsuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        //string ePass = Encrypt.GetSHA256(contrasena);
                        var oUsuario = db.usuario.Find(model.idactualiza);
                        oUsuario.nombre_usuario = model.nombre;
                        oUsuario.email = model.correo;
                        oUsuario.contrasena = Encrypt.GetSHA256(model.contrasena);
                        oUsuario.id_rol = model.idrolact;

                        db.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Usuarios/");
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
        public ActionResult Eliminar(int idelimina)
        {
            using (poligonoEntities db = new poligonoEntities())
            {
                var oUsuario = db.usuario.Find(idelimina);
                db.usuario.Remove(oUsuario);
                db.SaveChanges();
            }
            return Redirect("~/Usuarios/");
        }


    }
}