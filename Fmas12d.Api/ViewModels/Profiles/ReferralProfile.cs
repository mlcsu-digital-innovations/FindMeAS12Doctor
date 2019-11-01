using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ReferralProfile : Profile
    {
        public ReferralProfile()
        {
            CreateMap<Referral, BusinessModels.Referral>();
            CreateMap<BusinessModels.Referral, Referral>();
        }
    }
}