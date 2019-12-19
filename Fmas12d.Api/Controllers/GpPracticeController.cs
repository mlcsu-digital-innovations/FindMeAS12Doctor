using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class GpPracticeController : SearchBaseController<Business.Models.GpPractice>
  {
    public GpPracticeController(
      IGpPracticeService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    { }

  }
}