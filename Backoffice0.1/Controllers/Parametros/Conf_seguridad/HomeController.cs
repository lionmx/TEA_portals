using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backoffice0._1.Controllers
{
    public class HomeController : Controller
    {
       
        // GET: Home
        public ActionResult Index()
        {
            //#region Viewbags 

            //PERMISOS_ASIGNADOS ViewModel = new PERMISOS_ASIGNADOS();
            //List<string> listaPA = new List<string>();
            //string user = Session["LoggedUser"].ToString();
            //var id = from us in db.USUARIOS
            //         where us.NOMBRE.Equals(user)
            //         select us;

            //foreach (var i in id)
            //{
            //    user = i.ID_USUARIO;
            //}
            //for (int i = 00; i <= 8; i++)
            //{
            //    var mod1 = db.PERMISOS_ASIGNADOS.Where(a => a.ID_USUARIO.Equals(user) && a.ID_MODULO == "0" + i && a.ID_PERMISO == "07").FirstOrDefault();
            //    if (mod1 != null)
            //    {
            //        listaPA.Add(mod1.ID_MODULO);
            //        listaPA.Add(mod1.ID_PERMISO);

            //    }
            //}
            //ViewBag.data = listaPA;
            //List<string> listaPA2 = new List<string>();
            //for (int i = 0; i <= 8; i++)
            //{
            //    var mod2 = db.PERMISOS_ASIGNADOS.Where(a => a.ID_USUARIO.Equals(user) && a.ID_MODULO == "'0" + i + "'" && a.ID_PERMISO.Equals("08")).FirstOrDefault();
            //    if (mod2 != null)
            //    {
            //        listaPA2.Add(mod2.ID_MODULO);
            //        listaPA2.Add(mod2.ID_PERMISO);
            //    }
            //}
            //ViewBag.data2 = listaPA2;

            //#endregion

            return View();
        }

       
    }
}