using AutoMapper;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AmhpSearchController : GeneralSearchController<BusinessModels.UserAmhp>
  {
    public AmhpSearchController(
      IModelGeneralSearchService<BusinessModels.UserAmhp> service,
      IMapper mapper)
      : base(service, mapper)
    {      
    }
  }
}