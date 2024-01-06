using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class roiModel
    {
        public decimal projectedReturn { get; set; }
        public decimal totalFees { get; set; }
        public string convertedCurrencyCode { get; set; }
        public decimal convertedROI { get; set; }
    }
}
