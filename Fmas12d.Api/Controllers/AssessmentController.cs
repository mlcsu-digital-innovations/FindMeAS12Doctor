using AutoMapper;
using BusinessModels = Fmas12d.Business.Models;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]  
  [ApiController]
  [Authorize(Policy="User")]
  public class AssessmentController :
    ModelController<BusinessModels.Assessment,
                    ViewModels.Assessment,
                    RequestModels.AssessmentPut,
                    RequestModels.Assessment>
  {
    public AssessmentController(
      IModelService<BusinessModels.Assessment> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<ViewModels.AssessmentList>>> ListGet([FromQuery]
      RequestModels.AssessmentListSearch assessmentListSearch)
    {

      IEnumerable<BusinessModels.Assessment> businessModels = null;
      if (assessmentListSearch.AmhpUserId.HasValue)
      {
        businessModels = await (_service as AssessmentService)
          .GetAllFilterByAmhpUserIdAsync((int)assessmentListSearch.AmhpUserId, true, false);
      }

      if (businessModels.Any())
      {
        IEnumerable<ViewModels.AssessmentList> viewModels =
          businessModels.Select(ViewModels.AssessmentList.ProjectFromModel).ToList();

        return Ok(viewModels);
      }
      else
      {
        return NoContent();
      }
    }

    [HttpPut]
    [Route("outcome/{id:int}/failure")]
    public async Task<ActionResult<RequestModels.AssessmentOutcomeFailurePut>> OutcomeFailurePut(
      int id,
      [FromBody] RequestModels.AssessmentOutcomeFailurePut requestModel)
    {
      try
      {
        BusinessModels.AssessmentOutcome businessModel = requestModel.MapToBusinessModel(id);
        businessModel = await (_service as AssessmentService).UpdateOutcomeAsync(businessModel);
        requestModel = new RequestModels.AssessmentOutcomeFailurePut(businessModel);

        return Ok(requestModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPut]
    [Route("outcome/{id:int}/success")]
    public async Task<ActionResult<RequestModels.AssessmentOutcomeSuccessPut>> OutcomeSuccessPut(
      int id,
      [FromBody] RequestModels.AssessmentOutcomeSuccessPut requestModel)
    {
      try
      {
        BusinessModels.AssessmentOutcome businessModel = requestModel.MapToBusinessModel(id);
        businessModel = await (_service as AssessmentService).UpdateOutcomeAsync(businessModel);
        requestModel = new RequestModels.AssessmentOutcomeSuccessPut(businessModel);

        return Ok(requestModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPost]
    [Route("emergency")]
    public async Task<ActionResult<RequestModels.AssessmentPostEmergency>> PostEmergency(
      [FromBody] RequestModels.AssessmentPostEmergency requestModel)
    {
      try
      {
        BusinessModels.AssessmentCreate businessModel = requestModel.MapToBusinessModel();
        businessModel = await (_service as AssessmentService).CreateAsync(businessModel);
        requestModel = new RequestModels.AssessmentPostEmergency(businessModel);

        return Created(GetCreatedModelUri(businessModel.Id), requestModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }     

    [HttpPost]
    [Route("planned")]
    public async Task<ActionResult<RequestModels.AssessmentPostPlanned>> PostPlanned(
      [FromBody] RequestModels.AssessmentPostPlanned requestModel)
    {
      try
      {
        BusinessModels.AssessmentCreate businessModel = requestModel.MapToBusinessModel();
        businessModel = await (_service as AssessmentService).CreateAsync(businessModel);
        requestModel = new RequestModels.AssessmentPostPlanned(businessModel);

        return Created(GetCreatedModelUri(businessModel.Id), requestModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }    

    [HttpGet]
    [Route("view/{id:int}")]
    public async Task<ActionResult<ViewModels.AssessmentView>> ViewGet(int id)
    {
      try
      {
        BusinessModels.Assessment businessModel =
            await (_service as AssessmentService).GetByIdAsync(id, true);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {

          ViewModels.AssessmentView viewModel = new ViewModels.AssessmentView(businessModel);
          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

  }
}