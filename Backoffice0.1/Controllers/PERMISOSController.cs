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
    public class PERMISOSController : Controller
    {
        private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();

        // GET: PERMISOS
        public ActionResult Index()
        {
            return View(db.PERMISOS.ToList());
        }

        // GET: PERMISOS/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISOS pERMISOS = db.PERMISOS.Find(id);
            if (pERMISOS == null)
            {
                return HttpNotFound();
            }
            return View(pERMISOS);
        }

        // GET: PERMISOS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PERMISOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PERMISO,DESCRIPCION")] PERMISOS pERMISOS)
        {
            if (ModelState.IsValid)
            {
                db.PERMISOS.Add(pERMISOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pERMISOS);
        }

        // GET: PERMISOS/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISOS pERMISOS = db.PERMISOS.Find(id);
            if (pERMISOS == null)
            {
                return HttpNotFound();
            }
            return View(pERMISOS);
        }

        // POST: PERMISOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PERMISO,DESCRIPCION")] PERMISOS pERMISOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERMISOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pERMISOS);
        }

        // GET: PERMISOS/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISOS pERMISOS = db.PERMISOS.Find(id);
            if (pERMISOS == null)
            {
                return HttpNotFound();
            }
            return View(pERMISOS);
        }

        // POST: PERMISOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PERMISOS pERMISOS = db.PERMISOS.Find(id);
            db.PERMISOS.Remove(pERMISOS);
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
