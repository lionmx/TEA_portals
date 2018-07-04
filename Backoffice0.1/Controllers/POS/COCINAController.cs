using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace Backoffice0._1.Controllers.POS
{
    public class COCINAController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        SumaTotal model = new SumaTotal();
        int i = 0;
        // GET: COCINA
        public ActionResult Index()
        {
            #region Viewbags 

            //obtiene los permisos de cada servicio/modulo para el usuario loggeado           
            List<int> permisosLista = new List<int>();
            int loggedId = Convert.ToInt32(Session["LoggedId"]);
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
            pedidos();
             if (Session["t"] != null)
            
          
            {
                string newtime = Session["t"].ToString(); 
                
            } 
            return View("/Views/POS/Cocina.cshtml", model);
                
        }

       

        private void timer_tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void pedidos()
        {
            string[] arr = new string[] { };


            string pedidos = string.Empty;
            string sku = string.Empty;
            
            var query = db.C_pedidos.Where(a => a.id_tracking_status == 2 && a.fecha_entrega >= DateTime.Today).ToList();
            foreach (var n in query)
            {
                if (n.id_pedido_tipo != null)
                {
                    if (n.id_cliente != null)
                    { 
                    var detallePedido = db.C_pedidos_d.Where(a => a.id_pedido == n.id_pedido);
                   
                        var cliente = db.C_clientes.Where(a => a.id_cliente == n.id_cliente);

                    
                        foreach (var d in detallePedido)
                        {
                            var con = db.C_productos_cat.Where(a => a.sku.Equals(d.sku_producto));
                            foreach (var c in con)
                            {
                                sku = sku+ "1 Pza  " + c.nombre + "\n";
                            } 
                        }
                        var tipoPedido = db.C_pedidos_tipo.Where(a => a.id_pedido_tipo == n.id_pedido_tipo);
                        pedidos = pedidos + "#" + n.id_pedido.ToString() + " Cliente: " + cliente.FirstOrDefault().nombre + "\n  \n" + sku + ",";
                        sku = string.Empty;
                    }
                }
            }
            ViewBag.pedidos = pedidos;
        }
        [HttpPost]
        public ActionResult pedidoCompletado(FormCollection form)
        {
            var pedido = form["privacy_textarea"];
            var pedido2 = form["privacy_textarea"];
            var pedido3 = form["privacy_textarea"];

            if (pedido != null)
            {
                string cadena = pedido.Substring(0, pedido.IndexOf("#"));
                string cadena2 = pedido.Substring(cadena.Length + 1, 4);
                db.Database.ExecuteSqlCommand("UPDATE C_PEDIDOS SET id_tracking_status = 3 where id_pedido = '" + cadena2 + "'");
                pedidos();
            }
            if (pedido2 != null)
            {
                string cadena = pedido2.Substring(0, pedido2.IndexOf("#"));
                string cadena2 = pedido2.Substring(cadena.Length + 1, 4);
                db.Database.ExecuteSqlCommand("UPDATE C_PEDIDOS SET id_tracking_status = 3 where id_pedido = '" + cadena2 + "'");
                pedidos();
            }

            if (pedido3 != null)
            {
                string cadena = pedido3.Substring(0, pedido3.IndexOf("#"));
                string cadena2 = pedido3.Substring(cadena.Length + 1, 4);
                db.Database.ExecuteSqlCommand("UPDATE C_PEDIDOS SET id_tracking_status = 3 where id_pedido = '" + cadena2 + "'");
                pedidos();
            }
            return View("/Views/POS/Cocina.cshtml"); 
        }
    }
}