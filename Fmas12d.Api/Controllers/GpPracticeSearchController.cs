using AutoMapper;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class GpPracticeSearchController : GeneralSearchController<BusinessModels.GpPractice>
  {
    public GpPracticeSearchController(
      IModelGeneralSearchService<BusinessModels.GpPractice> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}
