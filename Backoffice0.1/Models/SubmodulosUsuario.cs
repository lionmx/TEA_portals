using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backoffice0._1.Models
{
    public class SubmodulosUsuario
    {
        private int _id_modulo;
        private int _id_submodulo;
        private string _nombre_submodulo;
        private string _funcion;
        private string _controlador;
        private string _parametros;

        public SubmodulosUsuario()
        {

        }

        public SubmodulosUsuario(int id_modulo,int id_submodulo,string nombre_submodulo, string funcion, string controlador, string parametros)
        {
            this._id_modulo = id_modulo;
            this._id_submodulo = id_submodulo;
            this._nombre_submodulo = nombre_submodulo;
            this._funcion = funcion;
            this._controlador = controlador;
            this._parametros = parametros;

        }
        public int Id_modulo { get => _id_modulo; set => _id_modulo = value; }
        public int Id_submodulo { get => _id_submodulo; set => _id_submodulo = value; }
        public string Nombre_submodulo { get => _nombre_submodulo; set => _nombre_submodulo = value; }
        public string Funcion { get => _funcion; set => _funcion = value; }
        public string Controlador { get => _controlador; set => _controlador = value; }
        public string Parametros { get => _parametros; set => _parametros = value; }

    }
}