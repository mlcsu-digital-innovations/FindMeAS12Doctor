using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
{
  public class AssessmentDetailTypeProfile : Profile
  {
    public AssessmentDetailTypeProfile()
    {
      CreateMap<AssessmentDetailType, BusinessModels.AssessmentDetailType>();

      CreateMap<BusinessModels.AssessmentDetailType, AssessmentDetailType>();
    }
  }
}