using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
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