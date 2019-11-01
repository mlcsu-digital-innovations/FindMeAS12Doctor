using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
{
  public class GpPracticeProfile : Profile
  {
    public GpPracticeProfile()
    {
      CreateMap<Entities.GpPractice, Models.GpPractice>();

      CreateMap<Models.GpPractice, Entities.GpPractice>();
    }
  }
}