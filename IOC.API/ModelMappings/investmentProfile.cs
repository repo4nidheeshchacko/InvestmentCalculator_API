using AutoMapper;
using IOC.Domain.Models;

namespace IOC.API.ModelMappings
{
    public class InvestmentProfile:Profile
    {
        public InvestmentProfile()
        {
            CreateMap<InvestmentModel, InvestmentViewModel>();
            CreateMap<InvestmentShareModel, InvestmentShareViewModel>();
        }
    }
}
