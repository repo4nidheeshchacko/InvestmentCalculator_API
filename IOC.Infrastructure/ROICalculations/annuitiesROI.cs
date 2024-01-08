using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public class AnnuitiesROI : RoiCalculator
    {
        protected override bool IsInvestmentOptionSelected(RoiCalculateInputModel roiCalculateInput)
        {
            return roiCalculateInput.InvestmentOptionId== Convert.ToInt32(InvestmentOptionsEnum.Annuities);
        }
        public override RoiCalculateModel Calculate(RoiCalculateInputModel roiCalculateInput)
        {
            Console.WriteLine($"The calculation is by Annuities");
            RoiCalculateModel roiCalculate = new RoiCalculateModel();
            roiCalculate.CalculatedROI = roiCalculateInput.InvestmentAmount * Convert.ToDecimal((4 / 100));
            roiCalculate.AssociatedFees = roiCalculate.CalculatedROI * Convert.ToDecimal((1.4 / 100));
            return roiCalculate;
        }
    }
}
