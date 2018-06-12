using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Backoffice0._1.Controllers
{
    public class ConfiguracionController : Controller
    {
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
     
        // GET: Configuracion
       modelConfigBase ViewModelUsuario = new modelConfigBase();
        class SelectedUser
        {
            public int id_servicio { get; set; }
            public string  nombre_servicio { get; set; }
            public string descripcionRol { get; set; }
            public string Nombre { get; set; }
        }

        class PermisosAsignados
        {
            public string idUsuario { get; set; }
            public int idServicio { get; set; }
            public string idRol { get; set; }
            public string idModulo { get; set; }
            public string status { get; set; }
            public int idPermisoAsignado { get; set; }
        }
        public ActionResult Configuracion()
        {
            #region Viewbags 

            //obtiene los permisos de cada servicio/modulo para el usuario loggeado           
            List<int> permisosLista = new List<int>();
            string loggedId = Session["LoggedId"].ToString();
            var permisosServicioModulo = db.Database.SqlQuery<permisosServicioModulo>("SELECT b.id_servicio as id_servicio, b.id_modulo as id_modulo, a.id_permiso as id_permiso from CS_PERMISOS_ASIGNADOS a JOIN C_SERVICIOS_MODULOS b on a.id_servicios_modulos = b.id_servicios_modulos WHERE a.ID_USUARIO = '" + Session["LoggedId"] + "'");

            if (permisosServicioModulo != null)
            {
                foreach (var n in permisosServicioModulo)
                {
                    permisosLista.Add(n.id_servicio);
                    permisosLista.Add(n.id_modulo);
                    permisosLista.Add(Convert.ToInt32(n.id_permiso));
                }
            }

            //get perfil (servicio) de usuario loggeado y guardarlo en ViewBag
            var perfil = db.CS_usuarios.Where(a => a.ID_USUARIO.Equals(loggedId)).FirstOrDefault();
            if (perfil != null)
            {
                ViewBag.idServicio = perfil.ID_SERVICIO;
            }
            ViewBag.permisos = permisosLista;
            #endregion


            var usuarios = db.CS_usuarios.ToList();
            var modulos = db.C_modulos.ToList();
            var permisos = db.CS_permisos.ToList();               
            ViewModelUsuario.modulos = modulos;
            ViewModelUsuario.usuarios = usuarios;
            ViewModelUsuario.permisos = permisos;
   
            return View(ViewModelUsuario);
           
        }
        

      [HttpPost]
        public ActionResult UserSelected(CS_usuarios model)
        {
            var permisos = db.CS_permisos.ToList();
            var usuarios = db.CS_usuarios.ToList();  
            var modulos = db.C_modulos.ToList();

            var usuario = db.Database.SqlQuery<SelectedUser>(
                @"SELECT  b.nombre_servicio as nombre_servicio, b.id_servicio as id_servicio FROM dbo.CS_USUARIOS a JOIN dbo.c_servicios b on a.id_servicio = b.id_servicio
                            WHERE ID_USUARIO = '" + model.ID_USUARIO + "'");

            var rol = db.Database.SqlQuery<SelectedUser>(
                @"select a.NOMBRE as Nombre, b.DESCRIPCION as descripcionRol FROM CS_USUARIOS a JOIN CS_ROLES b on a.ID_ROL = b.ID_ROL where id_usuario = '" + model.ID_USUARIO+ "'");
            ViewModelUsuario.permisos = permisos;
            ViewModelUsuario.usuarios = usuarios;
            ViewModelUsuario.modulos = modulos;
            ViewModelUsuario.ID_USUARIO = model.ID_USUARIO;
       
            foreach (var a in usuario)
            {
                ViewBag.DescripcionServicio = a.nombre_servicio;
                ViewBag.idServicio = a.id_servicio;                       
            }
            foreach (var m in modulos)
            {
                ViewModelUsuario.NOMBRE_MODULO = m.Nombre;
                ViewBag.NombreModulo = ViewModelUsuario.NOMBRE_MODULO;
            }
            foreach (var n in rol)
            {
                ViewBag.DescripcionRol = n.descripcionRol;
                ViewModelUsuario.NOMBRE = n.Nombre;
                ViewBag.Nombre = n.Nombre;
            }
            return View("Configuracion", ViewModelUsuario);

        }

        [HttpPost]
        public ActionResult moduleSelected(modelConfigBase modelBase)
        {

            var permisos = db.CS_permisos.ToList();
            var usuarios = db.CS_usuarios.ToList();
            var modulos = db.C_modulos.SqlQuery("SELECT * FROM dbo.C_modulos where idmodulo = '"+modelBase.ID_MODULO+"'").ToList();
            var usuario = db.Database.SqlQuery<SelectedUser>(
                @"SELECT  b.nombre_servicio as nombre_servicio, b.id_servicio as id_servicio FROM dbo.CS_USUARIOS a JOIN dbo.c_servicios b on a.id_servicio = b.id_servicio
                            WHERE ID_USUARIO = '" + Session["LoggedId"].ToString() + "'");

            var rol = db.Database.SqlQuery<SelectedUser>(
                @"select a.NOMBRE as Nombre, b.DESCRIPCION as descripcionRol FROM CS_USUARIOS a JOIN CS_ROLES b on a.ID_ROL = b.ID_ROL where id_usuario = '" + Session["LoggedId"].ToString() + "'");
            ViewModelUsuario.permisos = permisos;
            ViewModelUsuario.usuarios = usuarios;
            ViewModelUsuario.modulos = modulos;
           
            foreach (var a in usuario)
            {
                ViewBag.DescripcionServicio = a.nombre_servicio;
                ViewBag.idServicio = a.id_servicio;

            }
            foreach (var m in modulos)
            {
                ViewModelUsuario.NOMBRE_MODULO = m.Nombre;
                ViewBag.NombreModulo = ViewModelUsuario.NOMBRE_MODULO;
            }
            foreach (var n in rol)
            {
                ViewBag.DescripcionRol = n.descripcionRol;
                ViewModelUsuario.NOMBRE = n.Nombre;
                ViewBag.Nombre = n.Nombre;
            }

            var selectedModel = modelBase.ID_MODULO;
            int idServicio=0;
            CS_permisos_asignados ViewModelPermisos = new CS_permisos_asignados();
            string query = "INSERT INTO CS_permisos_asignados VALUES (";
            string id2;
            var items = db.Database.SqlQuery<PermisosAsignados>("SELECT ID_USUARIO as idUsuario, ID_SERVICIO as idServicio, ID_ROL as idRol FROM cs_USUARIOS WHERE NOMBRE = '" + ViewBag.Nombre + "'");
          
            foreach (var a in items)
            {
                ViewModelPermisos.ID_USUARIO = a.idUsuario;
                ViewModelPermisos.ID_ROL = a.idRol;
                idServicio = a.idServicio;
               
            }
           
           var servicioModulo = db.C_servicios_modulos.SqlQuery("SELECT * FROM C_SERVICIOS_MODULOS WHERE ID_SERVICIO = '" + idServicio + "' AND ID_MODULO = '" + modelBase.ID_MODULO+"'");
            if (servicioModulo != null)
            {
                foreach (var n in servicioModulo)
                {
                    ViewModelPermisos.ID_SERVICIOS_MODULOS = Convert.ToInt32(n.id_servicios_modulos);
                }
            }
           
       
            return View("Configuracion", modelBase);
        }
    

       
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
