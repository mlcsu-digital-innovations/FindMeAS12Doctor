using AutoMapper;
using BusinessModels = Mep.Business.Models;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
    public ReferralController(IReferralService service, IMapper mapper)
      : base(service, mapper)
    {
    }

    private ReferralService Service { get { return _service as ReferralService; } }

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
      IEnumerable<BusinessModels.Referral> businessModels = await Service.GetListAsync(true);

      if (businessModels.Any())
      {
        IEnumerable<ViewModels.ReferralList> viewModels =
            businessModels.Select(ViewModels.ReferralList.ProjectFromModel).ToList();

        return Ok(viewModels);
      }
      else
      {
        return NoContent();
      }
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