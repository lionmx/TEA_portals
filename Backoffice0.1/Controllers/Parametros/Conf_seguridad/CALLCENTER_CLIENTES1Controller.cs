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
         DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
      
        // GET: CALLCENTER_CLIENTES1
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
                    ViewBag.perfil = perfil.ID_PERFIL;
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
