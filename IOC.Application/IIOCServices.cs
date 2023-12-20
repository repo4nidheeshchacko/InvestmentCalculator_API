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
        List<Domain.Models.InvestmentOptionsModel> GetAllInvestmentOpions();
        Domain.Models.InvestmentMainModel CalculateInvestmentShares(InvestmentMainModel investmentData);
        Domain.Models.ROIModel CalculateROI(InvestmentMainModel investmentData);
        Domain.Models.ROIModel ValidateROIAmounts(ROIModel roiData);
    }
}
