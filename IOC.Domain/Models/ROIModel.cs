using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class ROIModel
    {
        public decimal ProjectedReturn { get; set; }
        public decimal TotalFees { get; set; }
        public string CurrencyCode { get; set; }
    }
}
