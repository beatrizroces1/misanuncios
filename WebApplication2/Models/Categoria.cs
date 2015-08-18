using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria"), StringLength(100)]
        public string descripcion { get; set; }

    }
}