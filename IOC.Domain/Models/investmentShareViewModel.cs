using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class investmentShareViewModel:investmentShareModel
    {
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal investedAmount { get; set; }
    }
}
