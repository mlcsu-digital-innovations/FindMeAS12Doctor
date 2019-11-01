using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class CcgProfile : Profile
  {
    public CcgProfile()
    {
      CreateMap<Entities.Ccg, Models.Ccg>();

      CreateMap<Models.Ccg, Entities.Ccg>();
    }
  }
}