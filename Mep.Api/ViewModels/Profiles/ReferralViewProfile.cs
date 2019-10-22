using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ReferralViewProfile : Profile
    {
        public ReferralViewProfile()
        {
            CreateMap<BusinessModels.Referral, ReferralView>();
        }
    }
}