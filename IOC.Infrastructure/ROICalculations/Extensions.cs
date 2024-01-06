using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Infrastructure.ROICalculations
{
    public static class Extensions
    {
        public static IEnumerable<Type> FindSubClasses(this Type baseType)
        {
            var assembly = baseType.Assembly;

            return assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));
        }
    }
}
