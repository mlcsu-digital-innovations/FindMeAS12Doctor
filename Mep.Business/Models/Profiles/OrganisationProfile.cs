using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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