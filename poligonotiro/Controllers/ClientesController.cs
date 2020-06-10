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
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            List<ListaClientes> lst;
            using (poligonoEntities db = new poligonoEntities())
            {
                lst = (from d in db.cliente
                       select new ListaClientes
                       {
                           Id = d.id_cliente,
                           nombrecliente = d.nombre_cliente,
                           documento = d.documento,
                           telefono = d.telefono,
                           direccion = d.direccion,
                           correo = d.email,
                           portearma = d.porte_arma,
                           registroarma = d.registro_arma,
                           idusuario = d.id_usuario
                       }).ToList();
            }


            //DROPDOWN DE USUARIOS

            List<ListaUsuarios> lst1;
            using (poligonoEntities db1 = new poligonoEntities())
            {
                lst1 = (from d1 in db1.usuario
                        select new ListaUsuarios
                        {
                            Id = d1.id_usuario,
                            usuario = d1.nombre_usuario
                        }).ToList();
            }

            List<SelectListItem> items = lst1.ConvertAll(d1 =>
            {
                return new SelectListItem()
                {
                    Text = d1.usuario.ToString(),
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
        public ActionResult Nuevo(ListaClientes model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        var oCliente = new cliente();
                        oCliente.nombre_cliente = model.nombrecliente;
                        oCliente.documento = model.documento;
                        oCliente.telefono = model.telefono;
                        oCliente.direccion = model.direccion;
                        oCliente.email = model.correo;
                        oCliente.porte_arma = model.portearma;
                        oCliente.registro_arma = model.registroarma;
                        oCliente.id_usuario = model.idusuario;

                        db.cliente.Add(oCliente);
                        db.SaveChanges();
                    }
                    return Redirect("~/Clientes/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public ActionResult Editar(int idactualiza)
        {
            ListaClientes model = new ListaClientes();

            using (poligonoEntities db = new poligonoEntities())
            {
                var oCliente = db.cliente.Find(idactualiza);
                model.nombrecliente = oCliente.nombre_cliente;
                model.documento = oCliente.email;
                model.telefono = oCliente.telefono;
                model.direccion = oCliente.direccion;
                model.correo = oCliente.email;
                model.portearma = oCliente.porte_arma;
                model.registroarma = oCliente.registro_arma;
                model.idusuario = oCliente.id_usuario;
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Editar(ListaClientes model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (poligonoEntities db = new poligonoEntities())
                    {
                        var oCliente = db.cliente.Find(model.Idactualiza);
                        oCliente.nombre_cliente = model.nombrecliente;
                        oCliente.documento = model.documento;
                        oCliente.telefono = model.telefono;
                        oCliente.direccion = model.direccion;
                        oCliente.email = model.correo;
                        oCliente.porte_arma = model.portearma;
                        oCliente.registro_arma = model.registroarma;
                        oCliente.id_usuario = model.idusuarioact;

                        db.Entry(oCliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Clientes/");
                }
                return View(model);
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
                var oCliente = db.cliente.Find(idelimina);
                db.cliente.Remove(oCliente);
                db.SaveChanges();
            }
            return Redirect("~/Clientes/");
        }


    }
}