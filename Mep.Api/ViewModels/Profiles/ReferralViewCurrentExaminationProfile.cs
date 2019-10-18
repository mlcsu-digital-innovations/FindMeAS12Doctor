using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ReferralViewCurrentExaminationProfile : Profile
    {
        public ReferralViewCurrentExaminationProfile()
        {
            CreateMap<BusinessModels.Examination, ReferralViewCurrentExamination>();
        }
    }
}