using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class InvestmentViewModel
    {
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal InvestmentAmount { get; set; }
        public List<InvestmentShareViewModel> InvestmentShares { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal AvailableAmount { get; set; }
        public InvestmentViewModel()
        {
            InvestmentShares = new List<InvestmentShareViewModel>();
        }
    }
}
