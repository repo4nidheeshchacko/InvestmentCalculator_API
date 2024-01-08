using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class RoiCalculateInputModel
    {
        public decimal InvestmentAmount { get; set; }
        public decimal InvestmentPercentage { get; set; }
        public double InvestmentOptionId { get; set; }
    }
}
