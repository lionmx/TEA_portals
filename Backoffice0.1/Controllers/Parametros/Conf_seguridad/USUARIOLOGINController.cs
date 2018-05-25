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
      
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
        
        //GET: USUARIOLOGIN
        public ActionResult UsuarioLogin()
        {

            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsuarioLogin( CS_usuario_login uSUARIO_LOGIN)
        {
            var encodingPasswordString = string.Empty;
            var items = db.CS_usuarios.Where(u => u.NOMBRE == uSUARIO_LOGIN.ID_USUARIO);
            if (items != null)
            {
                if (uSUARIO_LOGIN.PASS != null)
                {
                     encodingPasswordString = PasswordHelper.EncodePassword(uSUARIO_LOGIN.PASS, "MySalt");
                }
      
                foreach (var n in items)
            {
                if (uSUARIO_LOGIN.ID_USUARIO == n.NOMBRE && n.PASS.Equals(encodingPasswordString))
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
            if (ModelState.IsValid)
            {
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

                db.CS_usuario_login.Add(uSUARIO_LOGIN);
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