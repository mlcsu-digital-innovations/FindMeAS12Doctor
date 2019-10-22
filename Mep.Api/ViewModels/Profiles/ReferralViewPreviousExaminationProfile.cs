using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ReferralViewPreviousExaminationProfile : Profile
    {
        public ReferralViewPreviousExaminationProfile()
        {
            CreateMap<BusinessModels.Examination, ReferralViewPreviousExamination>();
        }
    }
}