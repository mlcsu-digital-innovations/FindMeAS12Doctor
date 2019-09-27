using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class ReferralProfile : Profile
  {
    public ReferralProfile()
    {
      CreateMap<Entities.Referral, Models.Referral>()
      ;//.ForPath(dest => dest.Patient.Referrals, opt => opt.Ignore());

      CreateMap<Models.Referral, Entities.Referral>();
    }
  }
}