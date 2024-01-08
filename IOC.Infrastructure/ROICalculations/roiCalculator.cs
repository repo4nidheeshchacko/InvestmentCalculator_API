using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public abstract class RoiCalculator : IDisposable
    {
        public static RoiCalculator GetRoiCalculator(RoiCalculateInputModel roiCalculateInput)
        {
            var instance = GetSuitableInstance(typeof(RoiCalculator).FindSubClasses(), roiCalculateInput);
            return instance;
        }

        private static RoiCalculator GetSuitableInstance(IEnumerable<Type> types, RoiCalculateInputModel roiCalculateInput)
        {
            foreach (var @class in types)
            {
                try
                {
                    var instance = Activator.CreateInstance(@class) as RoiCalculator;
                    var isSelected = instance.IsInvestmentOptionSelected(roiCalculateInput);

                    if (isSelected != true)
                    {
                        instance.Dispose();
                        continue;
                    }

                    return instance;
                }
                catch (System.Exception ex)
                {
                    continue;
                    throw ex;
                }
            }

            throw new NotImplementedException("System can not found any transporter for given order." + roiCalculateInput);
        }

        protected abstract bool IsInvestmentOptionSelected(RoiCalculateInputModel roiCalculateInput);
        public abstract RoiCalculateModel Calculate(RoiCalculateInputModel roiCalculateInput);
        
        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
