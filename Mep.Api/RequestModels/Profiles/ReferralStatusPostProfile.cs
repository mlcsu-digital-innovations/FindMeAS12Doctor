using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class ReferralStatusPostProfile : Profile
    {
        public ReferralStatusPostProfile()
        {
          CreateMap<ReferralStatusPost, BusinessModels.ReferralStatus>();
        }
    }
}