using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Backoffice0._1.Models;
using Backoffice0._1.Helper;

namespace Backoffice0._1.Controllers
{
    public class BACKORDERController : Controller
    {
        // GET: BACKORDER
        private DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
       
        public ActionResult Index()
        {
     
            return View();
        }
    }
}