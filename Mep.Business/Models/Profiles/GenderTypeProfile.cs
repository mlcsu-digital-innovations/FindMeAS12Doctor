using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class GenderTypeProfile : Profile
  {
    public GenderTypeProfile()
    {
      CreateMap<Entities.GenderType, Models.GenderType>()
      .ForMember(g => g.ModifiedByUser, opt => opt.Ignore());

      CreateMap<Models.GenderType, Entities.GenderType>();
    }
  }
}