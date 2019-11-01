using Mep.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PatientController : ModelControllerNoAutoMapper
  {
    public PatientController(IPatientService service)
      : base(service)
    {
    }

    [HttpPost]
    public virtual async Task<ActionResult<ViewModels.PatientPost>> Post(
      [FromBody] RequestModels.PatientPost requestModel)
    {
      try
      {
        Business.Models.Patient businessModel = requestModel.MapToBusinessModel();
        businessModel = await Service.CreateAsync(businessModel);
        ViewModels.PatientPost viewModel = new ViewModels.PatientPost(businessModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }    

    [Route("search")]
    [HttpGet]
    public async Task<ActionResult<ViewModels.PatientSearch>> GetSearch(
      [FromQuery] RequestModels.PatientSearch search)
    {
      Business.Models.Patient model;
      if (search.IsByNhsNumber)
      {

        model = await Service.GetByNhsNumber(search.NhsNumberAsLong);
      }
      else
      {
        model = await Service.GetByAlternativeIdentifier(search.AlternativeIdentifier);
      }

      if (model == null)
      {
        return NoContent();
      }
      else
      {
        ViewModels.PatientSearch viewModel = new ViewModels.PatientSearch(model);
        return Ok(viewModel);
      }
    }

    private IPatientService Service { get { return _service as IPatientService; } }
  }
}