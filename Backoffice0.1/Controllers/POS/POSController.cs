using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Backoffice0._1.Controllers.POS;
using System.Data.Entity;
using System.Configuration;
using FacturaControl;
using System.Configuration;
using System.Media;

namespace Backoffice0._1.Controllers
{
    public class POSController : Controller
    {
        // GET: POS
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        // variables de la session 
        
        string codigo_sucursal;
        int id_caja = 1;


        List<CarritoItem> compras;
        List<CarritoComboItem> carrito_combo;
        List<CarritoComboItem> detalle_combo;
        List<int> productos_promocion = new List<int>();
        List<float> productos_promocion_costos = new List<float>();
        List<int> detalles_combo;
        int prod_ele_det;
        int index_carrito;
        int index_detalle_combo;
        int cant_promociones;
        int ultima_promo;
        float total_promo;
        float desc_promo;
        string nombre_promocion;
        int id_promocion_real;
        int tipo_promocion;
        CarritoItem promocion_remover;
       

        public ActionResult Index()
        {
            Session["carrito"] = compras;
            ViewBag.compras = 0;
            var sucursales = db.C_sucursales.Where(m => m.activo == true).Select(m => m.nombre).Distinct().ToList();
            ViewBag.Sucursales = new SelectList(sucursales, "", "");
            ViewBag.IdRol = (int)Session["LoggedIdRol"];
            return View("Ventas/Index");
        }
        public ActionResult AgregaCarrito(string nombre, float costo, string sku, bool promocion, bool prom_pos, int id_promocion,int id_producto)
        {
            if (Session["Carrito"] == null)
            {
                compras = new List<CarritoItem>();
                index_carrito = 1;
            }
            else
            {
                compras = (List<CarritoItem>)Session["Carrito"];
                index_carrito = compras.Max(x => x.Index) + 1;
            }

            compras.Add(new CarritoItem(nombre, costo, sku, promocion, index_carrito, prom_pos, id_promocion, true,id_producto,false,0,0));
            Session["Carrito"] = compras;
            ViewBag.compras = compras;
            ValidarPromocion(nombre, costo, sku, promocion, prom_pos, id_promocion,false);
            return PartialView("Ventas/_AgregaCarrito");
        }

        public ActionResult RemueveCarrito(string sku, int index_carrito)
        {
            CarritoItem eliminar = null;
            compras = (List<CarritoItem>)Session["Carrito"];
            var item = compras.FirstOrDefault(x => x.Sku == sku && x.Index == index_carrito);
            promocion_remover = compras.Find(x => x.Id_promocion == item.Id_promocion);
            var id_promo = promocion_remover.Id_promocion;
            if (id_promo == 0)
            {
                compras.Remove(item);
            }
            else
            {
                for (int i = 0; i <= compras.Count(); i++)
                {
                    eliminar = compras.Find(x => x.Id_promocion == id_promo);
                    compras.Remove(eliminar);
                }
                promocion_rem(id_promo);// revalidar_promocion
            }
           
            if (compras.Count() == 0)
            {
                Session["Carrito"] = null;
                ViewBag.compras = "";
            }
            ViewBag.compras = Session["Carrito"];
            return PartialView("Ventas/_AgregaCarrito");
        }
        public void promocion_rem(int id_promo)// Re validar promocion
        {
            foreach (var item3 in compras.Where(x => x.Id_promocion == id_promo || x.Id_promocion == id_promo + 1))
            {
                item3.Promocion = false;
                item3.Id_promocion = 0;
                var producto = from p in db.C_productos_precios
                               where (p.sku_producto == item3.Sku && p.id_zona == 1)
                               select p;
                foreach (var item4 in producto)
                {
                    item3.Costo = (float)item4.precio_publico;
                }
            }
            var item2 = compras.FirstOrDefault(x => x.Id_promocion == 0 && x.Index == 0);
            compras.Remove(item2);
            foreach (var item4 in compras.Where(x => x.Promocion == false))
            {
                ValidarPromocion(item4.Producto, item4.Costo, item4.Sku, item4.Promocion, item4.Prom_pos, item4.Id_promocion,false);
            }
        }

