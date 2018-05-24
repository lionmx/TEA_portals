using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
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
        List<int> productos_promocion = new List<int>();
        int index_carrito;
       
        public ActionResult Index()
        {
            /* #region Viewbags 

             CS_permisos_asignados ViewModel = new CS_permisos_asignados();
             List<string> listaP = new List<string>();

             string user = Session["LoggedUser"].ToString();
             string loggedId = Session["LoggedId"].ToString();
             var id = from us in db.CS_usuarios
                      where us.NOMBRE.Equals(user)
                      select us;

             foreach (var i in id)
             {
                 user = i.ID_USUARIO;
             }
             for (int i = 00; i <= 8; i++)
             {
                 var mod1 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "0" + i && a.ID_PERMISO == "07").FirstOrDefault();
                 var perfil = db.CS_usuarios.Where(a => a.ID_USUARIO.Equals(loggedId)).FirstOrDefault();
                 if (mod1 != null)
                 {
                     listaP.Add(mod1.ID_MODULO);
                     listaP.Add(mod1.ID_PERMISO);
                     ViewBag.perfil = perfil.ID_PERFIL;

                 }
             }
             ViewBag.data = listaP;
             List<string> listaPA2 = new List<string>();
             for (int i = 0; i <= 8; i++)
             {
                 var mod2 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "'0" + i + "'" && a.ID_PERMISO.Equals("08")).FirstOrDefault();
                 if (mod2 != null)
                 {
                     listaPA2.Add(mod2.ID_MODULO);
                     listaPA2.Add(mod2.ID_PERMISO);
                 }
             }
             ViewBag.data2 = listaPA2;

             #endregion*/
            return View("Ventas/Index");
        }
       
        public ActionResult AgregaCarrito(string nombre, float costo, string sku, bool promocion, bool prom_pos)
        {
            if (Session["Carrito"] == null)
            {
                compras = new List<CarritoItem>();
            }
            else
            {
                compras = (List<CarritoItem>)Session["Carrito"];
            }
            index_carrito=compras.Count() + 1;
            compras.Add(new CarritoItem(nombre, costo, sku, promocion,index_carrito,prom_pos));
            Session["Carrito"] = compras;

            /*Validar promociones*/
            var bandera = 0;
            if (Session["Carrito"] == null)
            {
                compras = new List<CarritoItem>();
            }
            else
            {
                compras = (List<CarritoItem>)Session["Carrito"];
            }

            var promocion_producto = from gpp in db.C_grupo_productos_prods
                                     where gpp.sku_producto == sku && gpp.C_grupo_productos_d.C_grupo_productos_g.status == true
                                     && gpp.C_grupo_productos_d.C_grupo_productos_g.fecha_inicio <= DateTime.Now
                                     && gpp.C_grupo_productos_d.C_grupo_productos_g.fecha_final >= DateTime.Now
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
                        var promocion_1 = from gpp in db.C_grupo_productos_prods
                                          where gpp.id_grupo_productos_det
                                          == item3.id_grupo_productos_det
                                          select gpp;
                        if (promocion_1.Count() > 0)
                        {
                            foreach (var item4 in compras)
                            {
                                if ( item4.Promocion==false && agregados<item3.no_prod_seleccion)
                                {
                                    foreach (var item5 in promocion_1)
                                    {
                                        if (item4.Sku == item5.sku_producto && item4.Prom_pos == false )
                                        {
                                            compras.Find(x => x.Index == item4.Index).Prom_pos = true;
                                            productos_promocion.Add(item4.Index);
                                            bandera = 1;
                                            agregados++;
                                            var count_det = detalles.Count();
                                            if (contador == detalles.Count())
                                            {
                                                foreach (var item7 in productos_promocion)
                                                {
                                                    compras.Find(x => x.Index == item7).Promocion = true;
                                                    compras.Find(x => x.Index == item7).Producto = compras.Find(x => x.Index == item7).Producto+" promocion validada " +item2.descripcion;
                                                    
                                                }
                                                productos_promocion.Clear();
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (bandera == 0)
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



            return PartialView("Ventas/_AgregaCarrito");
        }

        public ActionResult RemueveCarrito(string sku)
        {
            compras = (List<CarritoItem>)Session["Carrito"];
            var item = compras.FirstOrDefault(x => x.Sku == sku);
            compras.Remove(item);
            return PartialView("Ventas/_AgregaCarrito");
        }

        public ActionResult VisualizaCarrito()
        {
            compras = (List<CarritoItem>)Session["Carrito"];
            return PartialView("Ventas/_VisualizaCarrito");
        }

        public PartialViewResult ConsultaCategorias()
        {
            return PartialView("Ventas/_Categorias", db.C_producto_tipo.ToList());
        }

        public PartialViewResult TrackingPedido()
        {
            return PartialView("Ventas/_TrackingPedido");
        }

        public PartialViewResult Clientes()
        {
            return PartialView("Ventas/_Clientes");
        }

        public PartialViewResult BotonesEspeciales()
        {
            return PartialView("Ventas/_BotonesEspeciales");
        }
    
        public PartialViewResult ConsultaSubCategorias(int id_tipo_producto)
        {
            var subcategorias = from sc in db.C_producto_clasificacion
                                where sc.id_tipo_producto == id_tipo_producto
                                select sc;

            return PartialView("Ventas/_SubCategorias", subcategorias);
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
        public PartialViewResult ConsultaPromocion(string nombre, float costo, string sku, bool promocion,int index_carrito,bool prom_pos)
        {
           
            compras.ToList();
            return PartialView("Ventas/_AgregaCarrito");
        }
    }
}
