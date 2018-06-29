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
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        // GET: ROLES
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
            var rOLES = db.CS_roles.Include(r => r.C_servicios);
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
            ViewBag.ID_PERFIL = new SelectList(db.C_servicios, "id_servicio", "nombre_servicio", rOLES.C_servicios);
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
