//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Backoffice0._1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class C_productos_precios
    {
        public int id_producto_precio { get; set; }
        public string sku_producto { get; set; }
        public Nullable<decimal> precio_publico { get; set; }
        public Nullable<decimal> costo_directo { get; set; }
        public Nullable<decimal> costo_real { get; set; }
        public Nullable<int> id_zona { get; set; }
        public string cfdi_prodid { get; set; }
    
        public virtual C_zonas_precio C_zonas_precio { get; set; }
    }
}
