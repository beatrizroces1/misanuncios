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
    public class UsuarioRolsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UsuarioRols
        public ActionResult Index()
        {
            return View(db.UsuarioRols.ToList());
        }

        // GET: UsuarioRols/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioRol usuarioRol = db.UsuarioRols.Find(id);
            if (usuarioRol == null)
            {
                return HttpNotFound();
            }
            return View(usuarioRol);
        }

        // GET: UsuarioRols/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioRols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,id_rol,id_usuario")] UsuarioRol usuarioRol)
        {
            if (ModelState.IsValid)
            {
                db.UsuarioRols.Add(usuarioRol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarioRol);
        }

        // GET: UsuarioRols/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioRol usuarioRol = db.UsuarioRols.Find(id);
            if (usuarioRol == null)
            {
                return HttpNotFound();
            }
            return View(usuarioRol);
        }

        // POST: UsuarioRols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,id_rol,id_usuario")] UsuarioRol usuarioRol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioRol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarioRol);
        }

        // GET: UsuarioRols/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioRol usuarioRol = db.UsuarioRols.Find(id);
            if (usuarioRol == null)
            {
                return HttpNotFound();
            }
            return View(usuarioRol);
        }

        // POST: UsuarioRols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioRol usuarioRol = db.UsuarioRols.Find(id);
            db.UsuarioRols.Remove(usuarioRol);
            db.SaveChanges();
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
