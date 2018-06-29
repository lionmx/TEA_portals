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
    public class C_PRODUCTO_CLASIFICACIONController : Controller
    {
        
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        // GET: C_PRODUCTO_CLASIFICACION
        public ActionResult Index()
        {
            return View(db.C_producto_clasificacion.ToList());
        }

        // GET: C_PRODUCTO_CLASIFICACION/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_producto_clasificacion c_producto_clasificacion = db.C_producto_clasificacion.Find(id);
            if (c_producto_clasificacion == null)
            {
                return HttpNotFound();
            }
            return View(c_producto_clasificacion);
        }

        // GET: C_PRODUCTO_CLASIFICACION/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: C_PRODUCTO_CLASIFICACION/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_producto_clasificacion,nombre_producto_clasificacion")] C_producto_clasificacion c_producto_clasificacion)
        {
            if (ModelState.IsValid)
            {
                db.C_producto_clasificacion.Add(c_producto_clasificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_producto_clasificacion);
        }

        // GET: C_PRODUCTO_CLASIFICACION/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_producto_clasificacion c_producto_clasificacion = db.C_producto_clasificacion.Find(id);
            if (c_producto_clasificacion == null)
            {
                return HttpNotFound();
            }
            return View(c_producto_clasificacion);
        }

        // POST: C_PRODUCTO_CLASIFICACION/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_producto_clasificacion,nombre_producto_clasificacion")] C_producto_clasificacion c_producto_clasificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_producto_clasificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_producto_clasificacion);
        }

        // GET: C_PRODUCTO_CLASIFICACION/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_producto_clasificacion c_producto_clasificacion = db.C_producto_clasificacion.Find(id);
            if (c_producto_clasificacion == null)
            {
                return HttpNotFound();
            }
            return View(c_producto_clasificacion);
        }

        // POST: C_PRODUCTO_CLASIFICACION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            C_producto_clasificacion c_producto_clasificacion = db.C_producto_clasificacion.Find(id);
            db.C_producto_clasificacion.Remove(c_producto_clasificacion);
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
