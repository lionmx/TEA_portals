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
    public class PERMISOS_ASIGNADOSController : Controller
    {
        private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();

        // GET: PERMISOS_ASIGNADOS
        public ActionResult Index()
        {
            var pERMISOS_ASIGNADOS = db.PERMISOS_ASIGNADOS.Include(p => p.MODULOS).Include(p => p.PERMISOS).Include(p => p.ROLES).Include(p => p.USUARIOS);
            return View(pERMISOS_ASIGNADOS.ToList());
        }

        // GET: PERMISOS_ASIGNADOS/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISOS_ASIGNADOS pERMISOS_ASIGNADOS = db.PERMISOS_ASIGNADOS.Find(id);
            if (pERMISOS_ASIGNADOS == null)
            {
                return HttpNotFound();
            }
            return View(pERMISOS_ASIGNADOS);
        }

        // GET: PERMISOS_ASIGNADOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_MODULO = new SelectList(db.MODULOS, "ID_MODULO", "NOMBRE");
            ViewBag.ID_PERMISO = new SelectList(db.PERMISOS, "ID_PERMISO", "DESCRIPCION");
            ViewBag.ID_ROL = new SelectList(db.ROLES, "ID_ROL", "ID_PERFIL");
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "NOMBRE");
            return View();
        }

        // POST: PERMISOS_ASIGNADOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_USUARIO,ID_ROL,ID_PERMISO,ID_MODULO,STATUS,ID_PERMISO_ASIGNADO")] PERMISOS_ASIGNADOS pERMISOS_ASIGNADOS)
        {
            if (ModelState.IsValid)
            {
                db.PERMISOS_ASIGNADOS.Add(pERMISOS_ASIGNADOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_MODULO = new SelectList(db.MODULOS, "ID_MODULO", "NOMBRE", pERMISOS_ASIGNADOS.ID_MODULO);
            ViewBag.ID_PERMISO = new SelectList(db.PERMISOS, "ID_PERMISO", "DESCRIPCION", pERMISOS_ASIGNADOS.ID_PERMISO);
            ViewBag.ID_ROL = new SelectList(db.ROLES, "ID_ROL", "ID_PERFIL", pERMISOS_ASIGNADOS.ID_ROL);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "NOMBRE", pERMISOS_ASIGNADOS.ID_USUARIO);
            return View(pERMISOS_ASIGNADOS);
        }

        // GET: PERMISOS_ASIGNADOS/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISOS_ASIGNADOS pERMISOS_ASIGNADOS = db.PERMISOS_ASIGNADOS.Find(id);
            if (pERMISOS_ASIGNADOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_MODULO = new SelectList(db.MODULOS, "ID_MODULO", "NOMBRE", pERMISOS_ASIGNADOS.ID_MODULO);
            ViewBag.ID_PERMISO = new SelectList(db.PERMISOS, "ID_PERMISO", "DESCRIPCION", pERMISOS_ASIGNADOS.ID_PERMISO);
            ViewBag.ID_ROL = new SelectList(db.ROLES, "ID_ROL", "ID_PERFIL", pERMISOS_ASIGNADOS.ID_ROL);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "NOMBRE", pERMISOS_ASIGNADOS.ID_USUARIO);
            return View(pERMISOS_ASIGNADOS);
        }

        // POST: PERMISOS_ASIGNADOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_USUARIO,ID_ROL,ID_PERMISO,ID_MODULO,STATUS,ID_PERMISO_ASIGNADO")] PERMISOS_ASIGNADOS pERMISOS_ASIGNADOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERMISOS_ASIGNADOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_MODULO = new SelectList(db.MODULOS, "ID_MODULO", "NOMBRE", pERMISOS_ASIGNADOS.ID_MODULO);
            ViewBag.ID_PERMISO = new SelectList(db.PERMISOS, "ID_PERMISO", "DESCRIPCION", pERMISOS_ASIGNADOS.ID_PERMISO);
            ViewBag.ID_ROL = new SelectList(db.ROLES, "ID_ROL", "ID_PERFIL", pERMISOS_ASIGNADOS.ID_ROL);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "NOMBRE", pERMISOS_ASIGNADOS.ID_USUARIO);
            return View(pERMISOS_ASIGNADOS);
        }

        // GET: PERMISOS_ASIGNADOS/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISOS_ASIGNADOS pERMISOS_ASIGNADOS = db.PERMISOS_ASIGNADOS.Find(id);
            if (pERMISOS_ASIGNADOS == null)
            {
                return HttpNotFound();
            }
            return View(pERMISOS_ASIGNADOS);
        }

        // POST: PERMISOS_ASIGNADOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PERMISOS_ASIGNADOS pERMISOS_ASIGNADOS = db.PERMISOS_ASIGNADOS.Find(id);
            db.PERMISOS_ASIGNADOS.Remove(pERMISOS_ASIGNADOS);
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
