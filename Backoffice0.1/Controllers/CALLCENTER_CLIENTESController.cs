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
    public class CALLCENTER_CLIENTESController : Controller
    {
        private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();

        // GET: CALLCENTER_CLIENTES
        public ActionResult Index()
        {
            return View(db.CALLCENTER_CLIENTES1Set.ToList());
        }

        // GET: CALLCENTER_CLIENTES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CALLCENTER_CLIENTES1 cALLCENTER_CLIENTES1 = db.CALLCENTER_CLIENTES1Set.Find(id);
            if (cALLCENTER_CLIENTES1 == null)
            {
                return HttpNotFound();
            }
            return View(cALLCENTER_CLIENTES1);
        }

        // GET: CALLCENTER_CLIENTES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CALLCENTER_CLIENTES/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CLIENTE,TELEFONO,NOMBRE,CALLE,ESPECIFICACION,COLONIA_ID,REFERENCIA,ADICIONALES,APP_UBICACION,APP_PWD,APP_GCM,ULTIMA_COMPRA")] CALLCENTER_CLIENTES1 cALLCENTER_CLIENTES1)
        {
            if (ModelState.IsValid)
            {
                db.CALLCENTER_CLIENTES1Set.Add(cALLCENTER_CLIENTES1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cALLCENTER_CLIENTES1);
        }

        // GET: CALLCENTER_CLIENTES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CALLCENTER_CLIENTES1 cALLCENTER_CLIENTES1 = db.CALLCENTER_CLIENTES1Set.Find(id);
            if (cALLCENTER_CLIENTES1 == null)
            {
                return HttpNotFound();
            }
            return View(cALLCENTER_CLIENTES1);
        }

        // POST: CALLCENTER_CLIENTES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CLIENTE,TELEFONO,NOMBRE,CALLE,ESPECIFICACION,COLONIA_ID,REFERENCIA,ADICIONALES,APP_UBICACION,APP_PWD,APP_GCM,ULTIMA_COMPRA")] CALLCENTER_CLIENTES1 cALLCENTER_CLIENTES1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cALLCENTER_CLIENTES1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cALLCENTER_CLIENTES1);
        }

        // GET: CALLCENTER_CLIENTES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CALLCENTER_CLIENTES1 cALLCENTER_CLIENTES1 = db.CALLCENTER_CLIENTES1Set.Find(id);
            if (cALLCENTER_CLIENTES1 == null)
            {
                return HttpNotFound();
            }
            return View(cALLCENTER_CLIENTES1);
        }

        // POST: CALLCENTER_CLIENTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CALLCENTER_CLIENTES1 cALLCENTER_CLIENTES1 = db.CALLCENTER_CLIENTES1Set.Find(id);
            db.CALLCENTER_CLIENTES1Set.Remove(cALLCENTER_CLIENTES1);
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
