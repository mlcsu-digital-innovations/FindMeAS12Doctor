using AutoMapper;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CcgSearchController : GeneralSearchController<BusinessModels.Ccg>
  {
    public CcgSearchController(
      IModelGeneralSearchService<BusinessModels.Ccg> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}