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
    
    public partial class C_productos_cat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_productos_cat()
        {
            this.C_impuesto_producto = new HashSet<C_impuesto_producto>();
            this.C_producto_presentacion = new HashSet<C_producto_presentacion>();
            this.C_recetas = new HashSet<C_recetas>();
        }
    
        public int id_producto { get; set; }
        public string sku { get; set; }
        public string plu { get; set; }
        public string codigo_barra { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> id_producto_clasificacion { get; set; }
        public Nullable<int> id_especialidad { get; set; }
        public Nullable<int> id_catalogo_tamanos { get; set; }
        public Nullable<decimal> existencia { get; set; }
        public Nullable<decimal> maximo { get; set; }
        public Nullable<decimal> minimo { get; set; }
        public Nullable<int> id_proveedor { get; set; }
        public Nullable<System.DateTime> fecha_ultima_compra { get; set; }
        public Nullable<bool> incluir_backorder { get; set; }
        public Nullable<int> id_unidad_medida { get; set; }
        public Nullable<int> id_producto_presentacion { get; set; }
        public Nullable<bool> insumos { get; set; }
        public Nullable<bool> activo { get; set; }
        public string path_imagen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_impuesto_producto> C_impuesto_producto { get; set; }
        public virtual C_producto_clasificacion C_producto_clasificacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_producto_presentacion> C_producto_presentacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_recetas> C_recetas { get; set; }
    }
}
