using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backoffice0._1.Controllers
{
    public class ConfiguracionController : Controller
    {

        private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();
        // GET: Configuracion
       UsuarioListaViewModel ViewModel = new UsuarioListaViewModel();
        class SelectedUser
        {
            public string DescripcionPerfil { get; set; }
            public string  DescripcionRol { get; set; }

            public string Nombre { get; set; }
        }

        class PermisosAsignados
        {
            public string idUsuario { get; set; }
            public string idPerfil { get; set; }
            public string idRol { get; set; }
            public string idModulo { get; set; }
            public string status { get; set; }
            public string idPermisoAsignado { get; set; }
        }
        public ActionResult Configuracion()
        {
            var usuarios = db.USUARIOS.ToList();



            ViewModel.Usuarios = usuarios;
             
          
            return View(ViewModel);
           
        }
        

      [HttpPost]
        public ActionResult UserSelected(UsuarioListaViewModel model)
        {

            var usuarios = db.USUARIOS.SqlQuery("SELECT * FROM dbo.USUARIOS").ToList();
            var usuario = db.Database.SqlQuery<SelectedUser>(
                @"SELECT  b.DESCRIPCION as DescripcionPerfil FROM dbo.USUARIOS a JOIN dbo.PERFILES b on a.ID_PERFIL = b.ID_PERFIL
                            WHERE ID_USUARIO = '" + model.SelectIdUsuario + "'");

            var rol = db.Database.SqlQuery<SelectedUser>(
                @"select a.NOMBRE as Nombre, b.DESCRIPCION as DescripcionRol FROM USUARIOS a JOIN ROLES b on a.ID_ROL = b.ID_ROL where id_usuario = '" + model.SelectIdUsuario + "'");

            ViewModel.Usuarios = usuarios;
            ViewModel.SelectIdUsuario = model.SelectIdUsuario;
            foreach (var a in usuario)
            {
                ViewBag.DescripcionPerfil = a.DescripcionPerfil;
               
             
            }

            foreach (var n in rol)
            {
                ViewBag.DescripcionRol = n.DescripcionRol;
                ViewModel.Nombre = n.Nombre;
                ViewBag.Nombre = n.Nombre;
            }
            return View("Configuracion", ViewModel);

        }

        public ActionResult GuardarPermisos(string param1, string perfil, string rol, string nombre, string modulo)
        {
            PERMISOS_ASIGNADOS ViewModelPermisos = new PERMISOS_ASIGNADOS();
            
            var items = db.Database.SqlQuery<PermisosAsignados>("SELECT ID_USUARIO as idUsuario, ID_PERFIL as idPerfil, ID_ROL as idRol FROM USUARIOS WHERE NOMBRE = '" + nombre + "'");
            var idModulo = db.Database.SqlQuery<PermisosAsignados>("SELECT ID_MODULO as idModulo FROM MODULOS where NOMBRE = '" + modulo + "'");
            string idPerfil;
            foreach (var a in items)
            {
                ViewModelPermisos.ID_USUARIO = a.idUsuario;
                ViewModelPermisos.ID_ROL = a.idRol;
                idPerfil = a.idPerfil;
            }
            foreach (var a in idModulo)
            {
                ViewModelPermisos.ID_MODULO = a.idModulo;
            }

            string cadena = param1;
            string guardar = "Guardar", editar = "Editar", eliminar = "Eliminar", cancelar = "Cancelar", imprimir = "Imprimir", exportar = "Exportar", ocultar = "Ocultar";
            string id;

            string query = "INSERT INTO PERMISOS_ASIGNADOS VALUES (";
            bool g = cadena.Contains(guardar);
            if (g)
            {
                id = "01";
                SqlCommand command = new SqlCommand();
                db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id + "', '" + ViewModelPermisos.ID_MODULO + "', 'A')");
             

            }
            bool e = cadena.Contains(editar);
            if (e)
            {
                id = "02";
                SqlCommand command = new SqlCommand();
                db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id + "', '" + ViewModelPermisos.ID_MODULO + "', 'A')");
            }
            bool el = cadena.Contains(eliminar);
            if (el)
            {
                id = "03";
                SqlCommand command = new SqlCommand();
                db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id + "', '" + ViewModelPermisos.ID_MODULO + "', 'A')");
            }
            bool c = cadena.Contains(cancelar);
            if (c)
            {
                id = "04";
                SqlCommand command = new SqlCommand();
                db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id + "', '" + ViewModelPermisos.ID_MODULO + "', 'A')");
            }
            bool ex = cadena.Contains(exportar);
            if (ex)
            {
                id = "05";
                SqlCommand command = new SqlCommand();
                db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id + "', '" + ViewModelPermisos.ID_MODULO + "', 'A')");
            }
            bool i = cadena.Contains(imprimir);
            if (i)
            {
                id = "06";
                SqlCommand command = new SqlCommand();
                db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id + "', '" + ViewModelPermisos.ID_MODULO + "', 'A')");
            }
           
            bool o = cadena.Contains(ocultar);
            if (o)
            {
                id = "07";
                SqlCommand command = new SqlCommand();
                db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id + "', '" + ViewModelPermisos.ID_MODULO + "', 'A')");
            }











            var usuarios = db.USUARIOS.SqlQuery("SELECT * FROM dbo.USUARIOS").ToList();
            ViewModel.Usuarios = usuarios;
            return View("Configuracion", ViewModel);
        }

        // GET: Configuracion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Configuracion/Create
     

        // POST: Configuracion/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuracion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Configuracion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuracion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Configuracion/Delete/5
        [HttpPost]

        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
