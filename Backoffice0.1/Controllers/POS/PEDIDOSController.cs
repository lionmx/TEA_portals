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
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        // variables de la session 
        string codigo_sucursal;
        int id_caja = 1;
        int id_marca = 1;
        int id_usuario = 1;
        int tipo_usuario = 0 ;
        IQueryable<C_marcas_sociedades> marca;


        // Variables
        int id_pedido = 0;

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
      
        public ActionResult Create2(C_pedidos c_pedidos, C_clientes c_clientes, C_direcciones_mx c_direcciones, C_telefonos c_telefonos, C_ventas_g c_ventas_g, C_eventos c_eventos, int[] c_ventas_pagos_tipo, Decimal[] c_ventas_pagos_montos, string[] c_ventas_pagos_tarjetas, float saldo)
        {
            codigo_sucursal= (string)Session["codigo_sucursal"];
            id_marca = (int)Session["id_marca"];
            tipo_usuario = (int)Session["LoggedIdRol"];
            List<CarritoItem> compras = Session["Carrito"] as List<CarritoItem>;
            DateTime now = DateTime.Now;

            c_pedidos.id_usuario_corporativo = (int)Session["LoggedId"];
            c_pedidos.fecha_entrega = now;
            c_pedidos.fecha_pedido = now;
            c_pedidos.id_marca = id_marca; 
            c_pedidos.id_tracking_status = 1;

            if (Session["id_pedido"] == null)
            {id_pedido=0;}
            else
            { id_pedido = (int)Session["id_pedido"];}

            if (c_pedidos.id_tipo_entrega == 1)
            {
                c_pedidos.codigo_sucursal=c_pedidos.codigo_sucursal;
            }
            if(tipo_usuario==6 && c_pedidos.id_pedido_tipo==1)
            {
                c_pedidos.codigo_sucursal = codigo_sucursal;
            }
            if (id_pedido == 0)
            {
                if (c_pedidos.id_pedido_tipo == 2 || c_pedidos.id_pedido_tipo == 3) // pedido de nuevo cliente o especial
                {
                    if (c_pedidos.id_tipo_entrega == 1 || c_pedidos.id_tipo_entrega == 2)
                    {
                        c_clientes.ultimacompra = now.ToString();
                        db.C_clientes.Add(c_clientes);
                        db.SaveChanges();
                        c_telefonos.status = true;
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
                    if (c_pedidos.id_tipo_entrega == 1)
                    {
                        db.C_direcciones_mx.Add(c_direcciones);
                        db.SaveChanges();
                        C_clientes_direccion c_clientes_direccion = new C_clientes_direccion();
                        c_clientes_direccion.id_cliente = c_clientes.id_cliente;
                        c_clientes_direccion.id_direccion = c_direcciones.id_direccion;
                        db.C_clientes_direccion.Add(c_clientes_direccion);
                        db.SaveChanges();
                    }
                }
                if ( c_pedidos.id_pedido_tipo == 1 || c_pedidos.id_pedido_tipo == 2) // asignacion de pedido tipo real
                {
                    if(tipo_usuario == 6)
                    {
                        c_pedidos.id_pedido_tipo = 1;
                    }
                    else if (tipo_usuario == 28)
                    {
                        c_pedidos.id_pedido_tipo = 2;
                    }
                }
                if (c_pedidos.id_pedido_tipo == 3) // pedido de especial
                {
                    c_pedidos.id_tracking_status = 1;
                    db.C_eventos.Add(c_eventos);
                    db.SaveChanges();
                    c_pedidos.id_evento = c_eventos.id_evento;
                }
                if (c_pedidos.id_cliente == null)
                {
                    c_pedidos.id_tipo_entrega = 2;
                }
                c_pedidos.id_direccion = c_direcciones.id_direccion;
                db.C_pedidos.Add(c_pedidos);
                db.SaveChanges();
                
                foreach (var item in compras)
                {
                    if (id_pedido == 0)
                    {
                        if (item.Sku != "")
                        {
                            C_pedidos_d c_pedidos_d = new C_pedidos_d
                            {
                                cantidad = 1,//cambiar
                                sku_producto = item.Sku,
                                id_pedido = c_pedidos.id_pedido,
                                pedido_d_status = true,
                                costo = (Decimal)item.Costo
                            };
                            db.C_pedidos_d.Add(c_pedidos_d);
                            db.SaveChanges();
                        }
                    }
                }
                Session["id_pedido"] = c_pedidos.id_pedido;
                id_pedido =(int) Session["id_pedido"];
            }

             c_ventas_g.id_pedido = id_pedido;
             c_ventas_g.codigo_sucursal = codigo_sucursal;//cambiar
             c_ventas_g.id_caja = id_caja;//cambiar
             c_ventas_g.folio = "folio" + id_pedido;//cambiar
             c_ventas_g.fecha = now;
             c_ventas_g.id_usuario = id_usuario;//cambiar
             c_ventas_g.id_impuesto = null;//cambiar
             c_ventas_g.id_venta_status = 1;
             
             db.C_ventas_g.Add(c_ventas_g);
             db.SaveChanges();

             for (int i = 0; i < c_ventas_pagos_montos.Length; i++)
             {
                 if (c_ventas_pagos_montos[i] > 0)
                 {
                     C_ventas_pagos c_ventas_pagos = new C_ventas_pagos();
                     c_ventas_pagos.id_venta = c_ventas_g.id_venta_g;
                     c_ventas_pagos.total = c_ventas_pagos_montos[i];
                     c_ventas_pagos.fecha = now;
                     c_ventas_pagos.id_pago_tipo = c_ventas_pagos_tipo[i];
                    if (c_ventas_pagos_tarjetas[i] != null)
                    {
                        c_ventas_pagos.tarjeta = c_ventas_pagos_tarjetas[i];
                    }
                   
                     db.C_ventas_pagos.Add(c_ventas_pagos);
                     db.SaveChanges();
                 }
             }
          
             foreach (var item in compras)
             {
                if (item.Cuenta == true && item.Sku != "" && item.Pagado == false)
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
                     compras.Find(x => x.Index == item.Index).Pagado = true;
                 }
             }
            
            Session["Carrito"] = compras;
            if (c_pedidos.id_pedido_tipo == 1)
            {
               // IMPRIMIRController ctl = new IMPRIMIRController();
               //  ctl.Imprimir(id_pedido,c_ventas_g.id_venta_g);
            }

            if (saldo == 0)
            {
                Session["Carrito"] = null;
                Session["id_pedido"] = null;
                id_pedido = 0;
            }
            
            return Content("True");
            
        }

        public bool RegistroMovimiento(int id_pedido)
        {
            DateTime now = DateTime.Now;
            codigo_sucursal = (string)Session["codigo_sucursal"];
            var pedido = from p in db.C_pedidos
                          where p.id_pedido == id_pedido
                          select p;
            C_insumo_mov_suc_g c_insumo_mov_suc_g = new C_insumo_mov_suc_g();
            foreach (var item in pedido)
            {
                c_insumo_mov_suc_g = new C_insumo_mov_suc_g
                {
                    fuente_origen = codigo_sucursal,//cambiar
                    origen_transaccion = "pedido:"+item.id_pedido,
                    fecha = now,
                    id_usuario = null,
                    id_tipo_mov = 4,//cambiar
                    observaciones = "Salida de insumos por venta",
                    id_insumo_mov_status = 5//cambiar
                };
                db.C_insumo_mov_suc_g.Add(c_insumo_mov_suc_g);
                
            }

            db.SaveChanges();

            var pedido_detalle = from p in db.C_pedidos_d
                                 where p.id_pedido == id_pedido
                                 select p;
            foreach (var item in pedido_detalle)
            {

                var insumos = from insumo in db.C_recetas
                              where insumo.sku == item.sku_producto && insumo.status == true
                              select insumo;
                foreach (var item_insumo in insumos)
                {
                    C_insumo_mov_suc_d c_insumo_suc_d = new C_insumo_mov_suc_d
                    {
                        id_insumo_mov = c_insumo_mov_suc_g.id_insumo_mov,
                        sku_insumo = item_insumo.sku_insumo,
                        cantidad = item_insumo.cantidad,
                        entrada_salida = false,
                        codigo_sucursal = codigo_sucursal //cambiar
                    };

                    db.C_insumo_mov_suc_d.Add(c_insumo_suc_d);
                    C_insumo_sucursal c_insumo_sucursal = db.C_insumo_sucursal.FirstOrDefault(m => m.sku_insumo == item_insumo.sku_insumo && m.codigo_sucursal == codigo_sucursal);
                  
                    var saldo = (Decimal)c_insumo_sucursal.saldo - (Decimal)item_insumo.cantidad;
                    c_insumo_sucursal.saldo = saldo;
                }
            }
            db.SaveChanges();
            return true;
        }
        public PartialViewResult AsignarSucursal(int id_colonia, string codigo_sucursal)
        {
            var sucursal_desvio = "";
            var nombre_sucursal = "";
         
            id_marca = (int)Session["id_marca"];
            IQueryable<C_sucursales_colonias> sucursal;
            if(id_colonia!=0)
            {
                sucursal = from sc in db.C_sucursales_colonias
                           join sm in db.C_sucursales_marcas on sc.codigo_sucursal equals sm.codigo_sucursal
                           where sm.id_marca == id_marca && sc.id_colonia == id_colonia
                           select sc;
            }
            else
            {
                sucursal = from sc in db.C_sucursales_colonias
                           join sm in db.C_sucursales_marcas on sc.codigo_sucursal equals sm.codigo_sucursal
                           where sm.id_marca == id_marca && sc.codigo_sucursal == codigo_sucursal
                           select sc;
            }
        
            foreach (var item in sucursal)
            {
                nombre_sucursal = item.C_sucursales.nombre;
                if (item.C_sucursales.status_servicio==false)
                {
                    sucursal_desvio = db.C_sucursales.Where(x => x.codigo_sucursal == item.C_sucursales.sucursal_desvio).Select(x => x.nombre).FirstOrDefault();
                    codigo_sucursal = item.C_sucursales.sucursal_desvio;
                }
                else
                {
                    codigo_sucursal = item.codigo_sucursal;
                }
            }
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == codigo_sucursal && (p.id_tracking_status == 2 || p.id_tracking_status == 1)
                          select p;

            var pedidos2 = from p in db.C_pedidos
                          where p.C_sucursales.nombre == sucursal_desvio && (p.id_tracking_status == 2 || p.id_tracking_status == 1)
                          select p;
            List<string> informacionEnvio = new List<string>();
            informacionEnvio.Add(codigo_sucursal);
            informacionEnvio.Add(nombre_sucursal);
            informacionEnvio.Add(sucursal_desvio);
            informacionEnvio.Add(pedidos.Count().ToString());
            informacionEnvio.Add(pedidos2.Count().ToString());
            return PartialView("../POS/Ventas/_InformacionEnvio",informacionEnvio);

        }
     
        public double ConsultarCostoEnvio(int id_colonia)
        {
            id_marca = (int)Session["id_marca"];
            double costo= 0.0;
            IQueryable<C_sucursales_colonias> sucursal;
            sucursal = from sc in db.C_sucursales_colonias
                       join sm in db.C_sucursales_marcas on sc.codigo_sucursal equals sm.codigo_sucursal
                       where sm.id_marca == id_marca && sc.id_colonia == id_colonia
                       select sc;
            foreach (var item in sucursal)
            {
                costo = (double)item.costo_envio;
            }
            return costo;

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
            if (status == 2)
            {
                RegistroMovimiento(id_pedido);
            }
            if(status == 5)
            {
                var repartidor_pedido = from rp in db.C_pedidos_empleados
                                        where rp.id_pedido == c_pedidos.id_pedido
                                        select rp;
                if (repartidor_pedido.Count() > 0)
                {
                    foreach (var item in repartidor_pedido)
                    {
                        C_pedidos_empleados c_pedidos_empleados = new C_pedidos_empleados();
                        c_pedidos_empleados.id_empleado = item.id_empleado;
                        c_pedidos_empleados.id_pedido = id_pedido;
                        c_pedidos_empleados.status = true;
                        c_pedidos_empleados.entrada_salida = true;
                        c_pedidos_empleados.fecha = DateTime.Now;
                        db.C_pedidos_empleados.Add(c_pedidos_empleados);
                       
                    }
                }
               
            }
            db.SaveChanges();
           
        }
        public int ConsultarMarcaPrincipal(string codigo_sucursal)
        {
            marca = from ms in db.C_marcas_sociedades
                        join se in db.C_sociedades_empresas on ms.id_sociedad equals se.id_sociedad
                        join es in db.C_empresas_sucursales on se.id_empresa equals es.id_empresa
                        where es.codigo_sucursal == codigo_sucursal && es.activo == true
                        select ms;

            if (marca.Count() > 0)
            {
                foreach (var item in marca)
                {
                    id_marca = (int)item.C_marcas_g.id_marca;
                }
            }
            return id_marca;
        }

        public PartialViewResult ConsultaPedidos(string busqueda)
        {
            var fecha = DateTime.Today;
            int id_marca = (int)Session["id_marca"];
            var pedidos = db.C_pedidos.Where(x => x.id_marca == id_marca && x.fecha_pedido>fecha &&(x.C_clientes.nombre.Contains(busqueda) )).ToList();
            return PartialView("../PEDIDOS/_VisualizaPedidos",pedidos);
        }
     
    }
}
