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
    public class C_cajasController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        // GET: C_cajas
        public ActionResult Index()
        {
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
            var c_cajas = db.C_cajas.ToList();



            return View(c_cajas);
        }

        // GET: C_cajas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_cajas c_cajas = db.C_cajas.Find(id);
            if (c_cajas == null)
            {
                return HttpNotFound();
            }
            return View(c_cajas);
        }

        // GET: C_cajas/Create
        public ActionResult Create()
        {
            ViewBag.id_sucursal = new SelectList(db.C_sucursales, "Id_sucursal", "nombre");
            return View();
        }

        // POST: C_cajas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "" +
            "caja,status,id_sucursal,nombre")] C_cajas c_cajas)
        {
            if (ModelState.IsValid)
            {
                db.C_cajas.Add(c_cajas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_sucursal = new SelectList(db.C_sucursales, "Id_sucursal", "codigo_sucursal", c_cajas.C_sucursales.Id_sucursal);
            return View(c_cajas);
        }

        // GET: C_cajas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_cajas c_cajas = db.C_cajas.Find(id);
            if (c_cajas == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_sucursal = new SelectList(db.C_sucursales, "Id_sucursal", "codigo_sucursal", c_cajas.C_sucursales.Id_sucursal);
            return View(c_cajas);
        }

        // POST: C_cajas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_caja,caja,status,id_sucursal")] C_cajas c_cajas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_cajas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_sucursal = new SelectList(db.C_sucursales, "Id_sucursal", "codigo_sucursal", c_cajas.C_sucursales.Id_sucursal);
            return View(c_cajas);
        }

        // GET: C_cajas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_cajas c_cajas = db.C_cajas.Find(id);
            if (c_cajas == null)
            {
                return HttpNotFound();
            }
            return View(c_cajas);
        }

        // POST: C_cajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_cajas c_cajas = db.C_cajas.Find(id);
            db.C_cajas.Remove(c_cajas);
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

 

