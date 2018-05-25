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
        public ActionResult Index()
        {
            #region Viewbags 

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
            #endregion
            return View("Ventas/Index");
        }
        List<CarritoItem> compras;
        public ActionResult AgregaCarrito(int ID)
        {
            if (Session["Carrito"] == null)
            {
                compras = new List<CarritoItem>();
            }
            else
            {
                compras = (List<CarritoItem>)Session["Carrito"];
            }
            compras.Add(new CarritoItem("Producto1", 1));
            Session["Carrito"] = compras;
            return PartialView("Ventas/_AgregaCarrito");
        }

        public ActionResult RemueveCarrito(int id)
        {
            compras = (List<CarritoItem>)Session["Carrito"];
            var item = compras.FirstOrDefault(x => x.Producto == "Producto1");
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
            if(id_cla==0)
            {
                id_cla = 1;
            }
            var productos = from ps in db.C_productos_sucursal
                            join p in db.C_productos_cat on ps.id_productos_sucursal equals p.id_producto
                            where (p.id_producto_clasificacion == id_cla && ps.codigo_sucursal.Equals(1))
                            select p;

            return PartialView("Ventas/_Productos", productos);
        }
        public PartialViewResult ConsultaBanners()
        {
            return PartialView("Ventas/_Banners");
        }

    }
}