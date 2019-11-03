using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
  public class ReferralListProfile : Profile
    {
        public ReferralListProfile()
        {
             CreateMap<BusinessModels.Referral, ReferralList>();
        }
    }
} 