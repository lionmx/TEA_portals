using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backoffice0._1.Models
{
    public class DeliveryModel
    {
        public int totalCocina { get; set; }

        public int totalRecibido { get; set; }

        public int totalPorAsignar { get; set; }

        public int totalEntregando { get; set; }

        public int totalEntregados { get; set; }

        public int totalRepaSuc { get; set; }
    }
}