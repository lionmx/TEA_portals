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
        //private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
        // GET: PERMISOS_ASIGNADOS
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
            var pERMISOS_ASIGNADOS = db.CS_permisos_asignados.Include(p => p.CS_modulos).Include(p => p.CS_permisos).Include(p => p.CS_roles).Include(p => p.CS_usuarios);
            return View(pERMISOS_ASIGNADOS.ToList());
        }

        // GET: PERMISOS_ASIGNADOS/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CS_permisos_asignados pERMISOS_ASIGNADOS = db.CS_permisos_asignados.Find(id);
            if (pERMISOS_ASIGNADOS == null)
            {
                return HttpNotFound();
            }
            return View(pERMISOS_ASIGNADOS);
        }

        // GET: PERMISOS_ASIGNADOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_MODULO = new SelectList(db.CS_modulos, "ID_MODULO", "NOMBRE");
            ViewBag.ID_PERMISO = new SelectList(db.CS_permisos, "ID_PERMISO", "DESCRIPCION");
            ViewBag.ID_ROL = new SelectList(db.CS_roles, "ID_ROL", "ID_PERFIL");
            ViewBag.ID_USUARIO = new SelectList(db.CS_usuarios, "ID_USUARIO", "NOMBRE");
            return View();
        }

        // POST: PERMISOS_ASIGNADOS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_USUARIO,ID_ROL,ID_PERMISO,ID_MODULO,STATUS,ID_PERMISO_ASIGNADO")] CS_permisos_asignados pERMISOS_ASIGNADOS)
        {
            if (ModelState.IsValid)
            {
                db.CS_permisos_asignados.Add(pERMISOS_ASIGNADOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_MODULO = new SelectList(db.CS_modulos, "ID_MODULO", "NOMBRE", pERMISOS_ASIGNADOS.ID_MODULO);
            ViewBag.ID_PERMISO = new SelectList(db.CS_permisos, "ID_PERMISO", "DESCRIPCION", pERMISOS_ASIGNADOS.ID_PERMISO);
            ViewBag.ID_ROL = new SelectList(db.CS_roles, "ID_ROL", "ID_PERFIL", pERMISOS_ASIGNADOS.ID_ROL);
            ViewBag.ID_USUARIO = new SelectList(db.CS_usuarios, "ID_USUARIO", "NOMBRE", pERMISOS_ASIGNADOS.ID_USUARIO);
            return View(pERMISOS_ASIGNADOS);
        }

        // GET: PERMISOS_ASIGNADOS/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CS_permisos_asignados pERMISOS_ASIGNADOS = db.CS_permisos_asignados.Find(id);
            if (pERMISOS_ASIGNADOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_MODULO = new SelectList(db.CS_modulos, "ID_MODULO", "NOMBRE", pERMISOS_ASIGNADOS.ID_MODULO);
            ViewBag.ID_PERMISO = new SelectList(db.CS_permisos, "ID_PERMISO", "DESCRIPCION", pERMISOS_ASIGNADOS.ID_PERMISO);
            ViewBag.ID_ROL = new SelectList(db.CS_roles, "ID_ROL", "ID_PERFIL", pERMISOS_ASIGNADOS.ID_ROL);
            ViewBag.ID_USUARIO = new SelectList(db.CS_usuarios, "ID_USUARIO", "NOMBRE", pERMISOS_ASIGNADOS.ID_USUARIO);
            return View(pERMISOS_ASIGNADOS);
        }

        // POST: PERMISOS_ASIGNADOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_USUARIO,ID_ROL,ID_PERMISO,ID_MODULO,STATUS,ID_PERMISO_ASIGNADO")] CS_permisos_asignados pERMISOS_ASIGNADOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERMISOS_ASIGNADOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_MODULO = new SelectList(db.CS_modulos, "ID_MODULO", "NOMBRE", pERMISOS_ASIGNADOS.ID_MODULO);
            ViewBag.ID_PERMISO = new SelectList(db.CS_permisos, "ID_PERMISO", "DESCRIPCION", pERMISOS_ASIGNADOS.ID_PERMISO);
            ViewBag.ID_ROL = new SelectList(db.CS_roles, "ID_ROL", "ID_PERFIL", pERMISOS_ASIGNADOS.ID_ROL);
            ViewBag.ID_USUARIO = new SelectList(db.CS_usuarios, "ID_USUARIO", "NOMBRE", pERMISOS_ASIGNADOS.ID_USUARIO);
            return View(pERMISOS_ASIGNADOS);
        }

        // GET: PERMISOS_ASIGNADOS/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CS_permisos_asignados pERMISOS_ASIGNADOS = db.CS_permisos_asignados.Find(id);
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
            CS_permisos_asignados pERMISOS_ASIGNADOS = db.CS_permisos_asignados.Find(id);
            db.CS_permisos_asignados.Remove(pERMISOS_ASIGNADOS);
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
