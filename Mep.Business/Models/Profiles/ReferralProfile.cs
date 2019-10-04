using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class ReferralProfile : Profile
  {
    public ReferralProfile()
    {
      CreateMap<Entities.Referral, Referral>();
      CreateMap<Referral, Entities.Referral>();
    }
  }
}