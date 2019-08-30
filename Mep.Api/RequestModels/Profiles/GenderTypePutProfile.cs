using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class GenderTypePutProfile : Profile
    {
        public GenderTypePutProfile()
        {
            CreateMap<GenderTypePut, BusinessModels.GenderType>();
        }
    }
}