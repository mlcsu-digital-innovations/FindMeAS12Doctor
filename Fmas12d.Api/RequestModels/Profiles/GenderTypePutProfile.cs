using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class GenderTypePutProfile : Profile
    {
        public GenderTypePutProfile()
        {
            CreateMap<GenderTypePut, BusinessModels.GenderType>();
        }
    }
}