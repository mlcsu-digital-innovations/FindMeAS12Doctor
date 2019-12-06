using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class PatientController : ModelControllerDeletePatchBase
  {
    public PatientController(
      IPatientService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {
    }

    [HttpPost]
    public virtual async Task<ActionResult<ViewModels.Patient>> Post(
      [FromBody] RequestModels.PatientPost requestModel)
    {
      try
      {
        Business.Models.Patient businessModel = new Business.Models.Patient();
        requestModel.MapToBusinessModel(businessModel);
        businessModel = await Service.CreateAsync(businessModel);
        ViewModels.Patient viewModel = new ViewModels.Patient(businessModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPut]
    [Route("{id:int}")]
    public virtual async Task<ActionResult<ViewModels.Patient>> Put(
      int id,
      [FromBody] RequestModels.PatientPut requestModel)
    {
      try
      {
        Business.Models.Patient businessModel = new Business.Models.Patient();
        requestModel.MapToBusinessModel(businessModel);
        businessModel.Id = id;
        businessModel = await Service.UpdateAsync(businessModel);
        ViewModels.Patient viewModel = new ViewModels.Patient(businessModel);

        return Ok(viewModel);
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

        model = await Service.GetByNhsNumberAsync(search.NhsNumberAsLong);
      }
      else
      {
        model = await Service.GetByAlternativeIdentifierAsync(search.AlternativeIdentifier);
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