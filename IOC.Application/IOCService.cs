
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
        Domain.Models.InvestmentMainModel IIOCServices.CalculateInvestmentShares(InvestmentMainModel investmentData)
        {
            InvestmentValidator IValidator = new InvestmentValidator();
            IValidator.Validate(investmentData, options => options.ThrowOnFailures());
            return this.iocRepository.CalculateInvestmentShares(investmentData);
        }
        Domain.Models.ROIModel IIOCServices.CalculateROI(InvestmentMainModel investmentData)
        {
            InvestmentValidator IValidator = new InvestmentValidator();
            IValidator.Validate(investmentData, options => options.ThrowOnFailures());
            return this.iocRepository.CalculateROI(investmentData);
        }
        Domain.Models.ROIModel IIOCServices.ValidateROIAmounts(ROIModel roiData)
        {
            ROICurrencyValidator RValidator = new ROICurrencyValidator();
            RValidator.Validate(roiData, options => options.ThrowOnFailures());
            return roiData;
        }
    }
}
