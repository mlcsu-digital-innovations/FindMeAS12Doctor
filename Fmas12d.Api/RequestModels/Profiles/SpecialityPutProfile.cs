using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class PutSpecialityProfile : Profile
    {
        public PutSpecialityProfile()
        {
            CreateMap<SpecialityPut, BusinessModels.Speciality>();
        }
    }
}