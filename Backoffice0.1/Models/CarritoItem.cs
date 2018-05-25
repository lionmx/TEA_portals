using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backoffice0._1.Models
{
    public class CarritoItem
    {
        private int _index;
        private int _id_promocion;
        private bool _promocion;
        private bool _prom_pos;
        private string _sku;
        private string _producto;
        private float _costo;
       

        public CarritoItem()
        {

        }

        public CarritoItem(string producto, float cantidad, string sku, bool promocion, int index, bool prom_pos, int id_promocion)
        {
            this._index = index;
            this._producto = producto;
            this._costo = cantidad;
            this._sku = sku;
            this._promocion = promocion;
            this._prom_pos = prom_pos;
            this._id_promocion = id_promocion;
        }

        
        public int Index { get => _index; set => _index = value; }
        public int Id_promocion { get => _id_promocion; set => _id_promocion = value; }
        public bool Promocion { get => _promocion; set => _promocion = value; }
        public bool Prom_pos { get => _prom_pos; set => _prom_pos = value; }
        public string Sku { get => _sku; set => _sku = value; }
        public float Costo { get => _costo; set => _costo = value; }
        public string Producto { get => _producto; set => _producto = value; } 

       
    }
}