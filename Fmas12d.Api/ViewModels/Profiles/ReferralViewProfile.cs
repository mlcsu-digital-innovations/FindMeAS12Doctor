using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
    public class ReferralViewProfile : Profile
    {
        public ReferralViewProfile()
        {
            CreateMap<BusinessModels.Referral, ReferralView>();
        }
    }
}