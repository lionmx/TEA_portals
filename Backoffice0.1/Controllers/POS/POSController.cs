using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backoffice0._1.Controllers
{
    public class POSController : Controller
    {
        // GET: POS
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();

        List<CarritoItem> compras;
        List<CarritoComboItem> carrito_combo;
        List<int> productos_promocion = new List<int>();
        List<int> detalles_combo;
        int prod_ele_det;
        int index_carrito;
        int index_detalle_combo;
        int cant_promociones;
        int ultima_promo;
        float total_promo;
        float desc_promo;
        string nombre_promocion;
        CarritoItem promocion_remover;
        public ActionResult Index()
        {
            var collection = db.C_cp_mexico.Select(m => m.d_codigo).Distinct().ToList();
            ViewBag.CP = new SelectList(collection, "", "");

            var collection2 = db.C_cp_mexico.Select(m => m.d_asenta).Distinct().ToList();
            ViewBag.Colonias = new SelectList(collection2, "", "");

            /*#region Viewbags 

            //obtiene los permisos de cada servicio/modulo para el usuario loggeado           
            List<int> permisosLista = new List<int>();
            string loggedId = Session["LoggedId"].ToString();
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
            #endregion*/
            ViewBag.tipo_usuario = 2;
            return View("Ventas/Index");
        }

        public ActionResult AgregaCarrito(string nombre, float costo, string sku, bool promocion, bool prom_pos, int id_promocion)
        {
            if (Session["Carrito"] == null)
            { compras = new List<CarritoItem>();
                index_carrito = 1;
            }
            else
            {
                compras = (List<CarritoItem>)Session["Carrito"];
                index_carrito = compras.Max(x => x.Index) + 1;
            }

            compras.Add(new CarritoItem(nombre, costo, sku, promocion, index_carrito, prom_pos, id_promocion,true));
            Session["Carrito"] = compras;
            ValidarPromocion(nombre, costo, sku, promocion, prom_pos, id_promocion);

            return PartialView("Ventas/_AgregaCarrito");
        }

        public ActionResult RemueveCarrito(string sku, int index_carrito)
        {
            compras = (List<CarritoItem>)Session["Carrito"];
            var item = compras.FirstOrDefault(x => x.Sku == sku && x.Index == index_carrito);
            promocion_remover = compras.Find(x => x.Id_promocion == item.Id_promocion);
            compras.Remove(item);
            var id_promo = promocion_remover.Id_promocion;
            if (id_promo != 0)
            {
                promocion_rem(id_promo);
            }
            if(compras.Count()==0)
            {
                Session["Carrito"] = null;
            }
            return PartialView("Ventas/_AgregaCarrito");
        }
        public void promocion_rem(int id_promo)
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
                ValidarPromocion(item4.Producto, item4.Costo, item4.Sku, item4.Promocion, item4.Prom_pos, item4.Id_promocion);
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
            return PartialView("Ventas/_Clientes",null);
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
            if (id_cla == 0)
            {
                id_cla = 1;
            }
            var productos = from p in db.C_productos_cat
                            join ps in db.C_productos_sucursal on p.sku equals ps.sku_producto
                            join pp in db.C_productos_precios on p.sku equals pp.sku_producto
                            where (p.id_producto_clasificacion == id_cla && ps.codigo_sucursal == "SUC001" && pp.id_zona == 1)
                            select new Productos { c_productos_cat = p, c_productos_sucursal = ps, c_productos_precios = pp };

            return PartialView("Ventas/_Productos", productos);

        }
        public PartialViewResult ConsultaBanners()
        {
            return PartialView("Ventas/_Banners");
        }

        public void ValidarPromocion(string nombre, float costo, string sku, bool promocion, bool prom_pos, int id_promocion)
        {
            var bandera = 0;
            total_promo = 0;
            int id_grupo_aplicacion = 0;

            /*Validar promociones*/
            var promocion_producto = from gpp in db.C_grupo_productos_prods
                                     join gps in db.C_grupo_productos_sucursales on gpp.C_grupo_productos_d.id_grupo_productos equals gps.id_grupo_productos
                                     where gpp.sku_producto == sku && gpp.C_grupo_productos_d.C_grupo_productos_g.status == true
                                     && gpp.C_grupo_productos_d.C_grupo_productos_g.fecha_inicio <= DateTime.Now
                                     && gpp.C_grupo_productos_d.C_grupo_productos_g.fecha_final >= DateTime.Now
                                     && gps.codigo_sucursal == "SUC001" && gpp.C_grupo_productos_d.C_grupo_productos_g.id_grupo_producto_tipo == 1
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
                                            if (contador == detalles.Count())
                                            {
                                                desc_promo = (float)item2.C_grupo_productos_d.C_grupo_productos_g.descuento;
                                                cant_promociones++;
                                                ultima_promo = compras.Max(x => x.Id_promocion);
                                                nombre_promocion = item2.C_grupo_productos_d.C_grupo_productos_g.nombre;
                                                id_grupo_aplicacion = (int)item2.C_grupo_productos_d.C_grupo_productos_g.id_grupo_aplica_operacion;

                                                foreach (var item7 in productos_promocion)
                                                {
                                                    compras.Find(x => x.Index == item7).Promocion = true;
                                                    compras.Find(x => x.Index == item7).Id_promocion = ultima_promo + 1;
                                                    total_promo = total_promo + compras.Find(x => x.Index == item7).Costo;
                                                    if (id_grupo_aplicacion == 3)
                                                    {
                                                        compras.Find(x => x.Index == item7).Costo = 0;
                                                    }
                                                    compras.Find(x => x.Index == item7).Costo = 0;
                                                }
                                                if (id_grupo_aplicacion == 2)
                                                {
                                                    total_promo = -(total_promo * ((100 - desc_promo) / 100));
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
                compras.Add(new CarritoItem(nombre_promocion, total_promo, "", true, 0, false, ultima_promo + 2,true));
            }
            compras.Sort((x, y) => x.Id_promocion.CompareTo(y.Id_promocion));
        }

        //Configuracion de combos

        public PartialViewResult ConsultaCombos(int id_subclase)
        {
            var combos = from gpp in db.C_grupo_productos_g
                         join gps in db.C_grupo_productos_sucursales on gpp.id_grupo_productos equals gps.id_grupo_productos
                         where gpp.id_grupo_producto_tipo == 2 && gpp.id_grupo_producto_subclase == id_subclase && gpp.status == true
                         && gpp.fecha_inicio <= DateTime.Now
                         && gpp.fecha_final >= DateTime.Now
                         && gps.codigo_sucursal == "SUC001"
                         select gpp;
            return PartialView("Ventas/_Combos", combos);
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

        public PartialViewResult ConsultaCombosProductos(string sku, string producto, float costo)
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
                            select new Productos { c_productos_cat = p,c_productos_precios = pp, c_grupo_productos_prods = gpp };
           
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
                
                carrito_combo.Add(new CarritoComboItem(producto,1, sku,costo));
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
                                        select new Productos { c_productos_cat = p, c_productos_precios = pp,c_grupo_productos_prods=gpp };
                    }
                }
            }
            return PartialView("Ventas/_ConfiguraCombo",productos);
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
        public void ProductoCuenta(int index)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["Carrito"];
            compras.Find(x => x.Index == index).Cuenta= false;
            Session["Carrito"] = compras;

        }
        public PartialViewResult BuscarClienteTelefono(string telefono)
        {
            var cliente = db.C_clientes.ToList();

            return PartialView("Ventas/_Clientes");
        }
        public PartialViewResult ConsultaTrackingPedidos()
        {
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == "SUC001"
                          select p;

            return PartialView("Ventas/_TrackingPedido", pedidos);
        }
        public PartialViewResult ConsultaTrackingPedidos1(int status)
        {
            ViewBag.status = status;
            var pedidos = from p in db.C_pedidos
                          where p.codigo_sucursal == "SUC001"
                          select p;

            return PartialView("Ventas/_TrackingPedido1", pedidos);
        }
        public PartialViewResult BuscarRepartidor(string gaffette)
        {
            var repartidores = from e in db.C_empleados
                               where e.gaffete == gaffette 
                               select e;

            return PartialView("Ventas/_AsignarRepartidor", repartidores);
        }
        public void AsignarRepartidor(int id_empleados, int id_pedido)
        {
                C_pedidos_empleados c_pedidos_empleados = new C_pedidos_empleados();
                c_pedidos_empleados.id_empleado = id_empleados;
                c_pedidos_empleados.id_pedido = id_pedido;
                c_pedidos_empleados.status = true;
                db.Entry(c_pedidos_empleados).State = EntityState.Added;
                db.SaveChanges();
        }
       


    }
}