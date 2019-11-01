using AutoMapper;
using Entities = Fmas12d.Data.Entities;
namespace Fmas12d.Business.Models.Profiles
{
  public class UserExaminationClaimProfile : Profile
  {
    public UserExaminationClaimProfile()
    {
      CreateMap<Entities.UserExaminationClaim, Models.UserExaminationClaim>();

      CreateMap<Models.UserExaminationClaim, Entities.UserExaminationClaim>();
    }
  }
}