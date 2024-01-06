using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Domain.Models
{
    public class investmentModel
    {
        public decimal investmentAmount { get; set; }
        public List<investmentShareModel> investmentShares { get; set; }
        public investmentModel()
        {
            investmentShares = new List<investmentShareModel>();
        }
    }
}
