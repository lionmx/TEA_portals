﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_CORPORATIVA_DEVEntities1 : DbContext
    {
        public DB_CORPORATIVA_DEVEntities1()
            : base("name=DB_CORPORATIVA_DEVEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C_almacen_insumos> C_almacen_insumos { get; set; }
        public virtual DbSet<C_almacenes> C_almacenes { get; set; }
        public virtual DbSet<C_bo_almacen> C_bo_almacen { get; set; }
        public virtual DbSet<C_bo_calculo> C_bo_calculo { get; set; }
        public virtual DbSet<C_bo_calculo_pe> C_bo_calculo_pe { get; set; }
        public virtual DbSet<C_bo_diafestivo_ap> C_bo_diafestivo_ap { get; set; }
        public virtual DbSet<C_bo_diasfestivos> C_bo_diasfestivos { get; set; }
        public virtual DbSet<C_bo_g> C_bo_g { get; set; }
        public virtual DbSet<C_bo_mov_estatus> C_bo_mov_estatus { get; set; }
        public virtual DbSet<C_bo_mov_tipo> C_bo_mov_tipo { get; set; }
        public virtual DbSet<C_bo_orden> C_bo_orden { get; set; }
        public virtual DbSet<C_bo_porc_df> C_bo_porc_df { get; set; }
        public virtual DbSet<C_bo_suc_ir> C_bo_suc_ir { get; set; }
        public virtual DbSet<C_cajas> C_cajas { get; set; }
        public virtual DbSet<C_campaña_codigos> C_campaña_codigos { get; set; }
        public virtual DbSet<C_campaña_empresas> C_campaña_empresas { get; set; }
        public virtual DbSet<C_campaña_medios> C_campaña_medios { get; set; }
        public virtual DbSet<C_campañas> C_campañas { get; set; }
        public virtual DbSet<C_cfdi_informacion> C_cfdi_informacion { get; set; }
        public virtual DbSet<C_ciudades> C_ciudades { get; set; }
        public virtual DbSet<C_clientes> C_clientes { get; set; }
        public virtual DbSet<C_clientes_telefono> C_clientes_telefono { get; set; }
        public virtual DbSet<C_codigos_postales> C_codigos_postales { get; set; }
        public virtual DbSet<C_colonias> C_colonias { get; set; }
        public virtual DbSet<C_cp_mexico> C_cp_mexico { get; set; }
        public virtual DbSet<C_delivery_sucursal_repartidor> C_delivery_sucursal_repartidor { get; set; }
        public virtual DbSet<C_direcciones> C_direcciones { get; set; }
        public virtual DbSet<C_direcciones_3_> C_direcciones_3_ { get; set; }
        public virtual DbSet<C_empleados> C_empleados { get; set; }
        public virtual DbSet<C_empleados_team_card> C_empleados_team_card { get; set; }
        public virtual DbSet<C_empresas> C_empresas { get; set; }
        public virtual DbSet<C_empresas_sucursales> C_empresas_sucursales { get; set; }
        public virtual DbSet<C_equipo_sucursal> C_equipo_sucursal { get; set; }
        public virtual DbSet<C_estados> C_estados { get; set; }
        public virtual DbSet<C_eventos> C_eventos { get; set; }
        public virtual DbSet<C_facturas> C_facturas { get; set; }
        public virtual DbSet<C_grupo_productos_codigos> C_grupo_productos_codigos { get; set; }
        public virtual DbSet<C_grupo_productos_codigos_movs> C_grupo_productos_codigos_movs { get; set; }
        public virtual DbSet<C_grupo_productos_d> C_grupo_productos_d { get; set; }
        public virtual DbSet<C_grupo_productos_destino_social> C_grupo_productos_destino_social { get; set; }
        public virtual DbSet<C_grupo_productos_g> C_grupo_productos_g { get; set; }
        public virtual DbSet<C_grupo_productos_prods> C_grupo_productos_prods { get; set; }
        public virtual DbSet<C_grupo_productos_subclases> C_grupo_productos_subclases { get; set; }
        public virtual DbSet<C_grupo_productos_sucursales> C_grupo_productos_sucursales { get; set; }
        public virtual DbSet<C_grupo_productos_tipos> C_grupo_productos_tipos { get; set; }
        public virtual DbSet<C_impuesto_producto> C_impuesto_producto { get; set; }
        public virtual DbSet<C_impuestos> C_impuestos { get; set; }
        public virtual DbSet<C_insumo_cat> C_insumo_cat { get; set; }
        public virtual DbSet<C_insumo_clasificacion> C_insumo_clasificacion { get; set; }
        public virtual DbSet<C_insumo_mov_status> C_insumo_mov_status { get; set; }
        public virtual DbSet<C_insumo_mov_suc_d> C_insumo_mov_suc_d { get; set; }
        public virtual DbSet<C_insumo_mov_suc_g> C_insumo_mov_suc_g { get; set; }
        public virtual DbSet<C_insumo_sucursal> C_insumo_sucursal { get; set; }
        public virtual DbSet<C_insumo_tipo_mov> C_insumo_tipo_mov { get; set; }
        public virtual DbSet<C_marca_config> C_marca_config { get; set; }
        public virtual DbSet<C_marcas> C_marcas { get; set; }
        public virtual DbSet<C_marcas_sociedades> C_marcas_sociedades { get; set; }
        public virtual DbSet<C_medios> C_medios { get; set; }
        public virtual DbSet<C_modulos> C_modulos { get; set; }
        public virtual DbSet<C_notificaciones_mensajes> C_notificaciones_mensajes { get; set; }
        public virtual DbSet<C_notificaciones_subtipos> C_notificaciones_subtipos { get; set; }
        public virtual DbSet<C_notificaciones_tipo> C_notificaciones_tipo { get; set; }
        public virtual DbSet<C_pago_tipo> C_pago_tipo { get; set; }
        public virtual DbSet<C_paises> C_paises { get; set; }
        public virtual DbSet<C_parametros> C_parametros { get; set; }
        public virtual DbSet<C_parametros_empresa> C_parametros_empresa { get; set; }
        public virtual DbSet<C_parametros_sucursales> C_parametros_sucursales { get; set; }
        public virtual DbSet<C_pedidos> C_pedidos { get; set; }
        public virtual DbSet<C_pedidos_d> C_pedidos_d { get; set; }
        public virtual DbSet<C_pedidos_empleados> C_pedidos_empleados { get; set; }
        public virtual DbSet<C_pedidos_propinas> C_pedidos_propinas { get; set; }
        public virtual DbSet<C_pedidos_tipo> C_pedidos_tipo { get; set; }
        public virtual DbSet<C_pos_caja_cambioturno> C_pos_caja_cambioturno { get; set; }
        public virtual DbSet<C_pos_caja_cambioturno_insumos> C_pos_caja_cambioturno_insumos { get; set; }
        public virtual DbSet<C_pos_caja_movs> C_pos_caja_movs { get; set; }
        public virtual DbSet<C_presentaciones> C_presentaciones { get; set; }
        public virtual DbSet<C_producto_clasificacion> C_producto_clasificacion { get; set; }
        public virtual DbSet<C_producto_presentacion> C_producto_presentacion { get; set; }
        public virtual DbSet<C_productos_cat> C_productos_cat { get; set; }
        public virtual DbSet<C_productos_precios> C_productos_precios { get; set; }
        public virtual DbSet<C_productos_sucursal> C_productos_sucursal { get; set; }
        public virtual DbSet<C_recetas> C_recetas { get; set; }
        public virtual DbSet<C_servicios> C_servicios { get; set; }
        public virtual DbSet<C_servicios_modulos> C_servicios_modulos { get; set; }
        public virtual DbSet<C_servicios_roles> C_servicios_roles { get; set; }
        public virtual DbSet<C_servicios_sucursal> C_servicios_sucursal { get; set; }
        public virtual DbSet<C_sociedades> C_sociedades { get; set; }
        public virtual DbSet<C_sociedades_empresas> C_sociedades_empresas { get; set; }
        public virtual DbSet<C_suc_invreal> C_suc_invreal { get; set; }
        public virtual DbSet<C_suc_invreal_d> C_suc_invreal_d { get; set; }
        public virtual DbSet<C_sucursales> C_sucursales { get; set; }
        public virtual DbSet<C_sucursales_direcciones> C_sucursales_direcciones { get; set; }
        public virtual DbSet<C_sval_actas_diferencia> C_sval_actas_diferencia { get; set; }
        public virtual DbSet<C_sval_cajeros> C_sval_cajeros { get; set; }
        public virtual DbSet<C_sval_deposito_billetes> C_sval_deposito_billetes { get; set; }
        public virtual DbSet<C_sval_deposito_envases> C_sval_deposito_envases { get; set; }
        public virtual DbSet<C_sval_depositos> C_sval_depositos { get; set; }
        public virtual DbSet<C_sval_empresa_servicios_contratados> C_sval_empresa_servicios_contratados { get; set; }
        public virtual DbSet<C_sval_nominas> C_sval_nominas { get; set; }
        public virtual DbSet<C_sval_nominas_envases> C_sval_nominas_envases { get; set; }
        public virtual DbSet<C_sval_orden_envases> C_sval_orden_envases { get; set; }
        public virtual DbSet<C_sval_ordenes> C_sval_ordenes { get; set; }
        public virtual DbSet<C_sval_servicios> C_sval_servicios { get; set; }
        public virtual DbSet<C_sval_sessiones_captura> C_sval_sessiones_captura { get; set; }
        public virtual DbSet<C_team_card_movs> C_team_card_movs { get; set; }
        public virtual DbSet<C_team_card_tipo_movs> C_team_card_tipo_movs { get; set; }
        public virtual DbSet<C_telefonos> C_telefonos { get; set; }
        public virtual DbSet<C_tipo_empaque> C_tipo_empaque { get; set; }
        public virtual DbSet<C_tipo_entrega> C_tipo_entrega { get; set; }
        public virtual DbSet<C_tipos_codigo> C_tipos_codigo { get; set; }
        public virtual DbSet<C_tracking_status> C_tracking_status { get; set; }
        public virtual DbSet<C_tracking_tiempos> C_tracking_tiempos { get; set; }
        public virtual DbSet<C_unidades> C_unidades { get; set; }
        public virtual DbSet<C_unidades_medida> C_unidades_medida { get; set; }
        public virtual DbSet<C_usuarios_sucursales> C_usuarios_sucursales { get; set; }
        public virtual DbSet<C_ventas_d> C_ventas_d { get; set; }
        public virtual DbSet<C_ventas_g> C_ventas_g { get; set; }
        public virtual DbSet<C_ventas_i> C_ventas_i { get; set; }
        public virtual DbSet<C_ventas_pagos> C_ventas_pagos { get; set; }
        public virtual DbSet<C_ventas_status> C_ventas_status { get; set; }
        public virtual DbSet<C_ventas_tipo> C_ventas_tipo { get; set; }
        public virtual DbSet<C_zona_almacen_suc> C_zona_almacen_suc { get; set; }
        public virtual DbSet<C_zonas_almacenaje> C_zonas_almacenaje { get; set; }
        public virtual DbSet<C_zonas_precio> C_zonas_precio { get; set; }
        public virtual DbSet<CS_callcenter_cliente> CS_callcenter_cliente { get; set; }
        public virtual DbSet<CS_permisos> CS_permisos { get; set; }
        public virtual DbSet<CS_permisos_asignados> CS_permisos_asignados { get; set; }
        public virtual DbSet<CS_roles> CS_roles { get; set; }
        public virtual DbSet<CS_usuario_login> CS_usuario_login { get; set; }
        public virtual DbSet<CS_usuarios> CS_usuarios { get; set; }
        public virtual DbSet<ELMAH_Error> ELMAH_Error { get; set; }
        public virtual DbSet<M_callcenter_clientes> M_callcenter_clientes { get; set; }
        public virtual DbSet<M_callcenter_clientes_adicionales> M_callcenter_clientes_adicionales { get; set; }
        public virtual DbSet<P_Usuarios> P_Usuarios { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UserLevelPermissions> UserLevelPermissions { get; set; }
        public virtual DbSet<UserLevels> UserLevels { get; set; }
        public virtual DbSet<C_catalogo_consecutivos> C_catalogo_consecutivos { get; set; }
        public virtual DbSet<C_catalogo_sn> C_catalogo_sn { get; set; }
        public virtual DbSet<C_catalogo_tamanos> C_catalogo_tamanos { get; set; }
        public virtual DbSet<C_clientes_direccion_old> C_clientes_direccion_old { get; set; }
        public virtual DbSet<C_empresas_servicios_contratados> C_empresas_servicios_contratados { get; set; }
        public virtual DbSet<C_empresas_tipo> C_empresas_tipo { get; set; }
        public virtual DbSet<C_grupo_aplica_operacion> C_grupo_aplica_operacion { get; set; }
        public virtual DbSet<C_insumo_cat_old> C_insumo_cat_old { get; set; }
        public virtual DbSet<C_insumo_tipo_empaque> C_insumo_tipo_empaque { get; set; }
        public virtual DbSet<C_insumo_tipo_envase> C_insumo_tipo_envase { get; set; }
        public virtual DbSet<C_insumo_tipo_inventario> C_insumo_tipo_inventario { get; set; }
        public virtual DbSet<C_producto_tipo> C_producto_tipo { get; set; }
        public virtual DbSet<C_productos_especialidades> C_productos_especialidades { get; set; }
        public virtual DbSet<C_productos_marca> C_productos_marca { get; set; }
        public virtual DbSet<C_proveedores> C_proveedores { get; set; }
        public virtual DbSet<C_sval_actas> C_sval_actas { get; set; }
        public virtual DbSet<C_sval_arqueos> C_sval_arqueos { get; set; }
        public virtual DbSet<C_sval_cat_desglose_billetes> C_sval_cat_desglose_billetes { get; set; }
        public virtual DbSet<C_sval_cat_desglose_monedas> C_sval_cat_desglose_monedas { get; set; }
        public virtual DbSet<C_sval_cliente_servicios> C_sval_cliente_servicios { get; set; }
        public virtual DbSet<C_sval_clientes> C_sval_clientes { get; set; }
        public virtual DbSet<C_sval_deposito_monedas> C_sval_deposito_monedas { get; set; }
        public virtual DbSet<C_sval_destinos_entrega> C_sval_destinos_entrega { get; set; }
        public virtual DbSet<C_sval_documentos> C_sval_documentos { get; set; }
        public virtual DbSet<C_sval_empresa_servicios> C_sval_empresa_servicios { get; set; }
        public virtual DbSet<C_sval_facturar_a> C_sval_facturar_a { get; set; }
        public virtual DbSet<C_sval_nominas_billetes> C_sval_nominas_billetes { get; set; }
        public virtual DbSet<C_sval_nominas_monedas> C_sval_nominas_monedas { get; set; }
        public virtual DbSet<C_sval_orden_billetes> C_sval_orden_billetes { get; set; }
        public virtual DbSet<C_sval_orden_monedas> C_sval_orden_monedas { get; set; }
        public virtual DbSet<C_sval_recepcion> C_sval_recepcion { get; set; }
        public virtual DbSet<C_sval_recepcion_billetes> C_sval_recepcion_billetes { get; set; }
        public virtual DbSet<C_sval_recepcion_monedas> C_sval_recepcion_monedas { get; set; }
        public virtual DbSet<C_sval_terminales> C_sval_terminales { get; set; }
        public virtual DbSet<C_sval_tipo_moneda> C_sval_tipo_moneda { get; set; }
        public virtual DbSet<C_sval_tipo_servicios> C_sval_tipo_servicios { get; set; }
        public virtual DbSet<P_encuesta_1> P_encuesta_1 { get; set; }
        public virtual DbSet<P_opciones> P_opciones { get; set; }
    }
}
