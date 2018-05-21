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
                            join p in db.C_productos_cat on ps.id_producto equals p.id_producto
                            where (p.id_producto_clasificacion == id_cla && ps.id_sucursal==1)
                            select p;

            return PartialView("Ventas/_Productos", productos);
        }
        public PartialViewResult ConsultaBanners()
        {
            return PartialView("Ventas/_Banners");
        }

    }
}