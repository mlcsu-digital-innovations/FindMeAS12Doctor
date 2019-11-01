using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class ClaimStatusProfile : Profile
  {
    public ClaimStatusProfile()
    {
      CreateMap<Entities.ClaimStatus, Models.ClaimStatus>();

      CreateMap<Models.ClaimStatus, Entities.ClaimStatus>();
    }
  }
}