using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class ReferralViewPreviousExaminationProfile : Profile
    {
        public ReferralViewPreviousExaminationProfile()
        {
            CreateMap<BusinessModels.Examination, ReferralViewPreviousExamination>();
        }
    }
}