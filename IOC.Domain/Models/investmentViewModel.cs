using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class investmentViewModel
    {
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal investmentAmount { get; set; }
        public List<investmentShareViewModel> investmentShares { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal availableAmount { get; set; }
        public investmentViewModel()
        {
            investmentShares = new List<investmentShareViewModel>();
        }
    }
}
