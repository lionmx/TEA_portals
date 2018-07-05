using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Backoffice0._1.Models;

namespace Backoffice0._1.Controllers.Clientes
{
    public class CLIENTESController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        // GET: CLIENTES
        public ActionResult Index()
        {
            return View(db.C_clientes.ToList());
        }

        // GET: CLIENTES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_clientes c_clientes = db.C_clientes.Find(id);
            if (c_clientes == null)
            {
                return HttpNotFound();
            }
            return View(c_clientes);
        }

        // GET: CLIENTES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CLIENTES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cliente,nombre,ultimacompra,status,email,password")] C_clientes c_clientes)
        {
            if (ModelState.IsValid)
            {
                db.C_clientes.Add(c_clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_clientes);
        }

        // GET: CLIENTES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_clientes c_clientes = db.C_clientes.Find(id);
            if (c_clientes == null)
            {
                return HttpNotFound();
            }
            return View(c_clientes);
        }

        // POST: CLIENTES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cliente,nombre,ultimacompra,status,email,password")] C_clientes c_clientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_clientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_clientes);
        }

        // GET: CLIENTES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_clientes c_clientes = db.C_clientes.Find(id);
            if (c_clientes == null)
            {
                return HttpNotFound();
            }
            return View(c_clientes);
        }

        // POST: CLIENTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_clientes c_clientes = db.C_clientes.Find(id);
            db.C_clientes.Remove(c_clientes);
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

        public ActionResult BuscarClienteTelefono(string telefono)
        {
            var cliente_direccion = from c in db.C_clientes
                          join c_t in db.C_clientes_telefono on c.id_cliente equals c_t.id_cliente
                          join c_d in db.C_clientes_direccion on c.id_cliente equals c_d.id_cliente
                          where (c_t.C_telefonos.telefono.Contains(telefono) || c.clave_cliente.Contains(telefono))
                          select c_d;
            if (cliente_direccion.Count()==0)
            {
                return Content("False");
            }

            return PartialView("../POS/Ventas/_Clientes", cliente_direccion);
        }
    }
}
