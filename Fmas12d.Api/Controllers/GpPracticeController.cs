using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
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