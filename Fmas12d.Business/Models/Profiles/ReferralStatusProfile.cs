using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class ReferralStatusProfile : Profile
  {
    public ReferralStatusProfile()
    {
      CreateMap<Entities.ReferralStatus, Models.ReferralStatus>();

      CreateMap<Models.ReferralStatus, Entities.ReferralStatus>();
    }
  }
}