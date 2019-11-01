using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
    public class ReferralStatusProfile : Profile
    {
        public ReferralStatusProfile()
        {
            CreateMap<ReferralStatus, BusinessModels.ReferralStatus>();
            CreateMap<BusinessModels.ReferralStatus, ReferralStatus>();
        }
    }
}