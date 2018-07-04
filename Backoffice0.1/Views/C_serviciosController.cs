using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backoffice0._1.Models;

namespace Backoffice0._1.Views
{
    public class C_serviciosController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        // GET: C_servicios
        public ActionResult Index()
        {
            return View(db.C_servicios.ToList());
        }

        // GET: C_servicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_servicios c_servicios = db.C_servicios.Find(id);
            if (c_servicios == null)
            {
                return HttpNotFound();
            }
            return View(c_servicios);
        }

        // GET: C_servicios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: C_servicios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_servicio,nombre_servicio,alias_servicio,descripcion_servicio,fecha_registro,activo")] C_servicios c_servicios)
        {
            if (ModelState.IsValid)
            {
                db.C_servicios.Add(c_servicios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_servicios);
        }

        // GET: C_servicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_servicios c_servicios = db.C_servicios.Find(id);
            if (c_servicios == null)
            {
                return HttpNotFound();
            }
            return View(c_servicios);
        }

        // POST: C_servicios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_servicio,nombre_servicio,alias_servicio,descripcion_servicio,fecha_registro,activo")] C_servicios c_servicios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_servicios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_servicios);
        }

        // GET: C_servicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_servicios c_servicios = db.C_servicios.Find(id);
            if (c_servicios == null)
            {
                return HttpNotFound();
            }
            return View(c_servicios);
        }

        // POST: C_servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_servicios c_servicios = db.C_servicios.Find(id);
            db.C_servicios.Remove(c_servicios);
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
