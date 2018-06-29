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
    public class CALLCENTER_CLIENTES1Controller : Controller
    {
        //private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();
         DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
      
        // GET: CALLCENTER_CLIENTES1
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
            return View(db.M_callcenter_clientes.ToList());
        }

        // GET: CALLCENTER_CLIENTES1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_callcenter_clientes cALLCENTER_CLIENTES1 = db.M_callcenter_clientes.Find(id);
            if (cALLCENTER_CLIENTES1 == null)
            {
                return HttpNotFound();
            }
            return View(cALLCENTER_CLIENTES1);
        }

        // GET: CALLCENTER_CLIENTES1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CALLCENTER_CLIENTES1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CLIENTE,TELEFONO,NOMBRE,CALLE,ESPECIFICACION,COLONIA_ID,REFERENCIA,ADICIONALES,APP_UBICACION,APP_PWD,APP_GCM,ULTIMA_COMPRA")] M_callcenter_clientes cALLCENTER_CLIENTES1)
        {
            if (ModelState.IsValid)
            {
                db.M_callcenter_clientes.Add(cALLCENTER_CLIENTES1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cALLCENTER_CLIENTES1);
        }

        // GET: CALLCENTER_CLIENTES1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            M_callcenter_clientes cALLCENTER_CLIENTES1 = db.M_callcenter_clientes.Find(id);
            if (cALLCENTER_CLIENTES1 == null)
            {
                return HttpNotFound();
            }
            return View(cALLCENTER_CLIENTES1);
        }

        // POST: CALLCENTER_CLIENTES1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CargarCalles()
        {
            return View(db.M_callcenter_clientes.ToList());
        }
        public ActionResult Edit([Bind(Include = "ID_CLIENTE,TELEFONO,NOMBRE,CALLE,ESPECIFICACION,COLONIA_ID,REFERENCIA,ADICIONALES,APP_UBICACION,APP_PWD,APP_GCM,ULTIMA_COMPRA")] M_callcenter_clientes cALLCENTER_CLIENTES1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cALLCENTER_CLIENTES1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cALLCENTER_CLIENTES1);
        }

        // GET: CALLCENTER_CLIENTES1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           M_callcenter_clientes cALLCENTER_CLIENTES1 = db.M_callcenter_clientes.Find(id);
            if (cALLCENTER_CLIENTES1 == null)
            {
                return HttpNotFound();
            }
            return View(cALLCENTER_CLIENTES1);
        }

        // POST: CALLCENTER_CLIENTES1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            M_callcenter_clientes cALLCENTER_CLIENTES1 = db.M_callcenter_clientes.Find(id);
            db.M_callcenter_clientes.Remove(cALLCENTER_CLIENTES1);
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
