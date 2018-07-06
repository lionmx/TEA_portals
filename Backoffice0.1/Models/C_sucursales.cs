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
    
    public partial class C_sucursales
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_sucursales()
        {
            this.C_autorizacion_solicitudes = new HashSet<C_autorizacion_solicitudes>();
            this.C_cajas_sucursales = new HashSet<C_cajas_sucursales>();
            this.C_empresas_sucursales = new HashSet<C_empresas_sucursales>();
            this.C_equipo_sucursal = new HashSet<C_equipo_sucursal>();
            this.C_grupo_productos_codigos_movs = new HashSet<C_grupo_productos_codigos_movs>();
            this.C_insumo_sucursal = new HashSet<C_insumo_sucursal>();
            this.C_parametros_sucursales = new HashSet<C_parametros_sucursales>();
            this.C_pedidos = new HashSet<C_pedidos>();
            this.C_servicios_sucursal = new HashSet<C_servicios_sucursal>();
            this.C_sucursales1 = new HashSet<C_sucursales>();
            this.C_sucursales_colonias = new HashSet<C_sucursales_colonias>();
            this.C_sucursales_config = new HashSet<C_sucursales_config>();
            this.C_sucursales_marcas = new HashSet<C_sucursales_marcas>();
            this.C_usuarios_sucursales = new HashSet<C_usuarios_sucursales>();
            this.C_ventas_g = new HashSet<C_ventas_g>();
        }
    
        public int Id_sucursal { get; set; }
        public string codigo_sucursal { get; set; }
        public string nombre { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public bool status_servicio { get; set; }
        public Nullable<int> id_sucursal_status_motivo { get; set; }
        public Nullable<int> id_direccion { get; set; }
        public Nullable<int> id_empresa { get; set; }
        public Nullable<int> id_zona_precio { get; set; }
        public string ip { get; set; }
        public Nullable<bool> activo { get; set; }
        public string sucursal_desvio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_autorizacion_solicitudes> C_autorizacion_solicitudes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_cajas_sucursales> C_cajas_sucursales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_empresas_sucursales> C_empresas_sucursales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_equipo_sucursal> C_equipo_sucursal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_grupo_productos_codigos_movs> C_grupo_productos_codigos_movs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_insumo_sucursal> C_insumo_sucursal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_parametros_sucursales> C_parametros_sucursales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_pedidos> C_pedidos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_servicios_sucursal> C_servicios_sucursal { get; set; }
        public virtual C_sucursal_status_motivo C_sucursal_status_motivo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_sucursales> C_sucursales1 { get; set; }
        public virtual C_sucursales C_sucursales2 { get; set; }
        public virtual C_sucursales_direcciones C_sucursales_direcciones { get; set; }
        public virtual C_zonas_precio C_zonas_precio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_sucursales_colonias> C_sucursales_colonias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_sucursales_config> C_sucursales_config { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_sucursales_marcas> C_sucursales_marcas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_usuarios_sucursales> C_usuarios_sucursales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C_ventas_g> C_ventas_g { get; set; }
    }
}
