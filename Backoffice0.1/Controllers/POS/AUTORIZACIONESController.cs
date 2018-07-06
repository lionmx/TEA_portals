using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backoffice0._1.Controllers.POS
{
    public class AUTORIZACIONESController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        // GET: SOLICITUD
        public ActionResult Index()
        {
            return View(); 
        }
        public PartialViewResult ConsultaMotivos(int id_tipo_solicitud)
        {
            var motivos = from sm in db.C_autorizacion_motivos
                          where sm.activo == true && sm.id_autorizacion_tipo == id_tipo_solicitud
                          select sm;

            return PartialView("../Autorizaciones/_Motivos", motivos);
        }
        public Double ConsultaCortesiaDescuento()
        {
            var cortesia = db.C_grupo_productos_g.Where(x => x.id_grupo_aplica_operacion == 7 && x.status == true).FirstOrDefault();
            
            var descuento = Convert.ToDouble(cortesia.descuento);
            return descuento;
        }
    }
}