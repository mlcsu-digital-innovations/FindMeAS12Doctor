using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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