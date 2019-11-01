using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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