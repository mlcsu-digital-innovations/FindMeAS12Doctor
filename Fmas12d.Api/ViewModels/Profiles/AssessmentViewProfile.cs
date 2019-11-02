using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class AssessmentViewProfile : Profile
    {
        public AssessmentViewProfile()
        {
            CreateMap<BusinessModels.Assessment, AssessmentView>();
        }
    }
}