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
    
    public partial class C_ventas_pagos
    {
        public int id_pago { get; set; }
        public Nullable<int> id_venta { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> id_pago_tipo { get; set; }
    
        public virtual C_pago_tipo C_pago_tipo { get; set; }
        public virtual C_ventas_g C_ventas_g { get; set; }
    }
}
