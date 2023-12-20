using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class InvestmentMainModel
    {
        public decimal InvestmentAmount { get; set; }
        public List<InvestmentShareModel> InvestmentShares { get; set; }
        public decimal AvailableAmount { get; set; }
        public InvestmentMainModel()
        {
            InvestmentShares = new List<InvestmentShareModel>();
        }
    }
}
