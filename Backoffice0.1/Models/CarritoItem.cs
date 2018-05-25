using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backoffice0._1.Models
{
    public class CarritoItem
    {
        private string _producto;
        private int _cantidad;

        public CarritoItem()
        {

        }

        public CarritoItem(string producto, int cantidad)
        {
            this._producto = producto;
            this._cantidad = cantidad;

        }

        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public string Producto { get => _producto; set => _producto = value; }
    }
}