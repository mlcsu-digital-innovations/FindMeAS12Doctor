using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class PutSpecialityProfile : Profile
    {
        public PutSpecialityProfile()
        {
            CreateMap<PutSpeciality, BusinessModels.Speciality>();
        }
    }
}