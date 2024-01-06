using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class sharesROI : roiCalculator
    {
        protected override bool isInvestmentOptionSelected(roiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.investmentOptionId == Convert.ToInt32(investmentOptionsEnum.shares);
        }
        public override roiCalculateModel calculate(roiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Shares");
            roiCalculateModel roiCalculate = new roiCalculateModel();
            if (roiCalculateInput.investmentPercentage <= 70)
            {
                roiCalculate.calculatedROI = roiCalculateInput.investmentAmount * Convert.ToDecimal((4.3 / 100));
            }
            else if (roiCalculateInput.investmentPercentage > 70)
            {
                roiCalculate.calculatedROI = roiCalculateInput.investmentAmount * Convert.ToDecimal((6 / 100));
            }
            roiCalculate.associatedFees = roiCalculate.calculatedROI * Convert.ToDecimal((2.5 / 100));
            return roiCalculate;
        }
    }
}
