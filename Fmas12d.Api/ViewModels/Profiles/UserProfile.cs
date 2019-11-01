using AutoMapper;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.ViewModels.Profiles
{
  public class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<User, BusinessModels.User>();

      CreateMap<BusinessModels.User, User>()
      .ForMember(u => u.GenderName, opt => opt.MapFrom(x => x.GenderType.Name));
    }
  }
}