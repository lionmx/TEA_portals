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
    
    public partial class C_grupo_productos_codigos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_grupo_productos_codigos()
        {
            this.C_grupo_productos_codigos_movs = new HashSet<C_grupo_productos_codigos_movs>();
        }
    
        public int id_grupo_productos_codigo { get; set; }
        public Nullable<int> id_grupo_productos { get; set; }
        public string codigo { get; set; }
        public Nullable<int> no_codigos_autorizados { get; set; }
        public Nullable<System.DateTime> vigente_desde { get; set; }
        public Nullable<System.DateTime> vigente_hasta { get; set; }
        public Nullable<int> id_destino_social { get; set; }
        public Nullable<int> saldo { get; set; }
        public string descripcion { get; set; }
        public string reglas { get; set; }
        public Nullable<bool> activo { get; set; }
    
        public virtual C_grupo_productos_g C_grupo_productos_g { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_grupo_productos_codigos_movs> C_grupo_productos_codigos_movs { get; set; }
    }
}
