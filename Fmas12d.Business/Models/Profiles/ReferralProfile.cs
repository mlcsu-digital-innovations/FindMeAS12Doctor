using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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