using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backoffice0._1.Models
{
    public class modelConfigBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public modelConfigBase()
        {
            //this.CS_permisos_asignados = new HashSet<CS_permisos_asignados>();
            this.CS_usuario_login = new HashSet<CS_usuario_login>();
        }
        public ICollection<CS_usuarios> usuarios { get; set; }
        public string ID_USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string REF { get; set; }
        public string PASS { get; set; }
        public string ID_PERFIL { get; set; }
        public string ID_ROL { get; set; }
        public string TARJETA_EMPLEADO { get; set; }
        public string ESTADO_ACTUAL { get; set; }
        public string GAFETE_IDENTIFICACION { get; set; }

        public virtual C_servicios SERVICIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual CS_permisos_asignados CS_permisos_asignados { get; set; }
        public virtual CS_roles CS_roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CS_usuario_login> CS_usuario_login { get; set; }

        public int ID_MODULO { get; set; }
        public string NOMBRE_MODULO { get; set; }
         public string SELECTED_MODULO { get; set; }
        public ICollection<C_modulos> modulos { get; set; }
        public List<CS_permisos> permisos { get; set; }

   

    }
}