using AutoMapper;
using IOC.Domain.Models;

namespace IOC.API.ModelMappings
{
    public class investmentProfile:Profile
    {
        public investmentProfile()
        {
            CreateMap<investmentModel, investmentViewModel>();
            CreateMap<investmentShareModel, investmentShareViewModel>();
        }
    }
}
