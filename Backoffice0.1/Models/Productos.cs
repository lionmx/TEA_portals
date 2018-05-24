using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backoffice0._1.Models
{
    public class Productos
    {
        public C_productos_cat c_productos_cat  { get; set; }
        public C_productos_sucursal c_productos_sucursal { get; set; }
        public C_productos_precios c_productos_precios{ get; set; }
    }
}