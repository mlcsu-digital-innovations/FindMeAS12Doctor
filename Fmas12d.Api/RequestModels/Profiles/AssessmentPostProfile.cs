using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class AssessmentPostProfile : Profile
    {
        public AssessmentPostProfile()
        {
            CreateMap<Assessment, BusinessModels.Assessment>();
        }
    }
}