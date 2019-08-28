using AutoMapper;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReferralController :
    ModelController<BusinessModels.Referral,
                    ViewModels.Referral,
                    RequestModels.ReferralPut,
                    RequestModels.ReferralPost>
  {
    public ReferralController(
      IModelService<BusinessModels.Referral> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }
  }
}