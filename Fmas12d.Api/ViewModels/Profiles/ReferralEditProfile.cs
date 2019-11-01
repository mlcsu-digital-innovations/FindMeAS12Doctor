using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ReferralEditProfile : Profile
    {
        public ReferralEditProfile()
        {
            CreateMap<BusinessModels.Referral, ReferralEdit>();
        }
    }
}