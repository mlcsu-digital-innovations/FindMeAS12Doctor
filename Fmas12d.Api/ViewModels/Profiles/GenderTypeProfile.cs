using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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