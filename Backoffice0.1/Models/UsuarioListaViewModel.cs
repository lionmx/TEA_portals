using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backoffice0._1.Models
{
    public class UsuarioListaViewModel
    {
        public IEnumerable<USUARIOS> Usuarios { get; set; }

        public string SelectIdUsuario { get; set; }
        public string DescripcionPerfil { get; set; }

        public string DescripcionRol { get; set; }

        public string Nombre { get; set; }

        public string Permisos { get; set; }
    }
}