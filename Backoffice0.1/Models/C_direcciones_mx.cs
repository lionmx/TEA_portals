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
    
    public partial class C_direcciones_mx
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_direcciones_mx()
        {
            this.C_clientes_direccion = new HashSet<C_clientes_direccion>();
            this.C_pedidos = new HashSet<C_pedidos>();
        }
    
        public int id_direccion { get; set; }
        public string calle { get; set; }
        public string numero_ext { get; set; }
        public Nullable<int> id_colonia { get; set; }
        public string entre_calle1 { get; set; }
        public string entre_calle2 { get; set; }
        public string referencia { get; set; }
        public Nullable<int> d_codigo { get; set; }
        public Nullable<int> id_asenta_cpcons { get; set; }
        public Nullable<int> id_ciudad { get; set; }
        public Nullable<int> id_estado { get; set; }
        public Nullable<int> id_colonia_mx { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_clientes_direccion> C_clientes_direccion { get; set; }
        public virtual C_colonias_mx C_colonias_mx { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_pedidos> C_pedidos { get; set; }
    }
}
