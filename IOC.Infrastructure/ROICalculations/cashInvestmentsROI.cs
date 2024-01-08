using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class CashInvestmentsROI : RoiCalculator
    {
        protected override bool IsInvestmentOptionSelected(RoiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.CashInvestments);
        }
        public override RoiCalculateModel Calculate(RoiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Cash Investment");
            RoiCalculateModel roiCalculate = new RoiCalculateModel();
            if (roiCalculateInput.InvestmentPercentage <= 50)
            {
                roiCalculate.CalculatedROI = roiCalculateInput.InvestmentAmount * Convert.ToDecimal((8.5m/ 100m));
                roiCalculate.AssociatedFees = roiCalculate.CalculatedROI * Convert.ToDecimal((0.5m/ 100m));
            }
            else if (roiCalculateInput.InvestmentPercentage > 50) //Considering only > condition as = condition is already done in the above loop
            {
                roiCalculate.CalculatedROI = roiCalculateInput.InvestmentAmount * Convert.ToDecimal((10m/ 100m));
                roiCalculate.AssociatedFees = 0;
            }
            return roiCalculate;
        }
    }
}
