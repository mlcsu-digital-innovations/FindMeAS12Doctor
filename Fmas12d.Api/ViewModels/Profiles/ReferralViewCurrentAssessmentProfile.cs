using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class ReferralViewCurrentAssessmentProfile : Profile
    {
        public ReferralViewCurrentAssessmentProfile()
        {
            CreateMap<BusinessModels.Assessment, ReferralViewCurrentAssessment>();
        }
    }
}