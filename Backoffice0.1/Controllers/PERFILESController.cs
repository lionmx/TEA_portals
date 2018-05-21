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
    public class PERFILESController : Controller
    {
        private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();

        // GET: PERFILES
        public ActionResult Index()
        {
            return View(db.PERFILES.ToList());
        }

        // GET: PERFILES/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERFILES pERFILES = db.PERFILES.Find(id);
            if (pERFILES == null)
            {
                return HttpNotFound();
            }
            return View(pERFILES);
        }

        // GET: PERFILES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PERFILES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PERFIL,DESCRIPCION")] PERFILES pERFILES)
        {
            if (ModelState.IsValid)
            {
                db.PERFILES.Add(pERFILES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pERFILES);
        }

        // GET: PERFILES/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERFILES pERFILES = db.PERFILES.Find(id);
            if (pERFILES == null)
            {
                return HttpNotFound();
            }
            return View(pERFILES);
        }

        // POST: PERFILES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PERFIL,DESCRIPCION")] PERFILES pERFILES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERFILES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pERFILES);
        }

        // GET: PERFILES/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERFILES pERFILES = db.PERFILES.Find(id);
            if (pERFILES == null)
            {
                return HttpNotFound();
            }
            return View(pERFILES);
        }

        // POST: PERFILES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PERFILES pERFILES = db.PERFILES.Find(id);
            db.PERFILES.Remove(pERFILES);
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
