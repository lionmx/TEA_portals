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
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
        //private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();
        // GET: Configuracion
       CS_usuarios ViewModelUsuario = new CS_usuarios();
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
            #region Viewbags 

            CS_permisos_asignados ViewModel = new CS_permisos_asignados();
            List<string> listaPA = new List<string>();
            if (Session["LoggedUser"] != null)
            {

                string user = Session["LoggedUser"].ToString();
                string loggedId = Session["LoggedId"].ToString();
            var id = from us in db.CS_usuarios
                     where us.NOMBRE.Equals(user)
                     select us;

            foreach (var i in id)
            {
                user = i.ID_USUARIO;
            }
            for (int i = 0; i <= 8; i++)
            {
                var mod1 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "0" + i && a.ID_PERMISO.Equals("07")).FirstOrDefault();
                if (mod1 != null)
                {
                    listaPA.Add(mod1.ID_MODULO);
                    listaPA.Add(mod1.ID_PERMISO);

                }
            }
            ViewBag.data = listaPA;
            List<string> listaPA2 = new List<string>();
            for (int i = 0; i <= 8; i++)
            {
                var mod2 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "'0" + i + "'" && a.ID_PERMISO.Equals("08")).FirstOrDefault();
                if (mod2 != null)
                {
                    listaPA2.Add(mod2.ID_MODULO);
                    listaPA2.Add(mod2.ID_PERMISO);
                }
            }
            ViewBag.data2 = listaPA2;
            }
            else
            {
                CS_usuario_login vm = new CS_usuario_login();
                RedirectToAction("UsuarioLogin", vm);
            }
            #endregion


            var usuarios = db.CS_usuarios.ToList();
           // ViewModelUsuario.usuarios = usuarios;          
            return View(ViewModelUsuario);
           
        }
        

      [HttpPost]
        public ActionResult UserSelected(CS_usuarios model)
        {

            var usuarios = db.CS_usuarios.SqlQuery("SELECT * FROM dbo.CS_USUARIOS").ToList();
            var usuario = db.Database.SqlQuery<SelectedUser>(
                @"SELECT  b.DESCRIPCION as DescripcionPerfil FROM dbo.CS_USUARIOS a JOIN dbo.CS_PERFILES b on a.ID_PERFIL = b.ID_PERFIL
                            WHERE ID_USUARIO = '" + model.ID_USUARIO + "'");

            var rol = db.Database.SqlQuery<SelectedUser>(
                @"select a.NOMBRE as Nombre, b.DESCRIPCION as DescripcionRol FROM CS_USUARIOS a JOIN CS_ROLES b on a.ID_ROL = b.ID_ROL where id_usuario = '" + model.ID_USUARIO+ "'");

            //ViewModelUsuario.usuarios = usuarios;
            ViewModelUsuario.ID_USUARIO = model.ID_USUARIO;
            foreach (var a in usuario)
            {
                ViewBag.DescripcionPerfil = a.DescripcionPerfil;
               
             
            }

            foreach (var n in rol)
            {
                ViewBag.DescripcionRol = n.DescripcionRol;
                ViewModelUsuario.NOMBRE = n.Nombre;
                ViewBag.Nombre = n.Nombre;
            }
            return View("Configuracion", ViewModelUsuario);

        }

        public ActionResult GuardarPermisos(string param1, string perfil, string rol, string nombre, string modulo)
        {

            #region Viewbags 

            CS_permisos_asignados ViewModel = new CS_permisos_asignados();
            List<string> listaPA = new List<string>();
            string user = Session["LoggedUser"].ToString();
            var ids = from us in db.CS_usuarios
                     where us.NOMBRE.Equals(user)
                     select us;

            foreach (var i in ids)
            {
                user = i.ID_USUARIO;
            }
            for (int i = 00; i <= 8; i++)
            {
                var mod1 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(user) && a.ID_MODULO == "0" + i && a.ID_PERMISO == "07").FirstOrDefault();
                if (mod1 != null)
                {
                    listaPA.Add(mod1.ID_MODULO);
                    listaPA.Add(mod1.ID_PERMISO);

                }
            }
            ViewBag.data = listaPA;
            List<string> listaPA2 = new List<string>();
            for (int i = 0; i <= 8; i++)
            {
                var mod2 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(user) && a.ID_MODULO == "'0" + i + "'" && a.ID_PERMISO.Equals("08")).FirstOrDefault();
                if (mod2 != null)
                {
                    listaPA2.Add(mod2.ID_MODULO);
                    listaPA2.Add(mod2.ID_PERMISO);
                }
            }
            ViewBag.data2 = listaPA2;

            #endregion

           CS_permisos_asignados ViewModelPermisos = new CS_permisos_asignados();
            
            var items = db.Database.SqlQuery<PermisosAsignados>("SELECT ID_USUARIO as idUsuario, ID_PERFIL as idPerfil, ID_ROL as idRol FROM cs_USUARIOS WHERE NOMBRE = '" + nombre + "'");
            var idModulo = db.Database.SqlQuery<PermisosAsignados>("SELECT ID_MODULO as idModulo FROM cs_MODULOS where NOMBRE = '" + modulo + "'");
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
            string guardar = "Guardar", editar = "Editar", eliminar = "Eliminar", cancelar = "Cancelar", imprimir = "Imprimir", exportar = "Exportar", ocultar = "Ocultar", mostrar ="Mostrar";
            string id;

            if (cadena != null)
            {

            string query = "INSERT INTO CS_permisos_asignados VALUES (";
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
                var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '08' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                if (obj.Count() == 0)
                {
                SqlCommand command = new SqlCommand();
                db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id + "', '" + ViewModelPermisos.ID_MODULO + "', 'A')");
                }
                else
                {
                    SqlCommand command = new SqlCommand();
                    db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET STATUS = 'I' WHERE ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                }

            }

            bool m = cadena.Contains(mostrar);
            if (m)
            {
                id = "08";
                var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '07' AND ID_USUARIO = '" + ViewModelPermisos.ID_USUARIO + "', AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "', AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                if (obj == null)
                {
                    SqlCommand command = new SqlCommand();
                    db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id + "', '" + ViewModelPermisos.ID_MODULO + "', 'A')");
                }
                else
                {
                    SqlCommand command = new SqlCommand();
                    db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET STATUS = 'I' WHERE ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                }
            

            }
            }

            var usuarios = db.CS_usuarios.SqlQuery("SELECT * FROM dbo.CS_usuarios").ToList();
            ViewModelUsuario.usuarios = usuarios;
            return View("Configuracion", ViewModelUsuario);
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
