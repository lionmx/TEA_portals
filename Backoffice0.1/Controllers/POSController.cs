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
        public ActionResult Index()
        {
            return View();
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
            return PartialView("AgregaCarrito");
        }

        public ActionResult RemueveCarrito (int id)
        {
            compras = (List<CarritoItem>)Session["Carrito"];
            var item = compras.FirstOrDefault(x => x.Producto == "Producto1");
            compras.Remove(item);
            return PartialView("AgregaCarrito");
        }

        public ActionResult ConsultaCategorias()
        {
            var categoria = new Categorias()
            {
                IdCategoria = 1,
                Nombre = "categoria1",
             

            };
            var categoria2 = new Categorias()
            {
                IdCategoria = 2,
                Nombre = "categoria2"

            };
            return PartialView("Categorias",categoria);
        }


    }
}