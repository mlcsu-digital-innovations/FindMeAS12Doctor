using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class GenderTypePostProfile : Profile
    {
        public GenderTypePostProfile()
        {
            CreateMap<GenderTypePost, BusinessModels.GenderType>();
        }
    }
}