using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LocationDetailsController : ModelControllerNoAutoMapper
  {
    public LocationDetailsController(ILocationDetailService service) : base(service)
    {
    }

    [HttpGet("{postcode}")]
    public async Task<ActionResult<ViewModels.Postcode>> Get(string postcode)
    {
      try
      {
        Business.Models.Postcode businessModel = await Service.GetPostcodeDetailsAsync(postcode);
        ViewModels.Postcode viewModel = new ViewModels.Postcode(businessModel);

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