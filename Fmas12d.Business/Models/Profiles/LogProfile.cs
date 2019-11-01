using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class LogProfile : Profile
  {
    public LogProfile()
    {
      CreateMap<Entities.Log, Models.Log>();

      CreateMap<Models.Log, Entities.Log>();
    }
  }
}