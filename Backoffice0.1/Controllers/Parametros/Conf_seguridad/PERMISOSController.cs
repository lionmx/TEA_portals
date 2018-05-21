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
        //private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
        // GET: PERMISOS
        public ActionResult Index()
        {
            #region Viewbags 

            CS_permisos_asignados ViewModel = new CS_permisos_asignados();
            List<string> listaPA = new List<string>();
            string user = Session["LoggedUser"].ToString();
            var id = from us in db.CS_usuarios
                     where us.NOMBRE.Equals(user)
                     select us;

            foreach (var i in id)
            {
                user = i.ID_USUARIO;
            }
            for (int i = 00; i <= 8; i++)
            {
                var mod1 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(user) && a.ID_MODULO == "0" + i && a.ID_PERMISO == "07").FirstOrDefault();
                if (mod1 != null)
                {
                    listaPA.Add(mod1.ID_MODULO);
                    listaPA.Add(mod1.ID_PERMISO);

                }
            }
            ViewBag.data = listaPA;
            List<string> listaPA2 = new List<string>();
            for (int i = 0; i <= 8; i++)
            {
                var mod2 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(user) && a.ID_MODULO == "'0" + i + "'" && a.ID_PERMISO.Equals("08")).FirstOrDefault();
                if (mod2 != null)
                {
                    listaPA2.Add(mod2.ID_MODULO);
                    listaPA2.Add(mod2.ID_PERMISO);
                }
            }
            ViewBag.data2 = listaPA2;

            #endregion
            return View(db.CS_permisos.ToList());
        }

        // GET: PERMISOS/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           CS_permisos pERMISOS = db.CS_permisos.Find(id);
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
        public ActionResult Create([Bind(Include = "ID_PERMISO,DESCRIPCION")] CS_permisos pERMISOS)
        {
            if (ModelState.IsValid)
            {
                db.CS_permisos.Add(pERMISOS);
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
            CS_permisos pERMISOS = db.CS_permisos.Find(id);
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
        public ActionResult Edit([Bind(Include = "ID_PERMISO,DESCRIPCION")] CS_permisos pERMISOS)
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
            CS_permisos pERMISOS = db.CS_permisos.Find(id);
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
            CS_permisos pERMISOS = db.CS_permisos.Find(id);
            db.CS_permisos.Remove(pERMISOS);
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
