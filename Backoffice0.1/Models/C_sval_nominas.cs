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
    
    public partial class C_sval_nominas
    {
        public int id_sval_nomina { get; set; }
        public string codigo_usuario { get; set; }
        public string codigo_empresa { get; set; }
        public string codigo_sucursal { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string folio_nomina { get; set; }
        public string no_comprobante { get; set; }
        public Nullable<decimal> importe_total { get; set; }
    }
}
