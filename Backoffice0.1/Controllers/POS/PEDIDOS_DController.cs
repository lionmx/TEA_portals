using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backoffice0._1.Models;

namespace Backoffice0._1.Controllers.POS
{
    public class PEDIDOS_DController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        // GET: PEDIDOS_D
        public ActionResult Index()
        {
            //var c_pedidos_d = db.C_pedidos_d.Include(c => c.C_pedidos);
            return View();
        }

        // GET: PEDIDOS_D/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_pedidos_d c_pedidos_d = db.C_pedidos_d.Find(id);
            if (c_pedidos_d == null)
            {
                return HttpNotFound();
            }
            return View(c_pedidos_d);
        }

        // GET: PEDIDOS_D/Create
        public ActionResult Create()
        {
            ViewBag.id_pedido = new SelectList(db.C_pedidos, "id_pedido", "fecha_pedido");
            return View();
        }

        // POST: PEDIDOS_D/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pedido_d,id_pedido,id_producto,cantidad,pedido_d_status")] C_pedidos_d c_pedidos_d)
        {
            if (ModelState.IsValid)
            {
                db.C_pedidos_d.Add(c_pedidos_d);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pedido = new SelectList(db.C_pedidos, "id_pedido", "fecha_pedido", c_pedidos_d.id_pedido);
            return View(c_pedidos_d);
        }

        // GET: PEDIDOS_D/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_pedidos_d c_pedidos_d = db.C_pedidos_d.Find(id);
            if (c_pedidos_d == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_pedido = new SelectList(db.C_pedidos, "id_pedido", "fecha_pedido", c_pedidos_d.id_pedido);
            return View(c_pedidos_d);
        }

        // POST: PEDIDOS_D/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pedido_d,id_pedido,id_producto,cantidad,pedido_d_status")] C_pedidos_d c_pedidos_d)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_pedidos_d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_pedido = new SelectList(db.C_pedidos, "id_pedido", "fecha_pedido", c_pedidos_d.id_pedido);
            return View(c_pedidos_d);
        }

        // GET: PEDIDOS_D/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_pedidos_d c_pedidos_d = db.C_pedidos_d.Find(id);
            if (c_pedidos_d == null)
            {
                return HttpNotFound();
            }
            return View(c_pedidos_d);
        }

        // POST: PEDIDOS_D/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_pedidos_d c_pedidos_d = db.C_pedidos_d.Find(id);
            db.C_pedidos_d.Remove(c_pedidos_d);
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
