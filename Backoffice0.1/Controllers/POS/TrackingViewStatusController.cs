using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Backoffice0._1.Controllers.POS
{
    public class TrackingViewStatusController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        bool cambio;
        string status;
        // GET: TrackingViewStatus
        public ActionResult Index()
        {
            return View("/Views/POS/Ventas/TrackingViewStatus.cshtml");
        }

        [HttpPost]
        public ActionResult iniciaHilo(FormCollection form, C_tracking_status obj)
        {
            status = form["boton"]; 
            object model = obj;
            //ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(calcular);
            Thread hiloPrincipal = new Thread(calcular);
            hiloPrincipal.Start();
            hiloPrincipal.Join();
            if (cambio)
            {
                ViewBag.status = status;
            }
            return View("/Views/POS/Ventas/TrackingViewStatus.cshtml");
        }

        public void calcular()
        {

            Thread.Sleep(2000);
            cambio = true;
        }
    }
}