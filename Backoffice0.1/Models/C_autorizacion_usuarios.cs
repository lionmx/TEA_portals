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
    
    public partial class C_autorizacion_usuarios
    {
        public int id_autoriza_usuarios { get; set; }
        public Nullable<int> id_usuario { get; set; }
        public Nullable<int> id_autoriza_tipo { get; set; }
    
        public virtual C_usuarios_corporativo C_usuarios_corporativo { get; set; }
    }
}
