using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
  public class ReferralListProfile : Profile
    {
        public ReferralListProfile()
        {
             CreateMap<BusinessModels.Referral, ReferralList>();
        }
    }
} 