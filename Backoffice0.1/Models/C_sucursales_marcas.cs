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
    
    public partial class C_sucursales_marcas
    {
        public int id_sucursal_marcas { get; set; }
        public string codigo_sucursal { get; set; }
        public Nullable<int> id_marca { get; set; }
    
        public virtual C_marcas_g C_marcas_g { get; set; }
        public virtual C_sucursales C_sucursales { get; set; }
    }
}
