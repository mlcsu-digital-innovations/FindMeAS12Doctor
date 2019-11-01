using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.RequestModels.Profiles
{
    public class ReferralStatusPutProfile : Profile
    {
        public ReferralStatusPutProfile()
        {
          CreateMap<ReferralStatusPut, BusinessModels.ReferralStatus>();
        }
    }
}