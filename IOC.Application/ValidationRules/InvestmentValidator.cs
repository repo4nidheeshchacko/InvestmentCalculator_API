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
    public class investmentValidator : AbstractValidator<investmentViewModel>
    {
        public investmentValidator()
        {
            RuleFor(x => x.investmentAmount).Must(beAValidAmount).WithMessage("Investment Amount should be valid number or decimal")
                                            .GreaterThan(0).WithMessage("Please provide a valid Investment Amount");
            RuleFor(x => x.investmentShares).NotEmpty().WithMessage("Please choose atleast one investment option")
                                            .Must(beAValidTotalPercentage).WithMessage("Total Investment percentage should not exceed than 100");
            RuleForEach(x => x.investmentShares).Must((x,y) => 
                                                        (Convert.ToInt32(y.investmentOptionId) > 0) && 
                                                        (Enum.IsDefined(typeof(investmentOptionsEnum), y.investmentOptionId))
                                                    ).WithMessage("Please provide a valid Investment Option in {CollectionIndex}")
                                                 .Must((x, y) =>
                                                       (x.investmentShares.FindAll(sx => sx.investmentOptionId == y.investmentOptionId).Count<=1)
                                                    ).WithMessage("Duplicate Investment Option is not allowed");
            RuleForEach(x => x.investmentShares).Must(y => Convert.ToDecimal(y.investmentPercentage)>0 && Convert.ToDecimal(y.investmentPercentage)<=100).WithMessage("Please provide a valid percentage in {CollectionIndex}");
        }
        private bool beAValidAmount(decimal amount)
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
        private bool beAValidTotalPercentage(List<investmentShareViewModel> investmentShares)
        {
            // custom amount validating logic goes here
            decimal percentageSum = 0;
            foreach (investmentShareModel row in investmentShares)
            {
                percentageSum = percentageSum + row.investmentPercentage;
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
