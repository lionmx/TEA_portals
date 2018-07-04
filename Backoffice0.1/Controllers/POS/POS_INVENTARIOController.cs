using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Backoffice0._1.Controllers.POS
{
    public class POS_INVENTARIOController : Controller
    {
        private DB_CORPORATIVA_DEVEntities db = new DB_CORPORATIVA_DEVEntities();
        public ActionResult Index()
        {
           
            return View("../POS/Inventario/Index");
        }
    }
}
