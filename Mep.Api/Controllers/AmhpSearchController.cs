using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AmhpSearchController : GeneralSearchController<BusinessModels.User>
  {
    public AmhpSearchController(
      IModelGeneralSearchService<BusinessModels.User> service,
      IMapper mapper)
      : base(service, mapper)
    {      
    }
  }
}