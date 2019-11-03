using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class AssessmentProfile : Profile
  {
    public AssessmentProfile()
    {
      CreateMap<Entities.Assessment, Models.Assessment>();

      CreateMap<Models.Assessment, Entities.Assessment>();
    }
  }
}