using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class SharesROI : RoiCalculator
    {
        protected override bool IsInvestmentOptionSelected(RoiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.InvestmentOptionId == Convert.ToInt32(InvestmentOptionsEnum.Shares);
        }
        public override RoiCalculateModel Calculate(RoiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Shares");
            RoiCalculateModel roiCalculate = new RoiCalculateModel();
            if (roiCalculateInput.InvestmentPercentage <= 70)
            {
                roiCalculate.CalculatedROI = roiCalculateInput.InvestmentAmount * Convert.ToDecimal((4.3 / 100));
            }
            else if (roiCalculateInput.InvestmentPercentage > 70)
            {
                roiCalculate.CalculatedROI = roiCalculateInput.InvestmentAmount * Convert.ToDecimal((6 / 100));
            }
            roiCalculate.AssociatedFees = roiCalculate.CalculatedROI * Convert.ToDecimal((2.5 / 100));
            return roiCalculate;
        }
    }
}
