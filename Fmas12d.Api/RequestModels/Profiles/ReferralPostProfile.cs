using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class ReferralPostProfile : Profile
    {
        public ReferralPostProfile()
        {
            CreateMap<ReferralPost, BusinessModels.Referral>();
        }
    }
}