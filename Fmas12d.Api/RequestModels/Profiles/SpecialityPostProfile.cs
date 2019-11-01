using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class SpecialityPostProfile : Profile
    {
        public SpecialityPostProfile()
        {
            CreateMap<SpecialityPost, BusinessModels.Speciality>();
        }
    }
}