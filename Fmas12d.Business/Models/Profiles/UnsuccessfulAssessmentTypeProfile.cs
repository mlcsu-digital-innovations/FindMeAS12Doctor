using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class UnsuccessfulAssessmentTypeProfile : Profile
  {
    public UnsuccessfulAssessmentTypeProfile()
    {
      CreateMap<Entities.UnsuccessfulAssessmentType, Models.UnsuccessfulAssessmentType>();

      CreateMap<Models.UnsuccessfulAssessmentType, Entities.UnsuccessfulAssessmentType>();
    }
  }
}