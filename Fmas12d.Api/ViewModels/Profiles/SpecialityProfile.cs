using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class SpecialityProfile : Profile
    {
        public SpecialityProfile()
        {
            CreateMap<Speciality, BusinessModels.Speciality>()
                .ReverseMap();
        }
    }
}