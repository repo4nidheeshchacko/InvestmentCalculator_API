using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class ExchangeTradedFundsROI : RoiCalculator
    {
        protected override bool IsInvestmentOptionSelected(RoiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.ExchangeTradedFunds);
        }
        public override RoiCalculateModel Calculate(RoiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Exchange Traded Funds");
            RoiCalculateModel roiCalculate = new RoiCalculateModel();
            if (roiCalculateInput.InvestmentPercentage <= 40)
            {
                roiCalculate.CalculatedROI = roiCalculateInput.InvestmentAmount * Convert.ToDecimal((12.8m/ 100m));
            }
            else if (roiCalculateInput.InvestmentPercentage > 40)
            {
                roiCalculate.CalculatedROI = roiCalculateInput.InvestmentAmount * Convert.ToDecimal((25m/100m));
            }
            roiCalculate.AssociatedFees = roiCalculate.CalculatedROI * Convert.ToDecimal((2m/ 100m));
            return roiCalculate;
        }
    }
}
