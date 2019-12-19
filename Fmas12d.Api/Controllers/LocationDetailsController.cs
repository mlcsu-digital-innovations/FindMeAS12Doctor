using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "User")]

  public class LocationDetailsController : ModelControllerDeletePatchBase
  {
    public LocationDetailsController(
      ILocationDetailService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {
    }

    [HttpGet("{postcode}")]
    public async Task<ActionResult<ViewModels.Location>> Get(string postcode)
    {
      try
      {
        Business.Models.Location businessModel = await Service.GetPostcodeDetailsAsync(postcode);
        ViewModels.Location viewModel = new ViewModels.Location(businessModel);

        if (viewModel == null)
        {
          return NoContent();
        }
        else
        {
          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [Route("search")]
    [HttpGet]
    public async Task<ActionResult<string>> SearchPostcode([FromQuery] string postcode)
    {
      try
      {
        Business.Models.SearchModels.PostcodeIoSearchResult businessModel =
          await Service.SearchPostcodeAsync(postcode);
        ViewModels.AddressSearchResult viewModel =
          new ViewModels.AddressSearchResult(businessModel);

        if (viewModel == null)
        {
          return NoContent();
        }
        else
        {
          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }

    }

    private ILocationDetailService Service { get { return _service as ILocationDetailService; } }
  }
}