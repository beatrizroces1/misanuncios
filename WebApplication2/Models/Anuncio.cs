using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Anuncio
    {
        public int ID { get; set; }
        public int id_usuario { get; set; }
        [Required(ErrorMessage = "La categoría es obligatoria")]   
        public string categoria { get; set; }
        [Required(ErrorMessage = "El título es obligatorio"), StringLength(50)]
        public string titulo { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria"),StringLength(300)]
        public string descripcion { get; set; }
        public DateTime fechaPublicacion { get; set;  }
        public string ciudad { get; set; }



    }
   
}