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
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       

        // GET: Categorias
        public ActionResult Index()
        {
            return View(db.Categorias.ToList());
        }

       

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = new Categoria();
          
            try
            {
                 categoria = db.Categorias.Find(id);
               
            }
            catch (Exception)
            {               
               
            }
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,descripcion")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    try
                    {
                        db.Categorias.Add(categoria);
                    }
                    catch (Exception)
                    {
                        
                    }
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

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,descripcion")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(categoria).State = EntityState.Modified;
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
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categorias.Single(u => u.ID.Equals(id));

            var anuncios = from a in db.Anuncios where a.categoria == categoria.descripcion select a;
            
            int num = anuncios.Count();
            if (num > 0)
            {

                return View("EliminarCategoria");


            }
            else
            {

                return eliminarCategoria(categoria);
            }



           
        }

        public ActionResult eliminarCategoria(Categoria categoria)
        {

            int id = categoria.ID;
            try
            {
                categoria = db.Categorias.Find(id);
            }
            catch (Exception)
            {

            }
            try
            {
                db.Categorias.Remove(categoria);
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
