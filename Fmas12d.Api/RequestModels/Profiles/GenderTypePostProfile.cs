using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class GenderTypePostProfile : Profile
    {
        public GenderTypePostProfile()
        {
            CreateMap<GenderTypePost, BusinessModels.GenderType>();
        }
    }
}