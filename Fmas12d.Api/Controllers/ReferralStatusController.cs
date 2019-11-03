using AutoMapper;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Fmas12d.Business.Models;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReferralStatusController :
    ModelController<BusinessModels.ReferralStatus,
                    ViewModels.ReferralStatus,
                    RequestModels.ReferralStatusPut,
                    RequestModels.ReferralStatusPost>
  {
    public ReferralStatusController(
      IModelService<BusinessModels.ReferralStatus> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}