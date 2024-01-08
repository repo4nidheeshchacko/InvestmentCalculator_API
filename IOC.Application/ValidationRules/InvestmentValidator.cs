using FluentValidation;
using IOC.Domain.Enums;
using IOC.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Application.ValidationRules
{
    public class InvestmentValidator : AbstractValidator<InvestmentViewModel>
    {
        public InvestmentValidator()
        {
            RuleFor(x => x.InvestmentAmount).Must(BeAValidAmount).WithMessage("Investment Amount should be valid number or decimal")
                                            .GreaterThan(0).WithMessage("Please provide a valid Investment Amount");
            RuleFor(x => x.InvestmentShares).NotEmpty().WithMessage("Please choose atleast one investment option")
                                            .Must(BeAValidTotalPercentage).WithMessage("Total Investment percentage should not exceed than 100");
            RuleForEach(x => x.InvestmentShares).Must((x,y) => 
                                                        (Convert.ToInt32(y.InvestmentOptionId) > 0) && 
                                                        (Enum.IsDefined(typeof(InvestmentOptionsEnum), y.InvestmentOptionId))
                                                    ).WithMessage("Please provide a valid Investment Option in {CollectionIndex}")
                                                 .Must((x, y) =>
                                                       (x.InvestmentShares.FindAll(sx => sx.InvestmentOptionId == y.InvestmentOptionId).Count<=1)
                                                    ).WithMessage("Duplicate Investment Option is not allowed");
            RuleForEach(x => x.InvestmentShares).Must(y => Convert.ToDecimal(y.InvestmentPercentage)>0 && Convert.ToDecimal(y.InvestmentPercentage)<=100).WithMessage("Please provide a valid percentage in {CollectionIndex}");
        }
        private bool BeAValidAmount(decimal amount)
        {
            // custom amount validating logic goes here
            if (!decimal.TryParse(amount.ToString(), out decimal number))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool BeAValidTotalPercentage(List<InvestmentShareViewModel> investmentShares)
        {
            // custom amount validating logic goes here
            decimal percentageSum = 0;
            foreach (InvestmentShareModel row in investmentShares)
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
