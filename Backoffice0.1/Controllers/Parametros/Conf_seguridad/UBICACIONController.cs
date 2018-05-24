using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Backoffice0._1.Controllers
{
    public class UBICACIONController : Controller
    {
        //private QUESIPIZZAS_DEV1Entities2 db = new QUESIPIZZAS_DEV1Entities2();
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();

        // GET: UBICACION
        public ActionResult Index()
        {
            #region Viewbags 

            CS_permisos_asignados ViewModel = new CS_permisos_asignados();
            List<string> listaPA = new List<string>();
            string loggedId = Session["LoggedId"].ToString();
            for (int i = 00; i <= 8; i++)
            {
                var mod1 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "0" + i && a.ID_PERMISO == "07").FirstOrDefault();
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
                var mod2 = db.CS_permisos_asignados.Where(a => a.ID_USUARIO.Equals(loggedId) && a.ID_MODULO == "'0" + i + "'" && a.ID_PERMISO.Equals("08")).FirstOrDefault();
                var perfil = db.CS_usuarios.Where(a => a.ID_USUARIO.Equals(loggedId)).FirstOrDefault();
                if (mod2 != null)
                {
                    listaPA2.Add(mod2.ID_MODULO);
                    listaPA2.Add(mod2.ID_PERMISO);

                }
            }
            ViewBag.data2 = listaPA2;
            #endregion

            M_callcenter_clientes cc = new M_callcenter_clientes();
            var usuarios = db.M_callcenter_clientes.SqlQuery("SELECT * FROM dbo.M_callcenter_clientes").ToList();
           // cc.callcenterClientesList = usuarios;
            return View(cc);
        }
    }
}