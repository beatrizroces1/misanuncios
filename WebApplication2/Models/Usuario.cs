using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio"), StringLength(50)]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El primer apellido es obligatorio"), StringLength(50)]
        public string apellido1 { get; set; }
        [Required(ErrorMessage = "El segundo apellido es obligatorio"), StringLength(50)]
        public string apellido2 { get; set; }
        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public string ciudad { get; set; }
        [Required(ErrorMessage = "El email es obligatorio"), StringLength(100)]
        [EmailAddress]
        public string email { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria"), StringLength(10)]
        public string password { get; set; }
      
       
       
    }
    public class UsuarioInicioSesion
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string email { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
   
   
}