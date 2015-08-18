using ApiRest.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    public class Anuncio
    {
        public int ID { get; set; }
        public int id_usuario { get; set; }
        public string categoria { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaPublicacion { get; set;  }
    }
  
}