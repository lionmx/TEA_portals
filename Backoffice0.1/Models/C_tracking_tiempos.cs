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
    
    public partial class C_tracking_tiempos
    {
        public int C_Id_tracking_tiempos { get; set; }
        public Nullable<int> C_id_tracking_status { get; set; }
        public Nullable<decimal> minutos { get; set; }
        public Nullable<decimal> segundos { get; set; }
        public string Notas { get; set; }
        public Nullable<bool> estatus { get; set; }
    
        public virtual C_tracking_status C_tracking_status { get; set; }
    }
}
