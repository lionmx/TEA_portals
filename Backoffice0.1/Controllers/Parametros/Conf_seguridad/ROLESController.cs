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
        public ActionResult Index()
        {
            #region Viewbags 

            CS_permisos_asignados ViewModel = new CS_permisos_asignados();
            List<string> listaPA = new List<string>();
            string loggedId = Session["LoggedId"].ToString();
            for (int i = 00; i <= 8; i++)
            {
                var mod1 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "0" + i && a.ID_PERMISO == "07").FirstOrDefault();
                var perfil = db.CS_usuarios.Where(a => a.ID_USUARIO.Equals(loggedId)).FirstOrDefault();
                if (mod1 != null)
                {
                    listaPA.Add(mod1.ID_MODULO);
                    listaPA.Add(mod1.ID_PERMISO);
                   // ViewBag.perfil = perfil.ID_PERFIL;
                }
            }
            ViewBag.data = listaPA;
            List<string> listaPA2 = new List<string>();
            for (int i = 0; i <= 8; i++)
            {
                var mod2 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "'0" + i + "'" && a.ID_PERMISO.Equals("08")).FirstOrDefault();
                var perfil = db.CS_usuarios.Where(a => a.ID_USUARIO.Equals(loggedId)).FirstOrDefault();
                if (mod2 != null)
                {
                    listaPA2.Add(mod2.ID_MODULO);
                    listaPA2.Add(mod2.ID_PERMISO);

                }
            }
            ViewBag.data2 = listaPA2;
            #endregion
            //var rOLES = db.CS_roles.Include(r => r.CS_perfiles);
            return View(rOLES.ToList());
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
            ViewBag.ID_PERFIL = new SelectList(db.CS_perfiles, "ID_PERFIL", "DESCRIPCION");
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

            ViewBag.ID_PERFIL = new SelectList(db.CS_perfiles, "ID_PERFIL", "DESCRIPCION", rOLES.ID_PERFIL);
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
            ViewBag.ID_PERFIL = new SelectList(db.CS_perfiles, "ID_PERFIL", "DESCRIPCION", rOLES.ID_PERFIL);
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
            ViewBag.ID_PERFIL = new SelectList(db.CS_perfiles, "ID_PERFIL", "DESCRIPCION", rOLES.ID_PERFIL);
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
