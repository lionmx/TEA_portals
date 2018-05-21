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
        //private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
        // GET: USUARIOLOGIN
        //public ActionResult Index()
        //{
        //    return View();
        //}

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
               CS_permisos_asignados ViewModel = new CS_permisos_asignados();
                List<string> listaPA = new List<string>();
                string loggedId = Session["LoggedId"].ToString();
               for (int i = 00; i <= 8; i++)
                {
                    var mod1 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "0"+i && a.ID_PERMISO == "07").FirstOrDefault();
                    var perfil = db.CS_usuarios.Where(a => a.ID_USUARIO.Equals(loggedId)).FirstOrDefault();
                    if (mod1 != null)
                    {
                        listaPA.Add(mod1.ID_MODULO);
                        listaPA.Add(mod1.ID_PERMISO);
                        ViewBag.perfil = perfil.ID_PERFIL;
                    }
                }
                ViewBag.data = listaPA;
                List<string> listaPA2 = new List<string>();
               for (int i = 0; i <= 8; i++)
                {
                    var mod2 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "'0" + i + "'"  && a.ID_PERMISO.Equals("08")).FirstOrDefault();
                    var perfil = db.CS_usuarios.Where(a => a.ID_USUARIO.Equals(loggedId)).FirstOrDefault();
                    if (mod2 != null)
                    {
                        listaPA2.Add(mod2.ID_MODULO);
                        listaPA2.Add(mod2.ID_PERMISO);
                        
                    }
                }
                ViewBag.data2 = listaPA2;                             
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