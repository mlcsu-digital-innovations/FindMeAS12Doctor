using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class GenderTypeProfile : Profile
    {
        public GenderTypeProfile()
        {
            CreateMap<GenderType, BusinessModels.GenderType>();

            CreateMap<BusinessModels.GenderType, GenderType>();
        }
    }
}