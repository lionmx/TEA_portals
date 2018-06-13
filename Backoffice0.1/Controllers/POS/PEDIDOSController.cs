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
    public class PEDIDOSController : Controller
    {
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();

        // GET: PEDIDOS
        public ActionResult Index()
        {
            var c_pedidos = db.C_pedidos.Include(c => c.C_clientes).Include(c => c.C_eventos).Include(c => c.C_pedidos_tipo).Include(c => c.C_tracking_status);
            return View(c_pedidos.ToList());
        }

        // GET: PEDIDOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_pedidos c_pedidos = db.C_pedidos.Find(id);
            if (c_pedidos == null)
            {
                return HttpNotFound();
            }
            return View(c_pedidos);
        }


        public ActionResult Create2(C_pedidos c_pedidos, C_clientes c_clientes, C_direcciones c_direcciones, C_telefonos c_telefonos, C_ventas_g c_ventas_g, C_eventos c_eventos, int[] c_ventas_pagos_tipo, Decimal[] c_ventas_pagos_montos)
        {
            DateTime now = DateTime.Now;

            c_pedidos.fecha_entrega = now;
            c_pedidos.fecha_pedido = now;
            c_pedidos.codigo_sucursal = "SUC001"; //cambiar
            c_pedidos.id_marca = 1; //cambiar

            if (c_pedidos.id_pedido_tipo==1) // pedido de mostrador
            {
                
            }
            if (c_pedidos.id_pedido_tipo == 2 || c_pedidos.id_pedido_tipo == 3) // pedido de call center
            {
               
                if(c_pedidos.id_tipo_entrega == 1 || c_pedidos.id_tipo_entrega == 2)
                {
                    c_clientes.ultimacompra = now.ToString();
                    db.C_clientes.Add(c_clientes);
                    db.SaveChanges();

                    db.C_telefonos.Add(c_telefonos);
                    db.SaveChanges();

                    C_clientes_telefono c_clientes_telefono = new C_clientes_telefono();
                    c_clientes_telefono.id_cliente = c_clientes.id_cliente;
                    c_clientes_telefono.id_telefono = c_telefonos.id_telefono;
                    db.C_clientes_telefono.Add(c_clientes_telefono);
                    db.SaveChanges();

                    c_pedidos.id_cliente = c_clientes.id_cliente;
                    c_pedidos.id_telefono = c_telefonos.id_telefono;
                }
                if(c_pedidos.id_tipo_entrega == 2)
                {
                    db.C_direcciones.Add(c_direcciones);
                    db.SaveChanges();

                   /* C_clientes_direccion_old c_clientes_direccion = new C_clientes_direccion_old();
                    c_clientes_direccion.id_cliente = c_clientes.id_cliente;
                    c_clientes_direccion.id_direccion = c_direcciones.id_direccion;
                    db.C_clientes_direccion_old.Add(c_clientes_direccion);
                    db.SaveChanges();*/
                }
            }

            if (c_pedidos.id_pedido_tipo == 3) // pedido de especial
            {
                db.C_eventos.Add(c_eventos);
                db.SaveChanges();

                c_pedidos.id_evento = c_eventos.id_evento;

            }
            
          
            db.C_pedidos.Add(c_pedidos);
            db.SaveChanges();

            c_ventas_g.codigo_sucursal = "SUC001";//cambiar
            c_ventas_g.id_caja = 1;//cambiar
            c_ventas_g.folio = "folio" + c_pedidos.id_pedido;//cambiar
            c_ventas_g.fecha = now;
            c_ventas_g.id_usuario = 1;//cambiar
            c_ventas_g.id_impuesto = 1;//cambiar
            c_ventas_g.total = 111; // checar el impuesto para definir totales
            c_ventas_g.id_venta_status = 1;
            c_ventas_g.id_pedido = c_pedidos.id_pedido;
            db.C_ventas_g.Add(c_ventas_g);
            db.SaveChanges();

            for (int i = 0; i < c_ventas_pagos_tipo.Length; i++)
            {
                if (c_ventas_pagos_montos[i] > 0)
                {
                    C_ventas_pagos c_ventas_pagos = new C_ventas_pagos();
                    c_ventas_pagos.id_venta = c_ventas_g.id_venta_g;
                    c_ventas_pagos.total = c_ventas_pagos_montos[i];
                    c_ventas_pagos.fecha = now;
                    c_ventas_pagos.id_pago_tipo = c_ventas_pagos_tipo[i];
                    db.C_ventas_pagos.Add(c_ventas_pagos);
                    db.SaveChanges();
                }
            }
            foreach (var item in Session["Carrito"] as List<CarritoItem>)
            {
                C_pedidos_d c_pedidos_d = new C_pedidos_d
                {
                    cantidad = "1",//cambiar
                    sku_producto = item.Sku,
                    id_pedido = c_pedidos.id_pedido
                };
                db.C_pedidos_d.Add(c_pedidos_d);
                db.SaveChanges();

                C_insumo_mov_suc_g c_insumo_mov_suc_g = new C_insumo_mov_suc_g
                {
                    fuente_origen = "SUC001",//cambiar
                    origen_transaccion = c_ventas_g.folio,
                    fecha = now,
                    id_usuario = null,
                    id_tipo_mov = 4,//cambiar
                    observaciones = "Salida de insumos por venta",
                    id_insumo_mov_status = 5//cambiar
                };
                db.C_insumo_mov_suc_g.Add(c_insumo_mov_suc_g);
                db.SaveChanges();

                var insumos = from insumo in db.C_recetas
                              where insumo.sku == item.Sku && insumo.status == true
                              select insumo;
                foreach (var item_insumo in insumos)
                {
                    C_insumo_mov_suc_d c_insumo_suc_d = new C_insumo_mov_suc_d
                    {
                        id_insumo_mov = c_insumo_mov_suc_g.id_insumo_mov,
                        sku_insumo = item_insumo.sku_insumo,
                        cantidad = item_insumo.cantidad,
                        entrada_salida = false,
                        codigo_sucursal = "SUC001" //cambiar
                    };
                    db.C_insumo_mov_suc_d.Add(c_insumo_suc_d);

                }
                db.SaveChanges();

                if (item.Cuenta == true && item.Sku != "")
                {
                    C_ventas_d c_ventas_d = new C_ventas_d
                    {
                        sku_producto = item.Sku,
                        id_promocion = item.Id_promocion,
                        id_venta_g = c_ventas_g.id_venta_g,
                        status = true,
                        cantidad = 1,//cambiar
                        precio = (Decimal)item.Costo
                    };
                    db.C_ventas_d.Add(c_ventas_d);
                    db.SaveChanges();
                }
            }
            //Registro c_ventas_pagos

            return RedirectToAction("Index");
        }

        // GET: PEDIDOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_pedidos c_pedidos = db.C_pedidos.Find(id);
            if (c_pedidos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.C_clientes, "id_cliente", "nombre", c_pedidos.id_cliente);
            ViewBag.id_evento = new SelectList(db.C_eventos, "id_evento", "descripcion_evento", c_pedidos.id_evento);
            ViewBag.id_pedido_tipo = new SelectList(db.C_pedidos_tipo, "id_pedido_tipo", "tipo", c_pedidos.id_pedido_tipo);
            ViewBag.id_tracking_status = new SelectList(db.C_tracking_status, "C_id_tracking_status", "nombre_tracking_status", c_pedidos.id_tracking_status);
            return View(c_pedidos);
        }

        // POST: PEDIDOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pedido,id_cliente,fecha_pedido,fecha_entrega,id_telefono,id_direccion,monto,id_pago_tipo,pago_cantidad,id_pedido_tipo,id_tracking_status,id_pedido_status,id_evento,id_bo_g")] C_pedidos c_pedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_pedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.C_clientes, "id_cliente", "nombre", c_pedidos.id_cliente);
            ViewBag.id_evento = new SelectList(db.C_eventos, "id_evento", "descripcion_evento", c_pedidos.id_evento);
            ViewBag.id_pedido_tipo = new SelectList(db.C_pedidos_tipo, "id_pedido_tipo", "tipo", c_pedidos.id_pedido_tipo);
            ViewBag.id_tracking_status = new SelectList(db.C_tracking_status, "C_id_tracking_status", "nombre_tracking_status", c_pedidos.id_tracking_status);
            return View(c_pedidos);
        }

        // GET: PEDIDOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_pedidos c_pedidos = db.C_pedidos.Find(id);
            if (c_pedidos == null)
            {
                return HttpNotFound();
            }
            return View(c_pedidos);
        }

        // POST: PEDIDOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_pedidos c_pedidos = db.C_pedidos.Find(id);
            db.C_pedidos.Remove(c_pedidos);
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

        public void CambiarStatus(int status, int id_pedido)
        {
            C_pedidos c_pedidos = db.C_pedidos.Find(id_pedido);
            c_pedidos.id_tracking_status = status;
            db.SaveChanges();
           
        }

    }
}
