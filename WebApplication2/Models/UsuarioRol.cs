using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class UsuarioRol
    {
        public int ID { get; set; }
        public int id_rol { get; set; }
        public int id_usuario { get; set; }

        public UsuarioRol(int id_rol, int id_usuario)
        {
            this.id_rol = id_rol;
            this.id_usuario = id_usuario;
        }
        public UsuarioRol() { }

        
    }
    
}