using AutoMapper;
using Entities = Mep.Data.Entities;

namespace Mep.Business.Models.Profiles
{
    public class SpecialityProfile : Profile
    {
        public SpecialityProfile()
        {
            CreateMap<Entities.Speciality, Models.Speciality>()
                .ReverseMap();
        }
    }
}