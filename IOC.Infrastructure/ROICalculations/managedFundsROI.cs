using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class ManagedFundsROI : RoiCalculator
    {
        protected override bool IsInvestmentOptionSelected(RoiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.ManagedFunds);
        }
        public override RoiCalculateModel Calculate(RoiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Managed Funds");
            RoiCalculateModel roiCalculate = new RoiCalculateModel();
            roiCalculate.CalculatedROI = roiCalculateInput.InvestmentAmount * Convert.ToDecimal((12m/100m));
            roiCalculate.AssociatedFees = roiCalculate.CalculatedROI * Convert.ToDecimal((0.3m/ 100m));
            return roiCalculate;
        }
    }
}
