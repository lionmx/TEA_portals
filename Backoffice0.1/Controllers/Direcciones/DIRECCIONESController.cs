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
    public class DIRECCIONESController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        // GET: DIRECCIONES
        public ActionResult Index()
        {
            var c_direcciones_mx = db.C_direcciones_mx.Include(c => c.C_colonias_mx);
            return View(c_direcciones_mx.ToList());
        }

        // GET: DIRECCIONES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_direcciones_mx c_direcciones_mx = db.C_direcciones_mx.Find(id);
            if (c_direcciones_mx == null)
            {
                return HttpNotFound();
            }
            return View(c_direcciones_mx);
        }

        // GET: DIRECCIONES/Create
        public ActionResult Create()
        {
            ViewBag.id_colonia = new SelectList(db.C_colonias_mx, "id", "d_codigo");
            return View();
        }

        // POST: DIRECCIONES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_direccion,calle,numero_ext,id_colonia,entre_calle1,entre_calle2,referencia,d_codigo,id_asenta_cpcons,id_ciudad,id_estado,id_colonia_mx")] C_direcciones_mx c_direcciones_mx)
        {
            if (ModelState.IsValid)
            {
                db.C_direcciones_mx.Add(c_direcciones_mx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_colonia = new SelectList(db.C_colonias_mx, "id", "d_codigo", c_direcciones_mx.id_colonia);
            return View(c_direcciones_mx);
        }

        // GET: DIRECCIONES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_direcciones_mx c_direcciones_mx = db.C_direcciones_mx.Find(id);
            if (c_direcciones_mx == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_colonia = new SelectList(db.C_colonias_mx, "id", "d_codigo", c_direcciones_mx.id_colonia);
            return View(c_direcciones_mx);
        }

        // POST: DIRECCIONES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_direccion,calle,numero_ext,id_colonia,entre_calle1,entre_calle2,referencia,d_codigo,id_asenta_cpcons,id_ciudad,id_estado,id_colonia_mx")] C_direcciones_mx c_direcciones_mx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_direcciones_mx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_colonia = new SelectList(db.C_colonias_mx, "id", "d_codigo", c_direcciones_mx.id_colonia);
            return View(c_direcciones_mx);
        }

        // GET: DIRECCIONES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_direcciones_mx c_direcciones_mx = db.C_direcciones_mx.Find(id);
            if (c_direcciones_mx == null)
            {
                return HttpNotFound();
            }
            return View(c_direcciones_mx);
        }

        // POST: DIRECCIONES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_direcciones_mx c_direcciones_mx = db.C_direcciones_mx.Find(id);
            db.C_direcciones_mx.Remove(c_direcciones_mx);
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
