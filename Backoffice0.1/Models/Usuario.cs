using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backoffice0._1.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Ref { get; set; }

        public string Pass { get; set; }

        public int IdPerfil { get; set; }

        public int IdRol { get; set; }

        public string TarjetaEmpleado { get; set; }

        public string EdoActual { get; set; }

        public string GafeteIdentificacion { get; set; }
    }
}