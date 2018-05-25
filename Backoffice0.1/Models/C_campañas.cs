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
    
    public partial class C_campañas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_campañas()
        {
            this.C_campaña_medios = new HashSet<C_campaña_medios>();
            this.C_campaña_codigos = new HashSet<C_campaña_codigos>();
        }
    
        public int id_campaña { get; set; }
        public Nullable<int> id_empresa { get; set; }
        public string nombre_campaña { get; set; }
        public string fecha_registro { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
        public Nullable<int> id_campaña_empresa { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> id_tipo_codigo { get; set; }
        public Nullable<int> id_promocion { get; set; }
        public string limite_aplicacion { get; set; }
        public string plantilla_cupon { get; set; }
    
        public virtual C_campaña_empresas C_campaña_empresas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_campaña_medios> C_campaña_medios { get; set; }
        public virtual C_empresas C_empresas { get; set; }
        public virtual C_grupo_productos_g C_grupo_productos_g { get; set; }
        public virtual C_tipos_codigo C_tipos_codigo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_campaña_codigos> C_campaña_codigos { get; set; }
    }
}
