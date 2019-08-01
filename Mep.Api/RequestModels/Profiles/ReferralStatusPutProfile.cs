using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.RequestModels.Profiles
{
    public class ReferralStatusPutProfile : Profile
    {
        public ReferralStatusPutProfile()
        {
          CreateMap<ReferralStatusPut, BusinessModels.ReferralStatus>();
        }
    }
}