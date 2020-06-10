using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace poligonotiro.Models.ViewModels
{
    public class ListaClientes
    {
        public int Id { get; set; }
        public string nombrecliente { get; set; }
        public string documento { get; set; }
        public int telefono { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string portearma { get; set; }
        public string registroarma { get; set; }
        public int idusuario { get; set; }
        public int idusuarioact { get; set; }
        public int Idactualiza { get; set; }
    }

}