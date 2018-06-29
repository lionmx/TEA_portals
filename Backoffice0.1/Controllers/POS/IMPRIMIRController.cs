using Backoffice0._1.Models;
using ImprimirControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Backoffice0._1.Controllers.POS
{
    public class IMPRIMIRController : Controller
    {
        // GET: Imprimir
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public void Imprimir(int id_pedido, int id_venta)
        {
          
            Imprimir impresion = new Imprimir();
            string[] DateArray = DateTime.Now.ToShortDateString().Split('/');
            //Datos de Factura
            var tipo_venta = "";
            IQueryable<C_ventas_d> productos_venta;
            var pedido = from p in db.C_pedidos
                         where p.id_pedido == id_pedido 
                            select p;
            foreach (var item in pedido)
            {
                //Informacion de la marca
              
                impresion.AddDatos(item.C_marcas_g.nombre_marca.ToString(), "25", "10");
                impresion.AddDatos("Matriz: ", "5", "13");
               /* var marca_direcccion = from md in db.C_marcas_direcciones
                                       where md.id_marca == item.id_marca
                                       select md;
                impresion.AddDatos("Comercial Edujes, S.A de C.V", "5", "16");
                impresion.AddDatos("Libramiento Periférico Torreón. Gomez Km.6.7", "5", "19");
                impresion.AddDatos("CP: 35019, Residencial Hamburgo", "5", "22");
                impresion.AddDatos("Gomez Palacio, Dgo", "5", "25");
                impresion.AddDatos("RFC: CED140401881", "5", "28");*/
                if (item.id_marca==1)
                {
                    impresion.AddDatos("Comercial Edujes, S.A de C.V", "5", "16");
                    impresion.AddDatos("Libramiento Periférico Torreón. Gomez Km.6.7", "5", "19");
                    impresion.AddDatos("CP: 35019, Residencial Hamburgo", "5", "22");
                    impresion.AddDatos("Gomez Palacio, Dgo", "5", "25");
                    impresion.AddDatos("RFC: CED140401881", "5", "28");
                }
                if (item.id_marca == 2)
                {
                    impresion.AddDatos("Comercial Edujes, S.A de C.V", "5", "16");
                    impresion.AddDatos("Libramiento Periférico Torreón. Gomez Km.6.7", "5", "19");
                    impresion.AddDatos("CP: 35019, Residencial Hamburgo", "5", "22");
                    impresion.AddDatos("Gomez Palacio, Dgo", "5", "25");
                    impresion.AddDatos("RFC: CED140401881", "5", "28");
                    /*impresion.AddDatos("Razon social", "5", "16");
                    impresion.AddDatos("Calle", "5", "19");
                    impresion.AddDatos("CP: , Colonia", "5", "21");
                    impresion.AddDatos("Ciudad, Estado", "5", "24");
                    impresion.AddDatos("RFC: 0000000000000", "5", "27");*/
                }

                // informacion de la sucursal
                impresion.AddDatos("Sucursal: "+item.C_sucursales.nombre, "5", "30");
                impresion.AddDatos("Direccion: " + item.C_sucursales.nombre, "5", "33");
                // informacion del pedido
                impresion.AddDatos("# PEDIDO: " + item.id_pedido.ToString(), "5", "60");
                impresion.AddDatos("Fecha - Hora : " + item.fecha_pedido.ToString(), "5", "63");
                if (item.id_pedido_tipo == 1)
                   {
                      tipo_venta = "MOSTRADOR";
                   }
                if (item.id_pedido_tipo == 2)
                   {
                      tipo_venta = "CALL CENTER";

                   }
                impresion.AddDatos("Tipo Venta: "+tipo_venta, "5", "66");
                impresion.AddDatos("Cajero: " , "5", "69");


            }

            impresion.AddDatos("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _", "5", "72");
            impresion.AddDatos("Cant", "5", "75");
            impresion.AddDatos("Descripcion", "18", "75");
            impresion.AddDatos("Importe", "60", "75");
            impresion.AddDatos("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _", "5", "78");
            var espacio = 81;
            if (id_venta == 0)
            {
                 productos_venta = from v in db.C_ventas_d
                            where v.C_ventas_g.id_pedido == id_pedido
                            select v;
            }
            else
            {
                 productos_venta = from v in db.C_ventas_d
                                      where v.C_ventas_g.id_venta_g == id_venta
                                      select v;
            }
            foreach (var item2 in productos_venta)
            {
                    impresion.AddDatos("1", "5", espacio.ToString());
                    impresion.AddDatos(item2.C_productos_cat.nombre, "18", espacio.ToString());
                    impresion.AddDatos(item2.precio.ToString(), "60", espacio.ToString());
                    espacio += 3;
            }
            foreach (var item in pedido)
            {
                //Informacion de la marca

                impresion.AddDatos("Total= "+ Math.Round((float)item.monto, 2).ToString(), "30",(espacio+3).ToString());
                impresion.AddDatos("Recibe= "+ Math.Round((float)item.pago_recibido, 2).ToString(), "30", (espacio + 6).ToString());
                impresion.AddDatos("Cambio= " + Math.Round((float)(item.pago_recibido - item.monto), 2).ToString(), "30", (espacio + 9).ToString());

            }
            impresion.AddDatos("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _", "5", (espacio + 12).ToString());
            impresion.AddDatos("Servicio a domicilio: 7-13-31-31" , "10", (espacio + 15).ToString());

            impresion.PrintFactura("EPSON TM-T20II Receipt");
        }
    }
}