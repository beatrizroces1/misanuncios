using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AnunciosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Anuncios
        public ActionResult Index()
        {
          
            string nombreUsuario = User.Identity.GetUserName();
            
                Usuario usuario = db.Usuarios.Single(c => c.email.Equals(nombreUsuario));
                int idUsuario = usuario.ID;

                List<Anuncio> anuncios = new List<Anuncio>(from a in db.Anuncios where a.id_usuario == idUsuario select a);
                return View(anuncios);
            
             
        }



        // GET: Anuncios/Create
        //public ActionResult Buscar()
        //{
        //    var categorias = new List<string>();

        //    var categoria = from c in db.Categorias
        //                    orderby c.descripcion
        //                    select c.descripcion;

        //    categorias.AddRange(categoria.Distinct());
        //    ViewBag.categorias = new SelectList(categorias);

        //     var ciudades = new List<string>();

        //      var ciudad = from c in db.Ciudads
        //                    orderby c.nombre
        //                    select c.nombre;

        //    ciudades.AddRange(ciudad.Distinct());
        //    ViewBag.ciudades = new SelectList(ciudades);
            
        //    return View(db.Anuncios.ToList());
        //}



        // GET: Anuncios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }
        // GET: Anuncios/Details/5
        public ActionResult DetalleAnuncio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            int idUs = Convert.ToInt32(anuncio.id_usuario);
            Usuario usuario = db.Usuarios.Single(c => c.ID.Equals(idUs));
            ViewBag.Usuario = usuario.email;
            return View(anuncio);
        }

        // GET: Anuncios/Create
        public ActionResult Create()
        {
            FillDropDownList();
            return View();
        }


        public void FillDropDownList(int? categoriaPrimera = 0)
        {

            List<SelectListItem> categorias = new List<SelectListItem>();
        //  categorias.Add(new SelectListItem() { Value = "0", Text = "-", Selected = false });

            foreach (var categoria in db.Categorias)
                categorias.Add(new SelectListItem()
                {
                    Value = categoria.ID.ToString(),
                    Text = categoria.descripcion.ToString(),
                    Selected = categoriaPrimera == categoria.ID ? true : false
                });
            ViewData["categorias"] = categorias;
        }


        [HttpGet]      
        public ActionResult Buscar (string categorias, string ciudades)
        {
            var categoriasCombo = new List<string>();

            var categoriaCombo = from c in db.Categorias
                            orderby c.descripcion
                            select c.descripcion;

            categoriasCombo.AddRange(categoriaCombo.Distinct());
            ViewBag.categorias = new SelectList(categoriasCombo);

            var ciudadesCombo = new List<string>();

            var ciudadCombo = from c in db.Ciudads
                         orderby c.nombre
                         select c.nombre;

            ciudadesCombo.AddRange(ciudadCombo.Distinct());
            ViewBag.ciudades = new SelectList(ciudadesCombo);


            var anuncios = from m in db.Anuncios
                         select m;

            if (!String.IsNullOrEmpty(categorias) || !String.IsNullOrEmpty(ciudades))
            {


                if (!String.IsNullOrEmpty(categorias))
                {
                    string categoria = categorias;
                    if (!String.IsNullOrEmpty(ciudades))
                    {
                        string ciudad = ciudades;                       
                        anuncios = anuncios.Where(a => a.ciudad == ciudad && a.categoria == categoria);
                       
                    }
                    else
                    {
                      //  List<Anuncio> anuncios = new List<Anuncio>(from a in db.Anuncios where a.categoria == categoria select a);
                        anuncios = anuncios.Where(a => a.categoria == categoria);
                    }


                }
                else
                {
                    if (!String.IsNullOrEmpty(ciudades))
                    {
                        string ciudad = ciudades;
                       // List<Anuncio> anuncios = new List<Anuncio>(from a in db.Anuncios where a.ciudad == ciudad select a);
                        anuncios = anuncios.Where(a => a.ciudad == ciudad);

                    }
                  
                }
            }         
            
               

                return View(anuncios);

            



               
        }



    
       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,id_usuario,categoria,titulo,descripcion,fechaPublicacion")] Anuncio anuncio)
        {
            anuncio.fechaPublicacion = DateTime.Now.Date;
            string nombreUsuario = User.Identity.GetUserName();

            int cat = Convert.ToInt32(anuncio.categoria);
            Categoria categoria= db.Categorias.Single(c => c.ID.Equals(cat));         

            anuncio.categoria = categoria.descripcion;

            Usuario usuario = db.Usuarios.Single(c => c.email.Equals(nombreUsuario));
            int idUsuario = usuario.ID;
            anuncio.id_usuario = idUsuario;

            anuncio.ciudad = usuario.ciudad;

            if (ModelState.IsValid)
            {
                try
                {
                    db.Anuncios.Add(anuncio);
                }
                catch (Exception)
                {

                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return RedirectToAction("Index");
            }

            return View(anuncio);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            String catego = anuncio.categoria;
            Categoria cat = db.Categorias.Single(u => u.descripcion.Equals(catego));
       
            FillDropDownList(cat.ID);
            return View(anuncio);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,id_usuario,categoria,titulo,descripcion,fechaPublicacion")] Anuncio anuncio)
        {
           

            if (ModelState.IsValid)
            {
               // int cat = Convert.ToInt32(anuncio.categoria);
                //Categoria categoria = db.Categorias.Single(c => c.ID.Equals(cat));
                //anuncio.categoria = categoria.descripcion;
                try
                {
                    db.Entry(anuncio).State = EntityState.Modified;
                   
                }
                catch (Exception)
                {
                throw;
                 }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    
                    throw;
                }

                return RedirectToAction("Index");
            }
            return View(anuncio);
        }

        // GET: Anuncios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // POST: Anuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anuncio anuncio = new Anuncio();
            try
            {
                 anuncio = db.Anuncios.Find(id);
            }
            catch (Exception)
            {
                
            }
            try
            {
                db.Anuncios.Remove(anuncio);
            }
            catch (Exception)
            {

                
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
