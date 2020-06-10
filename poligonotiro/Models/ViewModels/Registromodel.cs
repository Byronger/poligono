using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace poligonotiro.Models.ViewModels
{
    public class Registromodel
    {

        public string nombre_usuario { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
        public int id_rol { get; set; }



    }
}