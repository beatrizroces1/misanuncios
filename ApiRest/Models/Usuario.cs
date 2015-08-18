using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido1 { get; set; }
        [Required]
        public string apellido2 { get; set; }
        [Required]
        public string ciudad { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
      
       
       
    }
    public class UsuarioInicioSesion
    {
        public int ID { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class UsuarioCambiarNombre
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "es obligatorio")]    
        public string nombreNuevo;
        [Required(ErrorMessage = "es obligatorio")]
        public string apellido1Nuevo;
        [Required(ErrorMessage = "es obligatorio")] 
        public string apellido2Nuevo;

    }
   
}