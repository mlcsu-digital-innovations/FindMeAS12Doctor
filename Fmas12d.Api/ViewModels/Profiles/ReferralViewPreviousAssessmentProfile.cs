using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class ReferralViewPreviousAssessmentProfile : Profile
    {
        public ReferralViewPreviousAssessmentProfile()
        {
            CreateMap<BusinessModels.Assessment, ReferralViewPreviousAssessment>();
        }
    }
}