using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Backoffice0._1.Models;


namespace Backoffice0._1.Controllers.POS
{

    public static class TrackingStatus
    {
        private static DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();


        public static List<DeliveryModel> estadoCocina()
        {
            List<DeliveryModel> listaPedidosSuc = new List<DeliveryModel>();
            var suc = db.C_sucursales.SqlQuery("SELECT * FROM C_SUCURSALES").ToList();
            foreach (var id in suc)
            {
                var query = db.Database.SqlQuery<DeliveryModel>("SELECT COUNT(ID_PEDIDO) AS totalCocina FROM C_PEDIDOS WHERE ID_TRACKING_STATUS = 2 AND CODIGO_SUCURSAL='" + id.codigo_sucursal + "'");

                if (query != null)
                {
                    listaPedidosSuc.Add(query.FirstOrDefault());
                }
            }

            return listaPedidosSuc;
        }
        public static List<DeliveryModel> estadoRecibido()
        {
            List<DeliveryModel> listaPedidosSuc = new List<DeliveryModel>();
            var suc = db.C_sucursales.SqlQuery("SELECT * FROM C_SUCURSALES").ToList();
            foreach (var id in suc)
            {
                var query = db.Database.SqlQuery<DeliveryModel>("SELECT COUNT(ID_PEDIDO) AS totalRecibido FROM C_PEDIDOS WHERE ID_TRACKING_STATUS = 1 AND CODIGO_SUCURSAL='" + id.codigo_sucursal + "'");

                if (query != null)
                {
                    listaPedidosSuc.Add(query.FirstOrDefault());
                }
            }

            return listaPedidosSuc;
        }
        public static List<DeliveryModel> estadoPorAsignar()
        {
            List<DeliveryModel> listaPedidosSuc = new List<DeliveryModel>();
            var suc = db.C_sucursales.SqlQuery("SELECT * FROM C_SUCURSALES").ToList();
            foreach (var id in suc)
            {
                var query = db.Database.SqlQuery<DeliveryModel>("SELECT COUNT(ID_PEDIDO) AS totalPorAsignar FROM C_PEDIDOS WHERE ID_TRACKING_STATUS = 3 AND CODIGO_SUCURSAL='" + id.codigo_sucursal + "'");

                if (query != null)
                {
                    listaPedidosSuc.Add(query.FirstOrDefault());
                }
            }

            return listaPedidosSuc;
        }
        public static List<DeliveryModel> estadoEntregando()
        {
            List<DeliveryModel> listaPedidosSuc = new List<DeliveryModel>();
            var suc = db.C_sucursales.SqlQuery("SELECT * FROM C_SUCURSALES").ToList();
            foreach (var id in suc)
            {
                var query = db.Database.SqlQuery<DeliveryModel>("SELECT COUNT(ID_PEDIDO) AS totalEntregando FROM C_PEDIDOS WHERE ID_TRACKING_STATUS = 4 AND CODIGO_SUCURSAL='" + id.codigo_sucursal + "'");

                if (query != null)
                {
                    listaPedidosSuc.Add(query.FirstOrDefault());
                }
            }

            return listaPedidosSuc;
        }
        public static List<DeliveryModel> totalRepaSuc()
        {
            List<DeliveryModel> listaRepaSuc = new List<DeliveryModel>();
            var suc = db.C_sucursales.SqlQuery("SELECT * FROM C_SUCURSALES").ToList();
            foreach (var id in suc)
            {
                var query = db.Database.SqlQuery<DeliveryModel>("SELECT COUNT(c.ID_USUARIO_CORPORATIVO) AS totalRepaSuc from c_usuarios_corporativo c join C_usuarios_sucursales s on c.id_usuario_corporativo = s.id_usuario_corporativo where c.id_rol = 4 and  s.codigo_sucursal = '" + id.codigo_sucursal + "'");

                if (query != null)
                {
                    listaRepaSuc.Add(query.FirstOrDefault());
                }
            }
            return listaRepaSuc;
        }

        public static List<DeliveryModel> totalRepaEntregando()
        {
            List<DeliveryModel> listaRepaEntregando = new List<DeliveryModel>();
            var suc = db.C_sucursales.SqlQuery("SELECT * FROM C_SUCURSALES").ToList();
            foreach (var n in suc)
            {
                var obj = db.Database.SqlQuery<DeliveryModel>(" select count(u.id_pedido) as totalRepaEntregando from C_pedidos u join c_pedidos_empleados p on u.id_pedido = p.id_pedido where p.entrada_salida = 1 and  u.codigo_sucursal = '" + n.codigo_sucursal + "'");
                if (obj != null)
                {
                    if (obj.FirstOrDefault() == null)
                    {
                        obj.FirstOrDefault().totalRepaEntregando = 0;
                        listaRepaEntregando.Add(obj.FirstOrDefault());
                    }
                    else
                    {
                        listaRepaEntregando.Add(obj.FirstOrDefault());
                    }
             
                }

            }
            return listaRepaEntregando;
            
        }
        public static List<DeliveryModel> estadoEntregados()
        {
            List<DeliveryModel> listaPedidosSuc = new List<DeliveryModel>();
            var suc = db.C_sucursales.SqlQuery("SELECT * FROM C_SUCURSALES").ToList();
            foreach (var id in suc)
            {
                var query = db.Database.SqlQuery<DeliveryModel>("SELECT COUNT(ID_PEDIDO) AS totalEntregados FROM C_PEDIDOS WHERE ID_TRACKING_STATUS = 5 AND CODIGO_SUCURSAL='" + id.codigo_sucursal + "'");

                if (query != null)
                {
                    listaPedidosSuc.Add(query.FirstOrDefault());
                }
            }

            return listaPedidosSuc;
        }

        //public static List<DeliveryModel> repartidoresEnSucursal(C_usuarios_sucursales m)
        //{
        //    List<DeliveryModel> listaRepasSuc = new List<DeliveryModel>();
        //    var obj = from c in db.C_usuarios_corporativo
        //              join u in db.C_usuarios_sucursales on c.id_usuario_corporativo equals u.id_usuario_corporativo
        //              where c.id_rol == 4 && u.codigo_sucursal == m.codigo_sucursal
        //              select c;

        //    List<string> reparNombre = new List<string>();
        //    if (obj != null)
        //    {

        //        foreach (var n in obj)
        //        {

        //            var nombreRepartidor = from r in db.C_empleados
        //                                   where r.id_empleado == n.id_empleado
        //                                   select r.nombres + " " + r.apellido_paterno + " " + r.apellido_materno;

        //            listaRepasSuc.Add(nombreRepartidor.FirstOrDefault());
        //        }
        //        //ViewBag.repartidorEnSucursal = reparNombre;
        //        return listaRepasSuc;

        //    }


    
    }
}