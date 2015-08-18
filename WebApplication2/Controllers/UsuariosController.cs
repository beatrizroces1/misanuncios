using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;



namespace WebApplication2.Controllers
{
    public class UsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
      
        /*public ActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registro(Usuario model)
        {

            try
            {
                db.Usuarios.Add(model);
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
            int id = model.ID;
            UsuarioRol usuarioRol = new UsuarioRol(2, id);
            try
            {
                db.UsuarioRols.Add(usuarioRol);
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
        }*/

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,nombre,apellido1,apellido2,ciudad,email,password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Usuarios.Add(usuario);
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

            return View(usuario);
        }

        /*public ActionResult InicioSesion(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult InicioSesion(UsuarioInicioSesion model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //  ModelState.AddModelError("", "Invalid login attempt.");
            // return View(model);                  
            return RedirectToLocal(returnUrl);
        }
        */

     
        public ActionResult Edit()
        {
            string email = User.Identity.GetUserName();
            Usuario usuario = db.Usuarios.Single(u => u.email.Equals(email));
            
            if (usuario == null)
            {
                return HttpNotFound();
            }
          
            return View(usuario);
        }
        public ActionResult EditCiudadPrimero()
        {
            string email = User.Identity.GetUserName();
            Usuario usuario = db.Usuarios.Single(u => u.email.Equals(email));

            if (usuario == null)
            {
                return HttpNotFound();
            }
            string nombreCiudad = usuario.ciudad;
            Ciudad ciudad = db.Ciudads.Single(u => u.nombre.Equals(nombreCiudad));

            FillDropDownListCiudad(ciudad.ID);
            return View(usuario);
        }

        [HttpGet]
        public ActionResult EditCiudad(string ciudades)
        {
            int ciu = Convert.ToInt32(ciudades);
            Ciudad ciudad = db.Ciudads.Single(c => c.ID.Equals(ciu));

          

            string email = User.Identity.GetUserName();
            Usuario usuario = db.Usuarios.Single(u => u.email.Equals(email));

            if (ciudad.nombre != usuario.ciudad)
            {
                var anuncios = from a in db.Anuncios where a.id_usuario == usuario.ID select a;

                int num = anuncios.Count();
                if (num > 0)
                {

                    return View("CambioCiudad");


                }
                else
                {

                    return cambiarCiudadUsuario(ciudad.nombre,usuario);
                }

            }
            else
            {
                return View("CambioCiudadNinguno");

            }
        }
        public ActionResult cambiarCiudadUsuario(string town, Usuario usuario)
        {
            usuario.ciudad = town;
            try
            {
                db.Entry(usuario).State = EntityState.Modified;
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

            return RedirectToAction("Index", "Manage");
        }

        public void FillDropDownListCiudad(int? ciudadPrimera = 0)
        {
            
            List<SelectListItem> ciudades = new List<SelectListItem>();
            //ciudades.Add(new SelectListItem() { Value = "0", Text = "-", Selected = false });

            foreach (var ciudad in db.Ciudads)
                ciudades.Add(new SelectListItem()
                {
                    Value = ciudad.ID.ToString(),
                    Text = ciudad.nombre.ToString(),
                    Selected = ciudadPrimera == ciudad.ID ? true : false
                });
            ViewData["ciudades"] = ciudades;

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit([Bind(Include = "ID,nombre,apellido1,apellido2,ciudad,email, password")] Usuario usuario)
        {

            if (ModelState.IsValid)
            {




                try
                {
                    db.Entry(usuario).State = EntityState.Modified;
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
                

                return RedirectToAction("Index", "Manage");
            }

            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = new Usuario();
            try
            {
                 usuario = db.Usuarios.Find(id);
            }
            catch (Exception)
            {

               
            }
            try
            {
                db.Usuarios.Remove(usuario);
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
