
using FluentValidation;
using IOC.Application.ValidationRules;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Application
{
    //Implement Bussiness Rule / USE CASES
    public class IOCService : IIOCServices
    {
        private readonly IIOCRepository iocRepository;
        public IOCService(IIOCRepository iocRepository)
        {
            this.iocRepository = iocRepository;
        }
        List<Domain.Models.InvestmentOptionsModel> IIOCServices.GetAllInvestmentOpions()
        {
            return this.iocRepository.GetAllInvestmentOpions();
        }
        Domain.Models.InvestmentViewModel IIOCServices.CalculateInvestmentShares(InvestmentViewModel investmentData)
        {
            InvestmentValidator iValidator = new InvestmentValidator();
            iValidator.Validate(investmentData, options => options.ThrowOnFailures());
            return this.iocRepository.CalculateInvestmentShares(investmentData);
        }
        Domain.Models.RoiModel IIOCServices.CalculateROI(InvestmentViewModel investmentData)
        {
            InvestmentValidator iValidator = new InvestmentValidator();
            iValidator.Validate(investmentData, options => options.ThrowOnFailures());
            return this.iocRepository.CalculateROI(investmentData);
        }
    }
}
