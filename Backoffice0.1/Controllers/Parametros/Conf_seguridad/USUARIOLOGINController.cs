using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backoffice0._1.Models;
using Backoffice0._1.Helper;

namespace Backoffice0._1.Controllers
{
    public class USUARIOLOGINController : Controller
    {

        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();

        //GET: USUARIOLOGIN
        public ActionResult UsuarioLogin()
        {

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsuarioLogin(CS_usuario_login uSUARIO_LOGIN)
        {
            var encodingPasswordString = string.Empty;
            var items = db.CS_usuarios.Where(u => u.NOMBRE.Equals(uSUARIO_LOGIN.CS_usuarios.NOMBRE));
            if (items != null)
            {
                if (uSUARIO_LOGIN.PASS != null)
                {
                    encodingPasswordString = PasswordHelper.EncodePassword(uSUARIO_LOGIN.PASS, "MySalt");
                }

                foreach (var n in items)
                {
                    if (n.PASS != null)
                    {
                        if (uSUARIO_LOGIN.CS_usuarios.NOMBRE.Equals(n.NOMBRE) && n.PASS.Equals(encodingPasswordString))
                        {
                            Session["LoggedUser"] = n.NOMBRE;
                            Session["LoggedId"] = n.ID_USUARIO;
                            uSUARIO_LOGIN.ID_USUARIO = n.ID_USUARIO;
                            uSUARIO_LOGIN.PASS = encodingPasswordString;
                            uSUARIO_LOGIN.FECHA_LOGIN = DateTime.Now.ToString();
                        }
                        else
                        {
                            ViewBag.Message = "Usuario o contraseña incorrectos";
                            return View("UsuarioLogin");
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                //obtiene los permisos de cada servicio/modulo para el usuario loggeado           
                List<int> permisosLista = new List<int>();
                int loggedId = Convert.ToInt32(Session["LoggedId"].ToString());
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

                CS_usuario_login obj = new CS_usuario_login();
                obj.ID_USUARIO = uSUARIO_LOGIN.ID_USUARIO;
                obj.PASS = uSUARIO_LOGIN.PASS;
                obj.FECHA_LOGIN = uSUARIO_LOGIN.FECHA_LOGIN;
                db.CS_usuario_login.Add(obj);
                db.SaveChanges();
                return View("/Views/Home/Index.cshtml");

            }

            ViewBag.Message = "Usuario o contraseña incorrectos";
            return RedirectToAction("UsuarioLogin");
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