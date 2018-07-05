using Backoffice0._1.Controllers.POS;
using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace Backoffice0._1.Controllers.Delivery
{
    public class DELIVERYController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        // GET: DELIVERY
        public ActionResult Index()
        {

            //List<DeliveryModel> listaPedidosSucCocina = TrackingStatus.estadoCocina();
            //List<DeliveryModel> listaPedidosSucRecibido = TrackingStatus.estadoRecibido();
            //List<DeliveryModel> listaPedidosSucPorAsignar = TrackingStatus.estadoPorAsignar();
            //List<DeliveryModel> listaPedidosSucEntregados = TrackingStatus.estadoEntregados();
            //List<DeliveryModel> listaPedidosSucEntregando = TrackingStatus.estadoEntregando();
            //List<DeliveryModel> listaRepaSuc = TrackingStatus.totalRepaSuc();
            

            ViewBag.pedidosSucCocina = TrackingStatus.estadoCocina();
            ViewBag.pedidosSucRecibido = TrackingStatus.estadoRecibido();
            ViewBag.pedidosSucPorAsignar = TrackingStatus.estadoPorAsignar();
            ViewBag.pedidosSucEntregando = TrackingStatus.estadoEntregando();
            ViewBag.pedidosSucEntregados = TrackingStatus.estadoEntregados();
            ViewBag.totalRepaSuc = TrackingStatus.totalRepaSuc();
            ViewBag.totalRepaEntregando = TrackingStatus.totalRepaEntregando();
            C_cajas t = new C_cajas();
            t.codigo_sucursal = "SUC038";
            estatusCajaInicial(t);

            return View("/Views/Delivery/Index.cshtml");
        }

        private void estatusCajaInicial(C_cajas t)
        {
            var objcaja = db.C_cajas.Where(a => a.codigo_sucursal.Equals(t.codigo_sucursal));


            string estatus = objcaja.FirstOrDefault().status.Trim();

            t.status = estatus;
            //db.Database.ExecuteSqlCommand("UPDATE C_CAJAS SET STATUS = '" + estatus + "' WHERE CODIGO_SUCURSAL = '" + t.codigo_sucursal + "'");
            List<C_cajas> objLista = new List<C_cajas>();
            objLista.Add(t);
            ViewBag.sucEstado = objLista;
        }

        [HttpPost]
        public ActionResult estatusCajaModalView(string sucursal)
        {
            C_cajas c = new C_cajas();
            if (sucursal != null)
            {
                c.codigo_sucursal = sucursal;
                estatusCaja(c);
                var m = db.C_cajas.SqlQuery("SELECT * FROM C_CAJAS WHERE CODIGO_SUCURSAL = '" + c.codigo_sucursal + "'");
                c.status = m.FirstOrDefault().status.Trim();
                return View("/Views/Delivery/Index.cshtml");
            }
            return View("/Views/Delivery/Index.cshtml");
        }

        [HttpPost]
        public void confirmarCambio(string sucursal, string nombre)
        {
            string[] arr = nombre.Split();
            //var obj = from s in db.C_usuarios_corporativo join 
            //          em in db.C_empleados on  s.id_empleado equals em.id_empleado
            //          where em.nombres.Equals(arr[0].ToString()) && em.apellido_paterno.Equals(arr[1].ToString()) && em.apellido_materno.Equals(arr[2].ToString())
            //          select s;

            var obj_ = db.Database.SqlQuery<C_usuarios_corporativo>("select * from c_usuarios_corporativo s join c_empleados e on s.id_empleado = e.id_empleado where e.nombres='" + arr[0] + "' and e.apellido_paterno='" + arr[1] + "' and e.apellido_materno='" + arr[2]+"'");


            //var usuc = from s in db.C_usuarios_sucursales
            //           where s.id_usuario_corporativo.Equals(obj.FirstOrDefault().id_usuario_corporativo)
            //           select s;

            var usuc_ = db.C_usuarios_sucursales.SqlQuery("select * from c_usuarios_sucursales where id_usuario_corporativo ='" + obj_.FirstOrDefault().id_usuario_corporativo+ "'");

         
        foreach (var n in usuc_)
            {
                n.prestamo = true;
                n.codigo_sucursal_prestamo = sucursal;
                db.C_usuarios_sucursales.Add(n);
              
            }
            db.SaveChanges();
        }
        private void estatusCaja(C_cajas cc)
        {
            using (var context = new DB_CORPORATIVA_DEVEntities())
            {


                if (cc.codigo_sucursal != null)
                {
                    var objcaja = context.C_cajas.Where(a => a.codigo_sucursal.Equals(cc.codigo_sucursal)).FirstOrDefault();


                    string estatus = objcaja.status.Trim();



                    if (estatus.Equals("A"))
                    {
                        estatus = "I";
                    }
                    else if (estatus.Equals("I"))
                    {
                        estatus = "A";
                    }
                    context.Database.ExecuteSqlCommand("UPDATE C_CAJAS SET STATUS = '" + estatus + "' WHERE CODIGO_SUCURSAL = '" + cc.codigo_sucursal + "'");

                    List<C_cajas> objLista = new List<C_cajas>();
                    objLista.Add(cc);
                    ViewBag.sucEstado = objLista;
                    List<DeliveryModel> listaPedidosSucCocina = TrackingStatus.estadoCocina();
                    List<DeliveryModel> listaPedidosSucRecibido = TrackingStatus.estadoRecibido();
                    List<DeliveryModel> listaPedidosSucPorAsignar = TrackingStatus.estadoPorAsignar();
                    List<DeliveryModel> listaRepaSuc = TrackingStatus.totalRepaSuc();
                    List<DeliveryModel> listaPedidosSucEntregados = TrackingStatus.estadoEntregados();
                    List<DeliveryModel> listaPedidosSucEntregando = TrackingStatus.estadoEntregando();
                    ViewBag.pedidosSucCocina = listaPedidosSucCocina;
                    ViewBag.pedidosSucRecibido = listaPedidosSucRecibido;
                    ViewBag.pedidosSucPorAsignar = listaPedidosSucPorAsignar;
                    ViewBag.pedidosSucEntregados = listaPedidosSucEntregados;
                    ViewBag.pedidosSucEntregando = listaPedidosSucEntregando;
                    ViewBag.totalRepaSuc = listaRepaSuc;
                    ViewBag.totalRepaEntregando = TrackingStatus.totalRepaEntregando();
                    context.Entry(objcaja).Reload();
                }
            }
                
        }
  
        public ActionResult repartidorModalView(C_usuarios_sucursales m)
        {
            repartidoresEnSucursal(m);
            return View();
        }
        public ActionResult moverRepartidorModalView(C_usuarios_sucursales m)
        { 
            repartidoresEnSucursal(m);
            var suc = db.C_sucursales.ToList();
            List<string> sucList = new List<string>();
            foreach (var n in suc)
            {
                sucList.Add(n.nombre);
            }

            ViewBag.sucursales = sucList;
            return View();
        }
        public ActionResult pedidosCocinaModalView(C_pedidos m)
        {
            pedidosEnCocinaEnSucursal(m);
            return View();
        }

        public ActionResult pedidosEntregandoModalView(C_pedidos m)
        {
            pedidosEntregandoSucursal(m);
            return View();
        }


        public ActionResult pedidosEntregadosModalView(C_pedidos m)
        {
            pedidosEntregadosEnSucursal(m);
            return View();
        }


        public ActionResult pedidosRecibidosModalView(C_pedidos m)
        {
            pedidosRecibidosEnSucursal(m);
            return View();
        }

        public ActionResult pedidosPorAsignarModalView(C_pedidos m)
        {
            pedidosPorAsignarEnSucursal(m);
            return View();
        }

        public ActionResult moverRepartidor()
        {
            return View("Views/Delivery/moverRepartidorModalView.cshtml");
        }
        private void pedidosPorAsignarEnSucursal(C_pedidos m)
        {
            List<string> detallePedidos = new List<string>();
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == m.codigo_sucursal && p.id_tracking_status == 4
                          select p;

            foreach (var n in pedidos)
            {
                var codigoDetallePedido = from dp in db.C_pedidos_d
                                          where dp.id_pedido == n.id_pedido
                                          select dp;
                foreach (var y in codigoDetallePedido)
                {
                    var detallePedido = from d in db.C_productos_cat
                                        where d.sku == y.sku_producto
                                        select d;

                    detallePedidos.Add(codigoDetallePedido.FirstOrDefault().id_pedido + " " + detallePedido.FirstOrDefault().nombre);

                }
            }
            ViewBag.detallePedidosPorAsignar = detallePedidos;
        }
        public void repartidoresEnSucursal(C_usuarios_sucursales m)
        {
            var obj = from c in db.C_usuarios_corporativo
                      join u in db.C_usuarios_sucursales on c.id_usuario_corporativo equals u.id_usuario_corporativo
                      where c.id_rol == 4 && u.codigo_sucursal == m.codigo_sucursal
                      select c;

            List<string> reparNombre = new List<string>();
            if (obj != null)
            {

                foreach (var n in obj)
                {

                    var nombreRepartidor = from r in db.C_empleados
                                           where r.id_empleado == n.id_empleado
                                           select r.nombres + " " + r.apellido_paterno + " " + r.apellido_materno;

                    reparNombre.Add(nombreRepartidor.FirstOrDefault());
                }
                ViewBag.repartidorEnSucursal = reparNombre;
                Session["repartidorEnSucursal"] = ViewBag.repartidorEnSucursal;

            }


        }

        private void pedidosRecibidosEnSucursal(C_pedidos m)
        {
            List<string> detallePedidos = new List<string>();
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == m.codigo_sucursal && p.id_tracking_status == 1
                          select p;

            foreach (var n in pedidos)
            {
                var codigoDetallePedido = from dp in db.C_pedidos_d
                                          where dp.id_pedido == n.id_pedido
                                          select dp;
                foreach (var y in codigoDetallePedido)
                {
                    var detallePedido = from d in db.C_productos_cat
                                        where d.sku == y.sku_producto
                                        select d;

                    detallePedidos.Add(codigoDetallePedido.FirstOrDefault().id_pedido + " " + detallePedido.FirstOrDefault().nombre);

                }
            }
            ViewBag.detallePedidosRecibidos = detallePedidos;
        }

        private void pedidosEnCocinaEnSucursal(C_pedidos m)
        {
            List<string> detallePedidos = new List<string>();
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == m.codigo_sucursal && p.id_tracking_status == 2
                          select p;

            foreach (var n in pedidos)
            {
                var codigoDetallePedido = from dp in db.C_pedidos_d
                                          where dp.id_pedido == n.id_pedido
                                          select dp;
                foreach (var y in codigoDetallePedido)
                {
                    var detallePedido = from d in db.C_productos_cat
                                        where d.sku == y.sku_producto
                                        select d;

                    detallePedidos.Add(codigoDetallePedido.FirstOrDefault().id_pedido + " " + detallePedido.FirstOrDefault().nombre);

                }
            }
            ViewBag.detallePedidos = detallePedidos;
        }
        private void pedidosEntregandoSucursal(C_pedidos m)
        {
            List<string> detallePedidos = new List<string>();
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == m.codigo_sucursal && p.id_tracking_status == 4
                          select p;

            foreach (var n in pedidos)
            {
                var codigoDetallePedido = from dp in db.C_pedidos_d
                                          where dp.id_pedido == n.id_pedido
                                          select dp;
                foreach (var y in codigoDetallePedido)
                {
                    var detallePedido = from d in db.C_productos_cat
                                        where d.sku == y.sku_producto
                                        select d;

                    detallePedidos.Add(codigoDetallePedido.FirstOrDefault().id_pedido + " " + detallePedido.FirstOrDefault().nombre);

                }
            }
            ViewBag.detallePedidosEntregando = detallePedidos;
        }

        private void pedidosEntregadosEnSucursal(C_pedidos m)
        {
            List<string> detallePedidos = new List<string>();
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == m.codigo_sucursal && p.id_tracking_status == 5
                          select p;

            foreach (var n in pedidos)
            {
                var codigoDetallePedido = from dp in db.C_pedidos_d
                                          where dp.id_pedido == n.id_pedido
                                          select dp;
                foreach (var y in codigoDetallePedido)
                {
                    var detallePedido = from d in db.C_productos_cat
                                        where d.sku == y.sku_producto
                                        select d;

                    detallePedidos.Add(codigoDetallePedido.FirstOrDefault().id_pedido + " " + detallePedido.FirstOrDefault().nombre);

                }
            }
            ViewBag.detallePedidosEntregados = detallePedidos;


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