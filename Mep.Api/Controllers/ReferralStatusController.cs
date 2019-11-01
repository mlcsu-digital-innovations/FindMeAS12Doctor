using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
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