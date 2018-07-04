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
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        // GET: Configuracion
        modelConfigBase ViewModelUsuario = new modelConfigBase();
        class SelectedUser
        {
            public int id_servicio { get; set; }
            public string nombre_servicio { get; set; }
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
           /* #region Viewbags 

            //obtiene los permisos de cada servicio/modulo para el usuario loggeado           
            //obtiene los permisos de cada servicio/modulo para el usuario loggeado           
            List<int> permisosLista = new List<int>();
            int loggedId = Convert.ToInt32(Session["LoggedId"]);
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
            */
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
                @"select a.NOMBRE as Nombre, b.DESCRIPCION as descripcionRol FROM CS_USUARIOS a JOIN CS_ROLES b on a.ID_ROL = b.ID_ROL where id_usuario = '" + model.ID_USUARIO + "'");

            int loggedId = Convert.ToInt32(Session["LoggedId"]);
            List<CS_permisos_asignados> paList = new List<CS_permisos_asignados>();
            ViewModelUsuario.permisos = permisos;
            ViewModelUsuario.usuarios = usuarios;
            ViewModelUsuario.modulos = modulos;
            ViewModelUsuario.ID_USUARIO = model.ID_USUARIO.ToString(); ;

            foreach (var a in usuario)
            {
                ViewBag.DescripcionServicio = a.nombre_servicio;
                ViewBag.idServicio = a.id_servicio;
            }
            foreach (var m in modulos)
            {
             //   ViewModelUsuario.NOMBRE_MODULO = m.nombre_modulo;
                ViewBag.NombreModulo = ViewModelUsuario.NOMBRE_MODULO;
            }
            foreach (var n in rol)
            {
                ViewBag.DescripcionRol = n.descripcionRol;
                ViewModelUsuario.NOMBRE = n.Nombre;
                ViewBag.Nombre = n.Nombre;
            }
            if (ViewBag.idServicio == 8)
            {
                var permisoAsignadoID = db.CS_permisos_asignados.Where(a => a.ID_USUARIO==loggedId && a.ID_SERVICIOS_MODULOS != null);
               foreach (var n in permisoAsignadoID)
                {
                    paList.Add(n);
                }

                
            }
            else
            {
                var permisoAsignadoID = db.CS_permisos_asignados.SqlQuery("SELECT id_servicios_modulos from cs_permisos_asignados where id_usuario='" + Session["LoggedId"] + "' and id_servicios_modulos = '" + ViewBag.idServicio + "'");
                ViewBag.permisoAsignado = permisoAsignadoID.FirstOrDefault().ID_SERVICIOS_MODULOS;
            }
            var permisosAsignados = db.CS_permisos_asignados.SqlQuery("SELECT ID_PERMISO, ID_MODULO FROM CS_PERMISOS_ASIGNADOS WHERE ID_USUARIO = '" + Session["LoggedId"] + "' and ACTIVO = 1 and ID_SERVICIOS_MODULOS = '" + ViewBag.permisoAsignado + "'");
            ViewModelUsuario.permisosAsignados = paList;
            ViewModelUsuario.modulosPermisosList.Add(new modulosPermisos());
            return View("Configuracion", ViewModelUsuario);

        }

        [HttpPost]
        public ActionResult moduleSelected(modelConfigBase modelBase, List<CS_permisos> pList)
        {
            List<CS_permisos> sesionList = new List<CS_permisos>();
            ViewBag.list = modelBase.permisosListLsit;
            var permisos = db.CS_permisos.ToList();
            var usuarios = db.CS_usuarios.ToList();
            var modulos = db.C_modulos.SqlQuery("SELECT * FROM dbo.C_modulos where idmodulo = '" + modelBase.ID_MODULO + "'").ToList();
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
               // ViewModelUsuario.NOMBRE_MODULO = m.nombre_modulo;
                ViewBag.NombreModulo = ViewModelUsuario.NOMBRE_MODULO;
            }
            foreach (var n in rol)
            {
                ViewBag.DescripcionRol = n.descripcionRol;
                ViewModelUsuario.NOMBRE = n.Nombre;
                ViewBag.Nombre = n.Nombre;
            }

            var selectedModel = modelBase.ID_MODULO;
            int idServicio = 0;
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

            var servicioModulo = db.C_servicios_modulos.SqlQuery("SELECT * FROM C_SERVICIOS_MODULOS WHERE ID_SERVICIO = '" + idServicio + "' AND ID_MODULO = '" + modelBase.ID_MODULO + "'");
            if (servicioModulo != null)
            {
                foreach (var n in servicioModulo)
                {
                    ViewModelPermisos.ID_SERVICIOS_MODULOS = Convert.ToInt32(n.id_servicios_modulos);
                }
            }

            for (int i = 0; i < modelBase.permisos.Count(); i++)
            {
                if (modelBase.permisos[i].ACTIVO & modelBase.permisos[i].DESCRIPCION == "GUARDAR")
                {
                    id2 = "01";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '01' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ACTIVO = 1'");
                    if (obj.Count() == 0)
                        db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id2 + "', '" + modelBase.ID_MODULO + "', " + ViewModelPermisos.ID_SERVICIOS_MODULOS + ", 1)");

                }
                else if (modelBase.permisos[i].ACTIVO == false & modelBase.permisos[i].DESCRIPCION == "GUARDAR")
                {
                    id2 = "01";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '01' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                    if (obj.Count() != 0)
                        db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET ACTIVO = 0 WHERE ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "'");

                }
                else if (modelBase.permisos[i].ACTIVO & modelBase.permisos[i].DESCRIPCION == "EDITAR")
                {
                    id2 = "02";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '02' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ACTIVO = 1'");
                    if (obj.Count() == 0)
                        db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id2 + "', '" + modelBase.ID_MODULO + "', " + ViewModelPermisos.ID_SERVICIOS_MODULOS + ", 1)");
                }
                else if (modelBase.permisos[i].ACTIVO == false & modelBase.permisos[i].DESCRIPCION == "EDITAR")
                {
                    id2 = "02";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '02' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                    if (obj.Count() != 0)
                        db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET ACTIVO = 0 WHERE ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "'");

                }
                else if (modelBase.permisos[i].ACTIVO & modelBase.permisos[i].DESCRIPCION == "ELIMINAR")
                {
                    id2 = "03";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '03' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ACTIVO = 1'");
                    if (obj.Count() == 0)
                        db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id2 + "', '" + modelBase.ID_MODULO + "', " + ViewModelPermisos.ID_SERVICIOS_MODULOS + ", 1)");
                }
                else if (modelBase.permisos[i].ACTIVO == false & modelBase.permisos[i].DESCRIPCION == "ELIMINAR")
                {
                    id2 = "03";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '03' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                    if (obj.Count() != 0)
                        db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET ACTIVO = 0 WHERE ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "'");
                }
                else if (modelBase.permisos[i].ACTIVO & modelBase.permisos[i].DESCRIPCION == "CANCELAR")
                {
                    id2 = "04";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '04' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ACTIVO = 1'");
                    if (obj.Count() == 0)
                        db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id2 + "', '" + modelBase.ID_MODULO + "', " + ViewModelPermisos.ID_SERVICIOS_MODULOS + ", 1)");
                }
                else if (modelBase.permisos[i].ACTIVO = false & modelBase.permisos[i].DESCRIPCION == "CANCELAR")
                {
                    id2 = "04";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '04' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                    if (obj.Count() != 0)
                        db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET ACTIVO = 0 WHERE ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "'");
                }
                else if (modelBase.permisos[i].ACTIVO & modelBase.permisos[i].DESCRIPCION == "EXPORTAR")
                {
                    id2 = "05";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '05' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ACTIVO = 1'");
                    if (obj.Count() == 0)
                        db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id2 + "', '" + modelBase.ID_MODULO + "', " + ViewModelPermisos.ID_SERVICIOS_MODULOS + ", 1)");
                }
                else if (modelBase.permisos[i].ACTIVO == false & modelBase.permisos[i].DESCRIPCION == "EXPORTAR")
                {
                    id2 = "05";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '05' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                    if (obj.Count() != 0)
                        db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET ACTIVO = 0 WHERE ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "'");
                }
                else if (modelBase.permisos[i].ACTIVO & modelBase.permisos[i].DESCRIPCION == "IMPRIMIR")
                {
                    id2 = "06";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '06' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ACTIVO = 1'");
                    if (obj.Count() == 0)
                        db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id2 + "', '" + modelBase.ID_MODULO + "', " + ViewModelPermisos.ID_SERVICIOS_MODULOS + ", 1)");
                }
                else if (modelBase.permisos[i].ACTIVO == false & modelBase.permisos[i].DESCRIPCION == "IMPRIMIR")
                {
                    id2 = "06";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '06' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                    if (obj.Count() != 0)
                        db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET ACTIVO = 0 WHERE ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "'");
                }
                else if (modelBase.permisos[i].ACTIVO & modelBase.permisos[i].DESCRIPCION == "OCULTAR")
                {
                    id2 = "07";
                    var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '08' AND ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'");
                    if (obj.Count() == 0)
                    {
                        db.Database.ExecuteSqlCommand(query + "'" + ViewModelPermisos.ID_USUARIO + "', '" + ViewModelPermisos.ID_ROL + "', '" + id2 + "', '" + ViewModelPermisos.ID_MODULO + "', '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "', 1,'");
                    }
                    else
                    {
                        db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET ACTIVO = 0 WHERE ID_USUARIO ='" + ViewModelPermisos.ID_USUARIO + "' AND ID_ROL = '" + ViewModelPermisos.ID_ROL + "' AND ID_MODULO = '" + ViewModelPermisos.ID_MODULO + "'" + "' AND ID_SERVICIOS_MODULOS = '" + ViewModelPermisos.ID_SERVICIOS_MODULOS + "'");
                    }
                }
            }

            return View("Configuracion", modelBase);
        }

        [HttpPost]
        public JsonResult SaveList(string ItemList)
        {
            string[] arr = ItemList.Split(',');

            for (int i = 0; i < arr.Count(); i++)
            {
                CS_permisos_asignados entity = new CS_permisos_asignados();
                string idServicio = string.Empty;
                int? idRol = 0;
                string idServicioModulo = string.Empty;
                string idPermiso = string.Empty;
                string permiso = arr[i].Substring(0, arr[i].Length - 1);
                string moduloId = arr[i].Substring(arr[i].Length - 1);
                int idUsuario = Convert.ToInt32(Session["LoggedId"]);
                var idServicioResult = db.Database.SqlQuery<CS_usuarios>("SELECT * FROM CS_USUARIOS WHERE ID_USUARIO=" + idUsuario + "");
                foreach (var n in idServicioResult)
                {
                    idServicio = n.ID_SERVICIO.ToString();
                    idRol = n.ID_ROL;
                }
                var idServicioModuloResult = db.C_servicios_modulos.SqlQuery("SELECT * FROM C_SERVICIOS_MODULOS WHERE ID_SERVICIO='" + idServicio + "'");
                foreach (var n in idServicioModuloResult)
                {
                    idServicioModulo = n.id_servicios_modulos.ToString();
                }
                var idPermisoResult = db.CS_permisos.SqlQuery("SELECT * FROM CS_PERMISOS WHERE DESCRIPCION = '" + permiso + "'");
                foreach (var n in idPermisoResult)
                {
                    idPermiso = n.ID_PERMISO;
                }
                Random rd = new Random();
                int idPermisoAsignado = rd.Next(50, 3000);
                var obj = db.CS_permisos_asignados.SqlQuery("SELECT * FROM CS_PERMISOS_ASIGNADOS WHERE ID_PERMISO = '" + idPermiso + "' AND ID_USUARIO ='" + idUsuario + "' AND ID_SERVICIOS_MODULOS = '" + idServicioModulo + "' AND ID_ROL = '" + idRol + "' AND ID_MODULO = '" + moduloId + "' AND ACTIVO = '1'");
                if (obj.Count() == 0)
                {
                    entity.ID_USUARIO = idUsuario;
                    entity.ID_ROL = idRol;
                    entity.ID_PERMISO = idPermiso;
                    entity.ID_SERVICIOS_MODULOS = Convert.ToInt32(idServicioModulo);
                    entity.ACTIVO = true;
                    entity.ID_PERMISO_ASIGNADOS = idPermisoAsignado;
                    entity.ID_MODULO = Convert.ToInt32(moduloId);                  
                    db.CS_permisos_asignados.Add(entity);
                    db.SaveChanges();
                 }               
                else
                    db.Database.ExecuteSqlCommand("UPDATE CS_PERMISOS_ASIGNADOS SET ACTIVO = 0 WHERE ID_USUARIO ='" + idUsuario + "' AND ID_ROL = '" + idRol + "' AND ID_MODULO = '" + moduloId + "' AND ID_SERVICIOS_MODULOS = '" + idServicioModulo + "'");

            }

            return Json("", JsonRequestBehavior.AllowGet);
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
