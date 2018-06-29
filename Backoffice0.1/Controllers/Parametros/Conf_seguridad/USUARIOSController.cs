using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backoffice0._1.Models;
using Backoffice0._1.Helper;


namespace Backoffice0._1.Controllers
{
    public class USUARIOSController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        //private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        // GET: USUARIOS
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

            var uSUARIOS = db.CS_usuarios.Include(u => u.C_Servicios).Include(u => u.CS_roles);
            return View(uSUARIOS.ToList());
        }

        // GET: USUARIOS/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CS_usuarios uSUARIOS = db.CS_usuarios.Find(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View("Details222", uSUARIOS);
        }

        // GET: USUARIOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_PERFIL = new SelectList(db.C_servicios, "id_servicio", "nombre_servicio");
            ViewBag.ID_ROL = new SelectList(db.CS_roles, "ID_ROL", "DESCRIPCION");
            return View();
        }

        // POST: USUARIOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_USUARIO,NOMBRE,REF,PASS,ID_PERFIL,TARJETA_EMPLEADO,ESTADO_ACTUAL,GAFETE_IDENTIFICACION,ID_ROL")] CS_usuarios uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                var password = PasswordHelper.EncodePassword(uSUARIOS.PASS, "MySalt");
                uSUARIOS.PASS = password;
                db.CS_usuarios.Add(uSUARIOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PERFIL = new SelectList(db.CS_usuarios, "id_Servicio", "nombre_servicio", uSUARIOS.ID_SERVICIO);
            ViewBag.ID_ROL = new SelectList(db.CS_roles, "ID_ROL", "DESCRIPCION", uSUARIOS.ID_ROL);
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CS_usuarios uSUARIOS = db.CS_usuarios.Find(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PERFIL = new SelectList(db.C_servicios, "id_servicio", "nombre_servicio", uSUARIOS.ID_SERVICIO);
            ViewBag.ID_ROL = new SelectList(db.CS_roles, "ID_ROL", "DESCRIPCION", uSUARIOS.ID_ROL);
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_USUARIO,NOMBRE,REF,PASS,ID_PERFIL,TARJETA_EMPLEADO,ESTADO_ACTUAL,GAFETE_IDENTIFICACION,ID_ROL")] CS_usuarios uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PERFIL = new SelectList(db.C_servicios, "id_servicio", "nombre_servicio", uSUARIOS.C_Servicios);
            ViewBag.ID_ROL = new SelectList(db.CS_roles, "ID_ROL", "DESCRIPCION", uSUARIOS.ID_ROL);
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           CS_usuarios uSUARIOS = db.CS_usuarios.Find(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
           CS_usuarios uSUARIOS = db.CS_usuarios.Find(id);
            db.CS_usuarios.Remove(uSUARIOS);
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
