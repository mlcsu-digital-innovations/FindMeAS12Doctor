using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GpPracticeController : SearchBaseController<Business.Models.GpPractice>
  {
    public GpPracticeController(
      IGpPracticeService service)
      : base(service)
    { }

  }
}