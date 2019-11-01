using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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