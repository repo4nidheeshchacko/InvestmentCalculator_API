using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class InvestmentShareModel
    {
        public int InvestmentOptionId { get; set; }
        public decimal InvestmentPercentage { get; set; }
        public decimal InvestedAmount { get; set; }
    }
}
