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
    
    public partial class C_pos_caja_cambioturno_insumos
    {
        public long id_caja_cti { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string hora { get; set; }
        public string insumo_id { get; set; }
        public string insumo_nombre { get; set; }
        public Nullable<int> unidad_medida { get; set; }
        public Nullable<decimal> costounidad { get; set; }
        public Nullable<decimal> exist_sistema { get; set; }
        public decimal existe_fisica { get; set; }
        public Nullable<decimal> diferencia { get; set; }
        public Nullable<decimal> diferencia_costo { get; set; }
        public Nullable<int> id_terminal { get; set; }
    }
}
