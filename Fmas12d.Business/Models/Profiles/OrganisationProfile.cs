using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class OrganisationProfile : Profile
  {
    public OrganisationProfile()
    {
      CreateMap<Entities.Organisation, Models.Organisation>();

      CreateMap<Models.Organisation, Entities.Organisation>();
    }
  }
}