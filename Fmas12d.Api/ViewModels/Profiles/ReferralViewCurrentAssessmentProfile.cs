using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class ReferralViewCurrentExaminationProfile : Profile
    {
        public ReferralViewCurrentExaminationProfile()
        {
            CreateMap<BusinessModels.Examination, ReferralViewCurrentExamination>();
        }
    }
}