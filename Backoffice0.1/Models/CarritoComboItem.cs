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
        public CarritoComboItem()
        {

        }

        public CarritoComboItem(string producto, int cantidad, string sku,float costo)
        {
            this._producto = producto;
            this._cantidad = cantidad;
            this._sku = sku;
            this._costo = costo;
        }
        public string Sku { get => _sku; set => _sku = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public string Producto { get => _producto; set => _producto = value; }
        public float Costo { get => _costo; set => _costo = value; }

    }
}