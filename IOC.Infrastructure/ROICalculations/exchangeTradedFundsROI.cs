using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class exchangeTradedFundsROI : roiCalculator
    {
        protected override bool isInvestmentOptionSelected(roiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.investmentOptionId == Convert.ToInt32(investmentOptionsEnum.exchangeTradedFunds);
        }
        public override roiCalculateModel calculate(roiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Exchange Traded Funds");
            roiCalculateModel roiCalculate = new roiCalculateModel();
            if (roiCalculateInput.investmentPercentage <= 40)
            {
                roiCalculate.calculatedROI = roiCalculateInput.investmentAmount * Convert.ToDecimal((12.8 / 100));
            }
            else if (roiCalculateInput.investmentPercentage > 40)
            {
                roiCalculate.calculatedROI = roiCalculateInput.investmentAmount * Convert.ToDecimal((25 / 100));
            }
            roiCalculate.associatedFees = roiCalculate.calculatedROI * Convert.ToDecimal((2 / 100));
            return roiCalculate;
        }
    }
}
