using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backoffice0._1.Models
{
    public class CarritoComboItem
    {
        private string _sku;
        private string _producto;
        private int _cantidad;
        private float _costo;
        private int _id_producto;
        private int _id_promocion;
        public CarritoComboItem()
        {

        }

        public CarritoComboItem(string producto, int cantidad, string sku, float costo, int id_producto,int id_promocion)
        {
            this._producto = producto;
            this._cantidad = cantidad;
            this._sku = sku;
            this._costo = costo;
            this._id_producto = id_producto;
            this._id_promocion = id_promocion;
        }
        public string Sku { get => _sku; set => _sku = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public string Producto { get => _producto; set => _producto = value; }
        public float Costo { get => _costo; set => _costo = value; }
        public int Id_producto { get => _id_producto; set => _id_producto = value; }
        public int Id_promocion { get => _id_promocion; set => _id_promocion = value; }
    }
}