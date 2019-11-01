using AutoMapper;
using BusinessModels = Mep.Business.Models;
using Mep.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Mep.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy="User")]
  public class ExaminationController :
    ModelController<BusinessModels.Examination,
                    ViewModels.Examination,
                    RequestModels.ExaminationPut,
                    RequestModels.Examination>
  {
    public ExaminationController(
      IModelService<BusinessModels.Examination> service,
      IMapper mapper)
      : base(service, mapper)
    {
    }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<ViewModels.ExaminationList>>> ListGet([FromQuery]
      RequestModels.ExaminationListSearch examinationListSearch)
    {

      IEnumerable<BusinessModels.Examination> businessModels = null;
      if (examinationListSearch.AmhpUserId.HasValue)
      {
        businessModels = await (_service as ExaminationService)
          .GetAllFilterByAmhpUserIdAsync((int)examinationListSearch.AmhpUserId, true, false);
      }

      if (businessModels.Any())
      {
        IEnumerable<ViewModels.ExaminationList> viewModels =
          businessModels.Select(ViewModels.ExaminationList.ProjectFromModel).ToList();

        return Ok(viewModels);
      }
      else
      {
        return NoContent();
      }
    }

    [HttpPut]
    [Route("outcome/{id:int}/failure")]
    public async Task<ActionResult<RequestModels.ExaminationOutcomeFailurePut>> OutcomeFailurePut(
      int id,
      [FromBody] RequestModels.ExaminationOutcomeFailurePut requestModel)
    {
      try
      {
        BusinessModels.ExaminationOutcome businessModel = requestModel.MapToBusinessModel(id);
        businessModel = await (_service as ExaminationService).UpdateOutcomeAsync(businessModel);
        requestModel = new RequestModels.ExaminationOutcomeFailurePut(businessModel);

        return Ok(requestModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPut]
    [Route("outcome/{id:int}/success")]
    public async Task<ActionResult<RequestModels.ExaminationOutcomeSuccessPut>> OutcomeSuccessPut(
      int id,
      [FromBody] RequestModels.ExaminationOutcomeSuccessPut requestModel)
    {
      try
      {
        BusinessModels.ExaminationOutcome businessModel = requestModel.MapToBusinessModel(id);
        businessModel = await (_service as ExaminationService).UpdateOutcomeAsync(businessModel);
        requestModel = new RequestModels.ExaminationOutcomeSuccessPut(businessModel);

        return Ok(requestModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPost]
    [Route("emergency")]
    public async Task<ActionResult<RequestModels.ExaminationPostEmergency>> PostEmergency(
      [FromBody] RequestModels.ExaminationPostEmergency requestModel)
    {
      try
      {
        BusinessModels.ExaminationCreate businessModel = requestModel.MapToBusinessModel();
        businessModel = await (_service as ExaminationService).CreateAsync(businessModel);
        requestModel = new RequestModels.ExaminationPostEmergency(businessModel);

        return Created(GetCreatedModelUri(businessModel.Id), requestModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }     

    [HttpPost]
    [Route("planned")]
    public async Task<ActionResult<RequestModels.ExaminationPostPlanned>> PostPlanned(
      [FromBody] RequestModels.ExaminationPostPlanned requestModel)
    {
      try
      {
        BusinessModels.ExaminationCreate businessModel = requestModel.MapToBusinessModel();
        businessModel = await (_service as ExaminationService).CreateAsync(businessModel);
        requestModel = new RequestModels.ExaminationPostPlanned(businessModel);

        return Created(GetCreatedModelUri(businessModel.Id), requestModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }    

    [HttpGet]
    [Route("view/{id:int}")]
    public async Task<ActionResult<ViewModels.ExaminationView>> ViewGet(int id)
    {
      try
      {
        BusinessModels.Examination businessModel =
            await (_service as ExaminationService).GetByIdAsync(id, true);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {

          ViewModels.ExaminationView viewModel = new ViewModels.ExaminationView(businessModel);
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