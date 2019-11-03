using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels.Profiles
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