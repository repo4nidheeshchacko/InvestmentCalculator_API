using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class roiCalculateInputModel
    {
        public decimal investmentAmount { get; set; }
        public decimal investmentPercentage { get; set; }
        public double investmentOptionId { get; set; }
    }
}
