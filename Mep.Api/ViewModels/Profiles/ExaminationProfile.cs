using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ExaminationProfile : Profile
    {
        public ExaminationProfile()
        {
            CreateMap<Referral, BusinessModels.Referral>();
            CreateMap<BusinessModels.Referral, Referral>();
        }
    }
}