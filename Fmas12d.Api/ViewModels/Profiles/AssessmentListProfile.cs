using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class AssessmentListProfile : Profile
    {
        public AssessmentListProfile()
        {
            CreateMap<AssessmentList, BusinessModels.Assessment>();
            CreateMap<BusinessModels.Assessment, AssessmentList>();
        }
    }
}