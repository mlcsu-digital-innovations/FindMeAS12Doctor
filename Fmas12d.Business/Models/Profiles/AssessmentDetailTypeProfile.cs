using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class AssessmentDetailTypeProfile : Profile
  {
    public AssessmentDetailTypeProfile()
    {
      CreateMap<Entities.AssessmentDetailType, Models.AssessmentDetailType>();

      CreateMap<Models.AssessmentDetailType, Entities.AssessmentDetailType>();
    }
  }
}