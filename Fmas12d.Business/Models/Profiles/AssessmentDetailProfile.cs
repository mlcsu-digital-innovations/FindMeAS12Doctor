using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class AssessmentDetailProfile : Profile
  {
    public AssessmentDetailProfile()
    {
      CreateMap<Entities.AssessmentDetail, Models.AssessmentDetail>();

      CreateMap<Models.AssessmentDetail , Entities.AssessmentDetail>();
    }
  }
}