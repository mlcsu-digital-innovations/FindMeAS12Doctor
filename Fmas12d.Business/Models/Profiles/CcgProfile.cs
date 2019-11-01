using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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