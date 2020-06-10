using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace poligonotiro.Models.ViewModels
{
    public class InsertRol
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "descripcion")]
        public string descripcion { get; set; }


    }
}