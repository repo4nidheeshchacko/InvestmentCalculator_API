using FluentValidation;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Application.ValidationRules
{
    public class InvestmentValidator : AbstractValidator<InvestmentMainModel>
    {
        public InvestmentValidator()
        {
            RuleFor(x => x.InvestmentAmount).Must(BeAValidAmount).WithMessage("Please provide a valid Investment Amount");
            RuleFor(x => x.InvestmentShares).NotEmpty().WithMessage("Please choose atleast one investment option");
            RuleFor(x => x.InvestmentShares).Must(BeAValidTotalPercentage).WithMessage("Total Investment percentage should not exceed than 100");
            RuleForEach(x => x.InvestmentShares).Must(y => y.InvestmentOptionId > 0).WithMessage("Please choose an Investment Option in {CollectionIndex}");
            RuleForEach(x => x.InvestmentShares).Must(y => y.InvestmentPercentage>0 && y.InvestmentPercentage<=100).WithMessage("Please provide a valid percentage in {CollectionIndex}");
        }
        private bool BeAValidAmount(decimal amount)
        {
            // custom amount validating logic goes here
            if (amount <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool BeAValidTotalPercentage(List<InvestmentShareModel> InvestmentShares)
        {
            // custom amount validating logic goes here
            decimal percentageSum = 0;
            foreach (InvestmentShareModel row in InvestmentShares)
            {
                percentageSum = percentageSum + row.InvestmentPercentage;
            }
            if (percentageSum > 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
