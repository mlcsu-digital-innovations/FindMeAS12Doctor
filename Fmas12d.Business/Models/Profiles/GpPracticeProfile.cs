using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
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