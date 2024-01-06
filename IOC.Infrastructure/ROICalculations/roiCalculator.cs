using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public abstract class roiCalculator : IDisposable
    {
        public static roiCalculator getRoiCalculator(roiCalculateInputModel roiCalculateInput)
        {
            var instance = GetSuitableInstance(typeof(roiCalculator).FindSubClasses(), roiCalculateInput);
            return instance;
        }

        private static roiCalculator GetSuitableInstance(IEnumerable<Type> types, roiCalculateInputModel roiCalculateInput)
        {
            foreach (var @class in types)
            {
                try
                {
                    var instance = Activator.CreateInstance(@class) as roiCalculator;
                    var isSelected = instance.isInvestmentOptionSelected(roiCalculateInput);

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
                }
            }

            throw new NotImplementedException("System can not found any transporter for given order." + roiCalculateInput);
        }

        protected abstract bool isInvestmentOptionSelected(roiCalculateInputModel roiCalculateInput);
        public abstract roiCalculateModel calculate(roiCalculateInputModel roiCalculateInput);
        
        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
