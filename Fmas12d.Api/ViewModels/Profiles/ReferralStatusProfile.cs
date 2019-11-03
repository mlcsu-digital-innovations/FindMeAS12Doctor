using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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