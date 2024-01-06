using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Application
{
    //This interface is use for Business Rule / USE CASE
    public interface IIOCServices
    {
        List<Domain.Models.investmentOptionsModel> getAllInvestmentOpions();
        Domain.Models.investmentViewModel calculateInvestmentShares(investmentViewModel investmentData);
        Domain.Models.roiModel calculateROI(investmentViewModel investmentData);
        Domain.Models.roiModel validateROIAmounts(roiModel roiData);
    }
}
