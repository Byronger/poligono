using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using poligonotiro.Models;
using poligonotiro.Models.ViewModels;

namespace poligonotiro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string message = "", string message1 = "")
        {
            ViewBag.Message = message;
            ViewBag.Message1 = message1;
            return View();
        }

        [HttpPost]
       public ActionResult Login(string email, string password)
        {

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                using (poligonoEntities db = new poligonoEntities())
                {
                    var oUsuario = new usuario();
                    string ePass = Encrypt.GetSHA256(password);
                    var usermail = db.usuario.FirstOrDefault(e => e.email == email && e.contrasena == ePass);

                    if (usermail != null)
                    {
                        //se encontraron datos
                        FormsAuthentication.SetAuthCookie(usermail.email, true);
                        return RedirectToAction("Index", "Profile");
                    }
                    else
                    {
                        return RedirectToAction("Index", new { message = "No se reconocen los datos, Intentelo de nuevo."});
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", new { message = "Llene los campos para iniciar sesión." });
            }
        }

        public ActionResult Registro(string emailregistro, string userregistro, string passwordregistro, string passwordconfirmregistro)
        {

            if (!string.IsNullOrEmpty(emailregistro) && !string.IsNullOrEmpty(userregistro) && !string.IsNullOrEmpty(passwordregistro) && !string.IsNullOrEmpty(passwordconfirmregistro))
            {
                if (passwordregistro != passwordconfirmregistro)
                {
                    return RedirectToAction("Index", new { message = "Las contraseñas no coinciden, intentelo de nuevo." });
                }
                else
                {
                    try
                            {
                                if (ModelState.IsValid)
                                {
                                    using (poligonoEntities db1 = new poligonoEntities())
                                    {
                                        var oUsuario1 = new usuario();
                                        oUsuario1.nombre_usuario = userregistro; 
                                        oUsuario1.email = emailregistro;
                                        oUsuario1.contrasena = Encrypt.GetSHA256(passwordregistro);
                                        oUsuario1.id_rol = 2;

                                        //para desencriptar
                                        //string pass = "123";
                                        //string ePass = Encrypt.GetSHA256(pass);

                                        db1.usuario.Add(oUsuario1);
                                        db1.SaveChanges();
                                    }
                                    return RedirectToAction("Index", new { message1 = "Felicidades, su registro se ha realizado éxitosamente." });
                                }
                                //return View(model);
                                return Redirect("/Home");
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                }
            }
            else
            {
                return RedirectToAction("Index", new { message = "Llene todos los campos para poder registrarse" });
            }
        }



        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}