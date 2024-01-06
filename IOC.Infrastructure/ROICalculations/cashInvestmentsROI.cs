using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class cashInvestmentsROI : roiCalculator
    {
        protected override bool isInvestmentOptionSelected(roiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.investmentOptionId == Convert.ToInt32(investmentOptionsEnum.cashInvestments);
        }
        public override roiCalculateModel calculate(roiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Cash Investment");
            roiCalculateModel roiCalculate = new roiCalculateModel();
            if (roiCalculateInput.investmentPercentage <= 50)
            {
                roiCalculate.calculatedROI = roiCalculateInput.investmentAmount * Convert.ToDecimal((8.5 / 100));
                roiCalculate.associatedFees = roiCalculate.calculatedROI * Convert.ToDecimal((0.5 / 100));
            }
            else if (roiCalculateInput.investmentPercentage > 50) //Considering only > condition as = condition is already done in the above loop
            {
                roiCalculate.calculatedROI = roiCalculateInput.investmentAmount * Convert.ToDecimal((10 / 100));
                roiCalculate.associatedFees = 0;
            }
            return roiCalculate;
        }
    }
}
