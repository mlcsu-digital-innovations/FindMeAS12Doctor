using AutoMapper;
using Mep.Business.Models;

namespace Mep.Business.Services
{
  public class UserAmhpSearchService : UserSearchService, IModelGeneralSearchService<UserAmhp>
  {
   public UserAmhpSearchService(ApplicationContext context, IMapper mapper)
      : base(context, mapper, Models.ProfileType.AMHP)
    {

    }
  }
}