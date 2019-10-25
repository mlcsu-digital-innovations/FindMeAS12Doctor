using AutoMapper;
using Mep.Business.Models;

namespace Mep.Business.Services
{
  public class UserDoctorSearchService : UserSearchService, IModelGeneralSearchService<UserDoctor>
  {
   public UserDoctorSearchService(ApplicationContext context, IMapper mapper)
      : base(context, mapper, Models.ProfileType.DOCTOR)
    {

    }
  }
}