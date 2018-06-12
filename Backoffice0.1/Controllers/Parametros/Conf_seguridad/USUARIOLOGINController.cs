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