using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]  
  public class UserController : 
    ModelController<BusinessModels.User, 
                    ViewModels.User, 
                    RequestModels.UserPut, 
                    RequestModels.UserPost>
  {
    public UserController(
      IModelService<BusinessModels.User> service, 
      IMapper mapper)
      : base (service, mapper)
    {
    }
 }
}