        public ActionResult VisualizaCarrito()
        {
            compras = (List<CarritoItem>)Session["Carrito"];
            return PartialView("Ventas/_VisualizaCarrito");
        }

        public ActionResult VisualizaCarrito1()
        {
            compras = (List<CarritoItem>)Session["Carrito"];
            return PartialView("Ventas/_AgregaCarrito");
        }

        public PartialViewResult ConsultaCategorias()
        {
            return PartialView("Ventas/_Categorias", db.C_producto_tipo.ToList());
        }
        
        public PartialViewResult Clientes()
        {
            return PartialView("Ventas/_Clientes", null);
        }

        public PartialViewResult ConsultaBotonesEspeciales()
        {
            return PartialView("Ventas/_BotonesEspeciales", db.C_grupo_productos_tipos.ToList());
        }

        public PartialViewResult ConsultaSubCategorias(int id_tipo_producto)
        {
            var subcategorias = from sc in db.C_producto_clasificacion
                                where sc.id_tipo_producto == id_tipo_producto
                                select sc;

            return PartialView("Ventas/_SubCategorias", subcategorias);
        }
        //Botones especiales subclases
        public PartialViewResult ConsultaSubClases(int id_grupo_producto_tipo)
        {
            var subclases = from sc in db.C_grupo_productos_subclases
                            where sc.id_grupo_producto_tipo == id_grupo_producto_tipo
                            select sc;

            return PartialView("Ventas/_SubClases", subclases);
        }

        public PartialViewResult FiltraProductos(int id_cla)
        {
            codigo_sucursal = (string)Session["codigo_sucursal"];
            if (id_cla == 0)
            {
                id_cla = 1;
            }
            //IQueryable<Productos> productos = null;
            var productos = from p in db.C_productos_cat
                                join ps in db.C_productos_sucursal on p.sku equals ps.sku_producto
                                join pp in db.C_productos_precios on p.sku equals pp.sku_producto
                                where (p.id_producto_clasificacion == id_cla && ps.codigo_sucursal == codigo_sucursal && pp.id_zona == 1 && ps.activo==true)
                                select new Productos { c_productos_cat = p, c_productos_sucursal = ps, c_productos_precios = pp };
            return PartialView("Ventas/_Productos", productos);
        }
        public PartialViewResult ConsultaBanners()
        {
            codigo_sucursal = (string)Session["codigo_sucursal"];
            var banners = db.C_grupo_productos_sucursales.Where(m => m.codigo_sucursal == codigo_sucursal && m.C_grupo_productos_g.status == true).ToList();
            
            return PartialView("Ventas/_Banners", banners);
        }

