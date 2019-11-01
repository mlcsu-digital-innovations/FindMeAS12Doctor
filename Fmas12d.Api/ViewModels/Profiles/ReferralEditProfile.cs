using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class ReferralEditProfile : Profile
    {
        public ReferralEditProfile()
        {
            CreateMap<BusinessModels.Referral, ReferralEdit>();
        }
    }
}