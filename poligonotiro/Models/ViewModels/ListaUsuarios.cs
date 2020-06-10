using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace poligonotiro.Models.ViewModels
{
    public class ListaUsuarios
    {
        public int Id { get; set; }
        public string usuario { get; set; }

        public string correo { get; set; }

        public string password { get; set; }

        public int rol { get; set; }
    }

}