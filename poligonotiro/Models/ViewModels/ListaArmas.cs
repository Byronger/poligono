using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace poligonotiro.Models.ViewModels
{
    public class ListaArmas
    {
        public int Idarma { get; set; }
        public string nombrearma { get; set; }
        public string modelo { get; set; }
        public string foto { get; set; }
        public string numeroserie { get; set; }

    }

}