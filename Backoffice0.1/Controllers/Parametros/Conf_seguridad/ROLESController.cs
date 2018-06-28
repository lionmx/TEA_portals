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
    public class ROLESController : Controller
    {
        //private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
        // GET: ROLES
        public void  Index()
        {
           
            /*
            var rOLES = db.CS_roles.Include(r => r.C_servicios);
            return View(rOLES.ToList());*/
        }

        // GET: ROLES/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CS_roles rOLES = db.CS_roles.Find(id);
            if (rOLES == null)
            {
                return HttpNotFound();
            }
            return View(rOLES);
        }

        // GET: ROLES/Create
        public ActionResult Create()
        {
            ViewBag.ID_PERFIL = new SelectList(db.C_servicios, "id_servicio", "nombre_servicio");
            return View();
        }

        // POST: ROLES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ROL,ID_PERFIL,DESCRIPCION")] CS_roles rOLES)
        {
            if (ModelState.IsValid)
            {
                db.CS_roles.Add(rOLES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PERFIL = new SelectList(db.C_servicios, "id_servicio", "nombre_servicio", rOLES.ID_SERVICIO);
            return View(rOLES);
        }

        // GET: ROLES/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CS_roles rOLES = db.CS_roles.Find(id);
            if (rOLES == null)
            {
                return HttpNotFound();
            }
           // ViewBag.ID_PERFIL = new SelectList(db.C_servicios, "id_servicio", "nombre_servicio", rOLES.C_servicios);
            return View(rOLES);
        }

        // POST: ROLES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ROL,ID_PERFIL,DESCRIPCION")] CS_roles rOLES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOLES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PERFIL = new SelectList(db.C_servicios, "id_servicio", "nombre_servicio", rOLES.ID_SERVICIO);
            return View(rOLES);
        }

        // GET: ROLES/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           CS_roles rOLES = db.CS_roles.Find(id);
            if (rOLES == null)
            {
                return HttpNotFound();
            }
            return View(rOLES);
        }

        // POST: ROLES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
           CS_roles rOLES = db.CS_roles.Find(id);
            db.CS_roles.Remove(rOLES);
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
