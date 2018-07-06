using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backoffice0._1.Controllers.POS
{
    public class COCINA2Controller : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        // GET: COCINA2
        public ActionResult Index()
        {
            var codigo_sucursal = (string)Session["codigo_sucursal"];

            var detalle_pedido = from dp in db.C_pedidos_d
                                 where dp.C_pedidos.codigo_sucursal == codigo_sucursal && dp.C_pedidos.id_tracking_status == 2
                                 select dp;
            return View();
        }

    }
}