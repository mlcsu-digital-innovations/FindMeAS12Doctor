using AutoMapper;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public class UserAmhpSearchService : UserSearchService, IModelGeneralSearchService<UserAmhp>
  {
   public UserAmhpSearchService(ApplicationContext context, IMapper mapper)
      : base(context, mapper, Models.ProfileType.AMHP)
    {

    }
  }
}