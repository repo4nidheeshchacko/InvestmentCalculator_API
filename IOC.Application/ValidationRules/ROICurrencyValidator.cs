using FluentValidation;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Application.ValidationRules
{
    public class roiCurrencyValidator : AbstractValidator<roiModel>
    {
        public roiCurrencyValidator()
        {
            RuleFor(x => x.projectedReturn).NotEmpty().WithMessage("Yearly Return Amount should not be Empty");
            RuleFor(x => x.totalFees).NotEqual(0).When(x => x.totalFees>0).WithMessage("Total Fees should be valid amount");
        }
    }
}
