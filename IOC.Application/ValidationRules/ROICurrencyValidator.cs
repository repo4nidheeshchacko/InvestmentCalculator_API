using FluentValidation;
using IOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Application.ValidationRules
{
    public class ROICurrencyValidator : AbstractValidator<ROIModel>
    {
        public ROICurrencyValidator()
        {
            RuleFor(x => x.ProjectedReturn).NotEmpty().WithMessage("Yearly Return Amount should not be Empty");
            RuleFor(x => x.TotalFees).NotEqual(0).When(x => x.TotalFees>0).WithMessage("Total Fees should be valid amount");
        }
    }
}
