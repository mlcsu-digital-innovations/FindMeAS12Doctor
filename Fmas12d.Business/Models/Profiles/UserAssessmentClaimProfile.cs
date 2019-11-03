using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class UserAssessmentClaimProfile : Profile
  {
    public UserAssessmentClaimProfile()
    {
      CreateMap<Entities.UserAssessmentClaim, Models.UserAssessmentClaim>();

      CreateMap<Models.UserAssessmentClaim, Entities.UserAssessmentClaim>();
    }
  }
}