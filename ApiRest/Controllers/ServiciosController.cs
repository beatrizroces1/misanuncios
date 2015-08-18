using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiRest.Models;


namespace ApiRest.Controllers
{
    public class ServiciosController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Anuncio> Get(string ciudad)
        {

            ciudad = ciudad.Trim('/', '"');
            try
            {
               // Ciudad ciudadaux = db.Ciudads.Single(c => c.nombre.Equals(ciudad));
              //  String idciudad = ciudadaux.ID.ToString();
                List<Anuncio> anuncios = new List<Anuncio>(from a in db.Anuncios join u in db.Usuarios on a.id_usuario equals u.ID where u.ciudad == ciudad select a);
               return anuncios;
            }
            catch (Exception)
            {

                throw;
            }


          
            
            
        }
    }
}