        public PartialViewResult ValidarPromocion(string nombre, float costo, string sku, bool promocion, bool prom_pos, int id_promocion, bool codigo)
        {
            codigo_sucursal = (string)Session["codigo_sucursal"];
            var bandera = 0;
            total_promo = 0;
            int id_grupo_aplicacion = 0;

            /*Validar promociones*/
            var promocion_producto = from gpp in db.C_grupo_productos_prods
                                     join gps in db.C_grupo_productos_sucursales on gpp.C_grupo_productos_d.id_grupo_productos equals gps.id_grupo_productos
                                     where gpp.sku_producto == sku && gpp.C_grupo_productos_d.C_grupo_productos_g.status == true
                                     && gpp.C_grupo_productos_d.C_grupo_productos_g.fecha_inicio <= DateTime.Now
                                     && gpp.C_grupo_productos_d.C_grupo_productos_g.fecha_final >= DateTime.Now
                                     && gps.codigo_sucursal == codigo_sucursal && gpp.C_grupo_productos_d.C_grupo_productos_g.id_grupo_producto_tipo == 1
                                     && gpp.C_grupo_productos_d.C_grupo_productos_g.requiere_codigo==codigo
                                     select gpp;

            if (promocion_producto.Count() > 0)
            {
                foreach (var item2 in promocion_producto)
                {
                    var agregados = 0;
                    var contador = 0;

                    bandera = 0;
                    var detalles = from gpd in db.C_grupo_productos_d
                                   where gpd.id_grupo_productos == item2.C_grupo_productos_d.C_grupo_productos_g.id_grupo_productos
                                   select gpd;

                    foreach (var item3 in detalles)
                    {
                        agregados = 0;
                        bandera = 0;
                        contador++;
                        var productos = from gpp in db.C_grupo_productos_prods
                                        where gpp.id_grupo_productos_det
                                        == item3.id_grupo_productos_det
                                        select gpp;
                        if (productos.Count() > 0)
                        {
                            foreach (var item4 in compras)
                            {
                                if (item4.Promocion == false && agregados < item3.no_prod_seleccion)
                                {
                                    foreach (var item5 in productos)
                                    {
                                        if (item4.Sku == item5.sku_producto && item4.Prom_pos == false)
                                        {
                                            compras.Find(x => x.Index == item4.Index).Prom_pos = true;
                                            productos_promocion.Add(item4.Index);
                                            bandera = 1;
                                            agregados++;
                                            var count_det = detalles.Count();
                                            if (contador == detalles.Count() && agregados==item3.no_prod_seleccion)
                                            {
                                                id_promocion_real = (int)item2.C_grupo_productos_d.C_grupo_productos_g.id_grupo_productos;
                                                tipo_promocion = (int)item2.C_grupo_productos_d.C_grupo_productos_g.id_grupo_producto_tipo;
                                                desc_promo = (float)item2.C_grupo_productos_d.C_grupo_productos_g.descuento;
                                                cant_promociones++;
                                                Session["ultima_promo"] = compras.Max(x => x.Id_promocion);
                                                nombre_promocion = item2.C_grupo_productos_d.C_grupo_productos_g.nombre;
                                                id_grupo_aplicacion = (int)item2.C_grupo_productos_d.C_grupo_productos_g.id_grupo_aplica_operacion;

                                                foreach (var item7 in productos_promocion)
                                                {
                                                    compras.Find(x => x.Index == item7).Promocion = true;
                                                    compras.Find(x => x.Index == item7).Id_promocion_real= id_promocion_real;
                                                    compras.Find(x => x.Index == item7).Id_tipo_promocion= tipo_promocion;
                                                    compras.Find(x => x.Index == item7).Id_promocion = (int)Session["ultima_promo"] + 1;
                                                    total_promo = total_promo + compras.Find(x => x.Index == item7).Costo;
                                                    productos_promocion_costos.Add(compras.Find(x => x.Index == item7).Costo);
                                                    compras.Find(x => x.Index == item7).Costo = 0;
                                                }
                                                if (id_grupo_aplicacion == 1)
                                                {
                                                    total_promo = productos_promocion_costos.Max();
                                                }

                                                if (id_grupo_aplicacion == 2)
                                                {
                                                    total_promo = total_promo-(total_promo * ((100 - desc_promo) / 100));
                                                }
                                                if (id_grupo_aplicacion == 3)
                                                {
                                                    total_promo = (float)item2.C_grupo_productos_d.C_grupo_productos_g.precio_unico;
                                                }
                                                productos_promocion.Clear();
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (bandera == 0 || agregados != item3.no_prod_seleccion)
                        {
                            break;
                        }
                    }
                    /*limpiar variable prom_pos*/
                    foreach (var item6 in compras)
                    {
                        item6.Prom_pos = false;
                    }
                }
            }
            if (cant_promociones > 0)
            {
                compras.Add(new CarritoItem(nombre_promocion, total_promo, "", true, 0, false, (int)Session["ultima_promo"] + 2, true, 0,false,id_promocion_real,tipo_promocion));
            }
            compras.Sort((x, y) => x.Id_promocion.CompareTo(y.Id_promocion));
            Session["Carrito"] = compras;
            return PartialView("Ventas/_AgregaCarrito");
        }
        //Configuracion de combos
        public PartialViewResult ConsultaCombos(int id_subclase)
        {
            codigo_sucursal = (string)Session["codigo_sucursal"];
            var combos = from gpp in db.C_grupo_productos_g
                         join gps in db.C_grupo_productos_sucursales on gpp.id_grupo_productos equals gps.id_grupo_productos
                         where gpp.id_grupo_producto_subclase == id_subclase && gpp.status == true
                         && gpp.fecha_inicio <= DateTime.Now
                         && gpp.fecha_final >= DateTime.Now
                         && gps.codigo_sucursal == codigo_sucursal
                         select gpp;
            return PartialView("Ventas/_Combos", combos);
        }

        public PartialViewResult AgregaCombo(float costo_combo, string nombre_combo)
        {
            compras = (List<CarritoItem>)Session["Carrito"];
            if (Session["Carrito"] == null)
            {
                compras = new List<CarritoItem>();
                index_carrito = 1;
                ultima_promo = 1;
            }
            else
            {
                compras = (List<CarritoItem>)Session["Carrito"];
                index_carrito = compras.Max(x => x.Index) + 1;
                ultima_promo = compras.Max(x => x.Id_promocion);
            }

            if (Session["CarritoCombo"] == null)
            {
                detalle_combo = new List<CarritoComboItem>() ;
            }
            else
            {
                detalle_combo = (List<CarritoComboItem>)Session["CarritoCombo"];
               
            }
           
            var id_combo = 0;
            
            foreach (var item in detalle_combo)
            {
                id_combo = item.Id_promocion;
                compras.Add(new CarritoItem(item.Producto, 0, item.Sku,true, index_carrito, false, ultima_promo + 1, true,item.Id_producto, false, item.Id_promocion,2));
                index_carrito++;
            }
            
            compras.Add(new CarritoItem(nombre_combo, costo_combo, "", true, 0, false, ultima_promo + 2, true, 0, false,id_combo,2));
            Session["Carrito"] = compras;
            return PartialView("Ventas/_AgregaCarrito");
        }

        public void ConsultaDetallesCombo(int id_combo)
        {
            detalles_combo = new List<int>();
            index_carrito = 1;
            var detalles = from gpd in db.C_grupo_productos_d
                           where gpd.id_grupo_productos == id_combo
                           select gpd;
            Session["Detalles_combo"] = detalles_combo;
            foreach (var item in detalles)
            {
                detalles_combo.Add(item.id_grupo_productos_det);
            }
        }

        public PartialViewResult ConsultaCombosProductos(string sku, string producto, float costo, int id_producto, int id_combo)
        {
            if (Session["Detalle"] == null) { index_detalle_combo = 0; }
            else { index_detalle_combo = (int)Session["Detalle"]; }

            if (Session["ProductosElegidos"] == null) { prod_ele_det = 0; }
            else { prod_ele_det = (int)Session["ProductosElegidos"]; }

            if (sku == "")
            {
                Session["CarritoCombo"] = null;
                carrito_combo = new List<CarritoComboItem>();
                Session["Detalle"] = null;
                index_detalle_combo = 0;
                Session["ProductosElegidos"] = null;
                prod_ele_det = 0;
            }

            detalles_combo = (List<int>)Session["Detalles_combo"];
            int id_grupo_productos_d = detalles_combo[index_detalle_combo];
            var productos = from p in db.C_productos_cat
                            join gpp in db.C_grupo_productos_prods on p.sku equals gpp.sku_producto
                            join pp in db.C_productos_precios on p.sku equals pp.sku_producto
                            where (gpp.id_grupo_productos_det == id_grupo_productos_d && pp.id_zona == 1)
                            select new Productos { c_productos_cat = p, c_productos_precios = pp, c_grupo_productos_prods = gpp };

            if (sku != "")
            {
                if (Session["CarritoCombo"] == null)
                {
                    carrito_combo = new List<CarritoComboItem>();
                }
                else
                {
                    carrito_combo = (List<CarritoComboItem>)Session["CarritoCombo"];
                }

                carrito_combo.Add(new CarritoComboItem(producto, 1, sku, costo, id_producto, id_combo));
                Session["CarritoCombo"] = carrito_combo;

                prod_ele_det++;
                var detalle = from gpd in db.C_grupo_productos_d
                              where gpd.id_grupo_productos_det == id_grupo_productos_d
                              select gpd;

                foreach (var item in detalle)
                {
                    if (prod_ele_det < item.no_prod_seleccion)
                    {
                        Session["ProductosElegidos"] = prod_ele_det;
                    }
                    else
                    {
                        prod_ele_det = 0;
                        Session["ProductosElegidos"] = prod_ele_det;
                        index_detalle_combo++;
                        if (index_detalle_combo < detalles_combo.Count())
                        {
                            Session["Detalle"] = index_detalle_combo;
                            id_grupo_productos_d = detalles_combo[index_detalle_combo];
                        }
                        else
                        {
                            id_grupo_productos_d = 0;
                        }
                        productos = from p in db.C_productos_cat
                                    join gpp in db.C_grupo_productos_prods on p.sku equals gpp.sku_producto
                                    join pp in db.C_productos_precios on p.sku equals pp.sku_producto
                                    where (gpp.id_grupo_productos_det == id_grupo_productos_d && pp.id_zona == 1)
                                    select new Productos { c_productos_cat = p, c_productos_precios = pp, c_grupo_productos_prods = gpp };
                       
                    }
                }
            }
            return PartialView("Ventas/_ConfiguraCombo", productos);
        }
        public PartialViewResult AgregaCarritoCombo()
        {
            return PartialView("Ventas/_AgregaCarritoCombo");
        }
        public PartialViewResult ConsultaInformacionCombo(int id_combo)
        {
            var combo = from gpg in db.C_grupo_productos_g
                        where gpg.id_grupo_productos == id_combo
                        select gpg;
            return PartialView("Ventas/_DetalleCombo", combo);
        }
        public void ProductoCuenta(int index, bool status)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["Carrito"];
            compras.Find(x => x.Index == index).Cuenta = status;
            Session["Carrito"] = compras;
        }
      
        public PartialViewResult ConsultaTrackingPedidos()
        {
            codigo_sucursal = (string)Session["codigo_sucursal"];
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == codigo_sucursal
                          select p;
            if (Session["PedidosCount"] != null)
            {
                int pedidos_cant = (int)Session["PedidosCount"];
                if (pedidos_cant < pedidos.Count())
                {
                    /*WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                    wplayer.URL = @"C:\Users\Desarrollo1\source\repos\backoffice_lion\Backoffice0.1\Content\production\sounds\1.mp3";
                    wplayer.controls.play();*/
                }
            }
            Session["PedidosCount"] = pedidos.Count();

            return PartialView("Ventas/_TrackingPedido", pedidos);
        }
        public PartialViewResult ConsultaCancelados()
        {
            codigo_sucursal = (string)Session["codigo_sucursal"];
            var pedidos = from p in db.C_pedidos
                          where (p.codigo_sucursal == codigo_sucursal && (p.id_tracking_status==6 || p.id_tracking_status==7 || p.id_tracking_status == 8 || p.id_tracking_status == 9)) 
                          select p;

            return PartialView("Ventas/_PedidosCancelados", pedidos);
        }
        public PartialViewResult ConsultaEspeciales()
        {
            codigo_sucursal = (string)Session["codigo_sucursal"];
            var pedidos = from p in db.C_pedidos
                          where (p.codigo_sucursal == codigo_sucursal && p.id_pedido_tipo==3 && p.id_tracking_status!=6)
                          select p;

            return PartialView("Ventas/_PedidosEspeciales", pedidos);
        }
        public PartialViewResult ConsultaTrackingPedidos1(int status)
        {
            codigo_sucursal = (string)Session["codigo_sucursal"];
            ViewBag.status = status;
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == codigo_sucursal
                          select p;
            foreach (var item in pedidos)
            {
                DateTime date1 = DateTime.Now;
                DateTime date2 = Convert.ToDateTime(item.fecha_pedido);
                item.tiempo_pedido= date1.Subtract(date2);
            }
          
            return PartialView("Ventas/_TrackingPedido1", pedidos);
        }
        public PartialViewResult BuscarRepartidor(string gaffette)
        {
            var repartidores = from e in db.C_empleados
                               where e.no_gaffete == gaffette
                               select e;

            return PartialView("Ventas/_AsignarRepartidor", repartidores);
        }
        public PartialViewResult ConsultaPedido(int id_pedido)
        {
            var pedidos = from p in db.C_pedidos 
                          where p.id_pedido==id_pedido
                          select p;
            return PartialView("Ventas/_DetallePedido", pedidos);
        }
        public PartialViewResult ConsultaPedidoProductos(int id_pedido)
        {
            var pedido_prods = from pp in db.C_pedidos_d
                          where pp.id_pedido == id_pedido
                          select pp;
            return PartialView("Ventas/_DetallePedidoProductos", pedido_prods);
        }
        public void AsignarRepartidor(int id_empleados, int id_pedido)
        {
            C_pedidos_empleados c_pedidos_empleados = new C_pedidos_empleados();
            c_pedidos_empleados.id_empleado = id_empleados;
            c_pedidos_empleados.id_pedido = id_pedido;
            c_pedidos_empleados.status = true;
            c_pedidos_empleados.entrada_salida = false;
            c_pedidos_empleados.fecha = DateTime.Now;
            db.Entry(c_pedidos_empleados).State = EntityState.Added;
            db.SaveChanges();
        }

        public PartialViewResult ConsultaColonias(string CP)
        {
            IQueryable<C_colonias_mx> colonias;
            if (CP == "")
            {
                colonias = from c in db.C_colonias_mx
                           where c.codigo_sucursal!=""
                           select c;
            }
            else
            {
                 colonias = from c in db.C_colonias_mx
                            where (c.d_codigo==CP && c.codigo_sucursal!=null)
                            select c;
            }
          
           return PartialView("Ventas/_Colonias", colonias);
        }
        public PartialViewResult ConsultaSucursales()
        {
            var sucursales = from s in db.C_sucursales
                       where  s.activo==true //FALTA BUSCAR QUE CORRESPONDA A LA MARCA
                       select s;

            return PartialView("Ventas/_Sucursales", sucursales);
        }
        public PartialViewResult ConsultaDesvioSucursales()
        {
            PEDIDOSController pc = new PEDIDOSController();
            int id_marca = (int)Session["id_marca"];
            var sucursales = from s in db.C_sucursales
                             join sm in db.C_sucursales_marcas on s.codigo_sucursal equals sm.codigo_sucursal
                             where  sm.id_marca == id_marca
                             select s;

            return PartialView("Ventas/_DesviosSucursales", sucursales);
        }

        public PartialViewResult ValidarCupon(string cupon)
        {
            codigo_sucursal = (string)Session["codigo_sucursal"];
            var promocion = from gpp in db.C_grupo_productos_prods
                            join gpc in db.C_grupo_productos_codigos on gpp.C_grupo_productos_d.C_grupo_productos_g.id_grupo_productos equals gpc.id_grupo_productos
                            join gps in db.C_grupo_productos_sucursales on gpp.C_grupo_productos_d.C_grupo_productos_g.id_grupo_productos equals gps.id_grupo_productos
                            where gpp.C_grupo_productos_d.C_grupo_productos_g.fecha_inicio <= DateTime.Now
                                && gpp.C_grupo_productos_d.C_grupo_productos_g.fecha_final >= DateTime.Now
                                && gpp.C_grupo_productos_d.C_grupo_productos_g.requiere_codigo == true
                                && gpp.C_grupo_productos_d.C_grupo_productos_g.status == true
                                && gps.codigo_sucursal == codigo_sucursal
                                && gpc.saldo>0
                                && gpc.codigo==cupon
                            select gpp;
            if (Session["Carrito"] == null)
            {
                compras = new List<CarritoItem>();
            }
            else
            {
                compras = (List<CarritoItem>)Session["Carrito"];
            }
            if (promocion.Count() > 0)
            {
               foreach(var item1 in promocion)
                {
                    foreach(var item2 in Session["Carrito"] as List<CarritoItem>)
                    {
                        if (item1.sku_producto == item2.Sku)
                        {
                            ValidarPromocion(item2.Producto,item2.Costo,item2.Sku,item2.Promocion,item2.Prom_pos,item2.Id_promocion,true);
                        }
                    }
                }
            }
            Session["Carrito"] = compras;
            return PartialView("Ventas/_AgregaCarrito");


        }
       

    }
}