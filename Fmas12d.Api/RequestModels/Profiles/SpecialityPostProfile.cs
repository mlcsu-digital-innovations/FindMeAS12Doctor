using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class SpecialityPostProfile : Profile
    {
        public SpecialityPostProfile()
        {
            CreateMap<SpecialityPost, BusinessModels.Speciality>();
        }
    }
}