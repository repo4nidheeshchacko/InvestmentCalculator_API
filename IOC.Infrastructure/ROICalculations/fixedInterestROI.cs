using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class fixedInterestROI : roiCalculator
    {
        protected override bool isInvestmentOptionSelected(roiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.investmentOptionId == Convert.ToInt32(investmentOptionsEnum.fixedInterest);
        }
        public override roiCalculateModel calculate(roiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Fixed Interest");
            roiCalculateModel roiCalculate = new roiCalculateModel();
            roiCalculate.calculatedROI = roiCalculateInput.investmentAmount * Convert.ToDecimal((10 / 100));
            roiCalculate.associatedFees = roiCalculate.calculatedROI * Convert.ToDecimal((1 / 100));
            return roiCalculate;
        }
    }
}
