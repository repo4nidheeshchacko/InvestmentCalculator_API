using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Application
{
    public interface IIOCRepository
    {
        List<Domain.Models.investmentOptionsModel> getAllInvestmentOpions();
        Domain.Models.investmentViewModel calculateInvestmentShares(investmentViewModel investmentData);
        Domain.Models.roiModel calculateROI(investmentViewModel investmentData);
    }
}
