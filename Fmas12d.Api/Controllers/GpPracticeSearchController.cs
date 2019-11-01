using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
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
