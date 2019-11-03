using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class GenderTypeProfile : Profile
  {
    public GenderTypeProfile()
    {
      CreateMap<Entities.GenderType, Models.GenderType>();

      CreateMap<Models.GenderType, Entities.GenderType>();
    }
  }
}