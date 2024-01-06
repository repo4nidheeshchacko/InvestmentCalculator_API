
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
        List<Domain.Models.investmentOptionsModel> IIOCServices.getAllInvestmentOpions()
        {
            return this.iocRepository.getAllInvestmentOpions();
        }
        Domain.Models.investmentViewModel IIOCServices.calculateInvestmentShares(investmentViewModel investmentData)
        {
            investmentValidator IValidator = new investmentValidator();
            IValidator.Validate(investmentData, options => options.ThrowOnFailures());
            return this.iocRepository.calculateInvestmentShares(investmentData);
        }
        Domain.Models.roiModel IIOCServices.calculateROI(investmentViewModel investmentData)
        {
            investmentValidator IValidator = new investmentValidator();
            IValidator.Validate(investmentData, options => options.ThrowOnFailures());
            return this.iocRepository.calculateROI(investmentData);
        }
        Domain.Models.roiModel IIOCServices.validateROIAmounts(roiModel roiData)
        {
            roiCurrencyValidator RValidator = new roiCurrencyValidator();
            RValidator.Validate(roiData, options => options.ThrowOnFailures());
            return roiData;
        }
    }
}
