using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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