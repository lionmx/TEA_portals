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
    
    public partial class C_sucursales_direcciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_sucursales_direcciones()
        {
            this.C_sucursales = new HashSet<C_sucursales>();
        }
    
        public int id_sucursal_direccion { get; set; }
        public string cp { get; set; }
        public string calle { get; set; }
        public string num_interior { get; set; }
        public string num_exterior { get; set; }
        public string colonia { get; set; }
        public string entre_calle1 { get; set; }
        public string entre_calle2 { get; set; }
        public string referencia { get; set; }
        public Nullable<int> d_codigo { get; set; }
        public Nullable<int> id_colonia { get; set; }
        public string codigo_sucursal { get; set; }
        public Nullable<int> id_ciudad { get; set; }
        public Nullable<int> id_estado { get; set; }
    
        public virtual C_colonias_mx C_colonias_mx { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_sucursales> C_sucursales { get; set; }
    }
}
