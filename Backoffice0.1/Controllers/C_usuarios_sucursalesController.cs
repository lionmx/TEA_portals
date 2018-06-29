using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backoffice0._1.Models;

namespace Backoffice0._1.Controllers
{
    public class C_usuarios_sucursalesController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        // GET: C_usuarios_sucursales
        public ActionResult Index()
        {
            var c_usuarios_sucursales = db.C_usuarios_sucursales.Include(c => c.CS_usuarios);
            return View(c_usuarios_sucursales.ToList());
        }

        // GET: C_usuarios_sucursales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_usuarios_sucursales c_usuarios_sucursales = db.C_usuarios_sucursales.Find(id);
            if (c_usuarios_sucursales == null)
            {
                return HttpNotFound();
            }
            return View(c_usuarios_sucursales);
        }

        // GET: C_usuarios_sucursales/Create
        public ActionResult Create()
        {
            ViewBag.id_usuario = new SelectList(db.CS_usuarios, "ID_USUARIO", "CODIGO_USUARIO");
            return View();
        }

        // POST: C_usuarios_sucursales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_usuarios_sucursales,id_sucursal,id_usuario")] C_usuarios_sucursales c_usuarios_sucursales)
        {
            if (ModelState.IsValid)
            {
                db.C_usuarios_sucursales.Add(c_usuarios_sucursales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_usuario = new SelectList(db.CS_usuarios, "ID_USUARIO", "CODIGO_USUARIO", c_usuarios_sucursales.id_usuarios_sucursales);
            return View(c_usuarios_sucursales);
        }

        // GET: C_usuarios_sucursales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_usuarios_sucursales c_usuarios_sucursales = db.C_usuarios_sucursales.Find(id);
            if (c_usuarios_sucursales == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_usuario = new SelectList(db.CS_usuarios, "ID_USUARIO", "CODIGO_USUARIO", c_usuarios_sucursales.id_usuarios_sucursales);
            return View(c_usuarios_sucursales);
        }

        // POST: C_usuarios_sucursales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_usuarios_sucursales,id_sucursal,id_usuario")] C_usuarios_sucursales c_usuarios_sucursales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_usuarios_sucursales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_usuario = new SelectList(db.CS_usuarios, "ID_USUARIO", "CODIGO_USUARIO", c_usuarios_sucursales.id_usuarios_sucursales);
            return View(c_usuarios_sucursales);
        }

        // GET: C_usuarios_sucursales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_usuarios_sucursales c_usuarios_sucursales = db.C_usuarios_sucursales.Find(id);
            if (c_usuarios_sucursales == null)
            {
                return HttpNotFound();
            }
            return View(c_usuarios_sucursales);
        }

        // POST: C_usuarios_sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_usuarios_sucursales c_usuarios_sucursales = db.C_usuarios_sucursales.Find(id);
            db.C_usuarios_sucursales.Remove(c_usuarios_sucursales);
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
