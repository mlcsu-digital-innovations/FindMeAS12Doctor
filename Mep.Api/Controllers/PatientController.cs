using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessModels = Mep.Business.Models;
using System.Threading.Tasks;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PatientController :
    ModelControllerNoAutoMapper<Business.Models.Patient>
  {
    public PatientController(IPatientService service)
      : base(service)
    {
    }

    [Route("search")]
    [HttpGet]
    public async Task<ActionResult<ViewModels.Patient>> GetSearch(
      [FromQuery] RequestModels.PatientSearch search)
    {
      BusinessModels.Patient model;
      if (search.IsByNhsNumber)
      {

        model = await Service.GetByNhsNumber(search.NhsNumberAsLong, activeOnly: false);
      }
      else
      {
        model = await Service.GetByAlternativeIdentifier(
          search.AlternativeIdentifier,
          activeOnly: false);
      }

      if (model == null)
      {
        return NoContent();
      }
      else
      {
        ViewModels.Patient viewModel = new ViewModels.Patient(model);
        return Ok(viewModel);
      }
    }

    private PatientService Service { get { return _service as PatientService; } }
  }
}