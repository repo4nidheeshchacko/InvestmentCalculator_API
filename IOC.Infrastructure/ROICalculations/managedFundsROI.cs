using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class managedFundsROI : roiCalculator
    {
        protected override bool isInvestmentOptionSelected(roiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.investmentOptionId == Convert.ToInt32(investmentOptionsEnum.managedFunds);
        }
        public override roiCalculateModel calculate(roiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Managed Funds");
            roiCalculateModel roiCalculate = new roiCalculateModel();
            roiCalculate.calculatedROI = roiCalculateInput.investmentAmount * Convert.ToDecimal((12 / 100));
            roiCalculate.associatedFees = roiCalculate.calculatedROI * Convert.ToDecimal((0.3 / 100));
            return roiCalculate;
        }
    }
}
