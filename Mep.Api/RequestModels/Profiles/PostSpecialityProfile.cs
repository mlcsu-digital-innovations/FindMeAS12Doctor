using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class PostSpecialityProfile : Profile
    {
        public PostSpecialityProfile()
        {
            CreateMap<PostSpeciality, BusinessModels.Speciality>();
        }
    }
}