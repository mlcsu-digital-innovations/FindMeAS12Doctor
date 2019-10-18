using System.Collections.Generic;
using System.Threading.Tasks;
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

    [HttpGet]
    [Route("edit/{id:int}")]
    public async Task<ActionResult<ViewModels.ReferralEdit>> GetEdit(int id)
    {
      BusinessModels.Referral businessModels =
          await _service.GetByIdAsync(id, true);

      ViewModels.ReferralEdit viewModel =
          _mapper.Map<ViewModels.ReferralEdit>(businessModels);

      return Ok(viewModel);
    }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<ViewModels.ReferralList>>> GetList()
    {
      IEnumerable<BusinessModels.Referral> businessModels =
          await _service.GetAllAsync(true);

      IEnumerable<ViewModels.ReferralList> viewModels =
          _mapper.Map<IEnumerable<ViewModels.ReferralList>>(businessModels);

      return Ok(viewModels);
    }

    [HttpGet]
    [Route("view/{id:int}")]
    public async Task<ActionResult<ViewModels.ReferralView>> GetView(int id)
    {
      BusinessModels.Referral businessModels =
          await _service.GetByIdAsync(id, true);

      ViewModels.ReferralView viewModel =
          _mapper.Map<ViewModels.ReferralView>(businessModels);

      return Ok(viewModel);
    }    
    
  }
}