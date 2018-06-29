using Backoffice0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backoffice0._1.Controllers
{
    public class POS_PRENOMINAController : Controller
    {
        // GET: POS_PRENOMINA
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
        public ActionResult Index()
        {
           

            return View("../POS/Prenomina/Index");
        }
    }
}