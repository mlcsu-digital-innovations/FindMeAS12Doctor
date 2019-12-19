using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "User")]

  public abstract class ModelControllerDeletePatchBase : ModelControllerBase
  {
    protected ModelControllerDeletePatchBase(
        IUserClaimsService userClaimsService,
        IServiceBase service
    ) : base(userClaimsService, service)
    {
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await _service.DeactivateAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id)
    {
      try
      {
        await _service.ActivateAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }    
  }
}