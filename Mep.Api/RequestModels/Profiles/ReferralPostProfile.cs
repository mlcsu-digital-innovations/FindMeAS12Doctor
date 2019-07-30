using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class ReferralPostProfile : Profile
    {
        public ReferralPostProfile()
        {
            CreateMap<ReferralPost, BusinessModels.Referral>();
        }
    }
}