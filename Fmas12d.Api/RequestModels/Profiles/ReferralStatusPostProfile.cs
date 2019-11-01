using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class ReferralStatusPostProfile : Profile
    {
        public ReferralStatusPostProfile()
        {
          CreateMap<ReferralStatusPost, BusinessModels.ReferralStatus>();
        }
    }
}