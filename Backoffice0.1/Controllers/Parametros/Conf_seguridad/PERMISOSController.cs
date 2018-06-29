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
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        // GET: PERMISOS
        public ActionResult Index()
        {
            #region Viewbags 

            //obtiene los permisos de cada servicio/modulo para el usuario loggeado           
            List<int> permisosLista = new List<int>();
            int loggedId = Convert.ToInt32(Session["LoggedId"]);
            var permisosServicioModulo = db.Database.SqlQuery<permisosServicioModulo>("SELECT b.id_servicio as id_servicio, b.id_modulo as id_modulo, a.id_permiso as id_permiso from CS_PERMISOS_ASIGNADOS a JOIN C_SERVICIOS_MODULOS b on a.id_servicios_modulos = b.id_servicios_modulos WHERE a.ID_USUARIO = '" + Session["LoggedId"] + "'");

            if (permisosServicioModulo != null)
            {
                foreach (var n in permisosServicioModulo)
                {
                    permisosLista.Add(n.id_servicio);
                    permisosLista.Add(n.id_modulo);
                    permisosLista.Add(Convert.ToInt32(n.id_permiso));
                }
            }

            //get perfil (servicio) de usuario loggeado y guardarlo en ViewBag
            var perfil = db.CS_usuarios.Where(a => a.ID_USUARIO.Equals(loggedId)).FirstOrDefault();
            if (perfil != null)
            {
                ViewBag.idServicio = perfil.ID_SERVICIO;
            }
            ViewBag.permisos = permisosLista;
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
