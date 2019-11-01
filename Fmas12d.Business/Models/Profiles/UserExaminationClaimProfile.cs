using AutoMapper;
using Entities = Mep.Data.Entities;
namespace Mep.Business.Models.Profiles
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