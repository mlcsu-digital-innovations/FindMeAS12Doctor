using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class AssessmentProfile : Profile
    {
        public AssessmentProfile()
        {
            CreateMap<Assessment, BusinessModels.Assessment>();
            CreateMap<BusinessModels.Assessment, Assessment>();
        }
    }
}