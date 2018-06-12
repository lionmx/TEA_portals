using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using Backoffice0._1.Models;


namespace Backoffice0._1.Controllers.POS
{

    public static class TrackingStatus
    {
        private static DB_CORPORATIVA_DEVEntities1 db = new DB_CORPORATIVA_DEVEntities1();
        //public  async Task<bool> estado()
        //{
        //    Thread t = new Thread(() => {
                //Thread.Start();
        //})
        //}

        public static bool estadoCambio(C_tracking_status status)
        {
            var original = db.C_tracking_status.Find(status.C_id_tracking_status);
            bool cambio = original.nombre_tracking_status != status.nombre_tracking_status;
            if (cambio)
            {
                return true;

            }
            else
                return false;
        }
    }
}