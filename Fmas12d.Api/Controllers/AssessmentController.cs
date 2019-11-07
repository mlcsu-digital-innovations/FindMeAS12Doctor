using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Fmas12d.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AssessmentController : ModelControllerNoAutoMapper
  {
    public AssessmentController(IAssessmentService service) : base(service)
    {
    }

    private IAssessmentService Service { get { return _service as IAssessmentService; } }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<ViewModels.AssessmentList>>> GetList([FromQuery]
      RequestModels.AssessmentListSearch assessmentListSearch)
    {
      try
      {
        IEnumerable<Business.Models.Assessment> businessModels = null;
        if (assessmentListSearch.AmhpUserId.HasValue)
        {
          businessModels = await Service.GetAllFilterByAmhpUserIdAsync(
            assessmentListSearch.AmhpUserId.Value, true, false);
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
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpGet]
    [Route("view/{id:int}")]
    public async Task<ActionResult<ViewModels.AssessmentView>> GetView(int id)
    {
      try
      {
        Business.Models.Assessment businessModel =
            await Service.GetByIdAsync(id, true);

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

    [HttpPost]
    [Route("emergency")]
    public async Task<ActionResult<ViewModels.AssessmentPost>> PostEmergency(
      [FromBody] RequestModels.AssessmentPostEmergency requestModel)
    {
      return await Create(requestModel);
    }

    [HttpPost]
    [Route("planned")]
    public async Task<ActionResult<ViewModels.AssessmentPost>> PostPlanned(
      [FromBody] RequestModels.AssessmentPostPlanned requestModel)
    {
      return await Create(requestModel);
    }

    [HttpPut]
    [Route("{id:int}/emergency")]
    public async Task<ActionResult<ViewModels.AssessmentPut>> PutEmergency(
      int id,
      [FromBody] RequestModels.AssessmentPutEmergency requestModel)
    {
      return await Update(id, requestModel);
    }

    [HttpPut]
    [Route("{id:int}/planned")]
    public async Task<ActionResult<ViewModels.AssessmentPut>> PutPlanned(
      int id,
      [FromBody] RequestModels.AssessmentPutPlanned requestModel)
    {
      return await Update(id, requestModel);
    }    

    [HttpPut]
    [Route("outcome/{id:int}/failure")]
    public async Task<ActionResult<ViewModels.AssessmentOutcomePut>> PutOutcomeFailure(
      int id,
      [FromBody] RequestModels.AssessmentOutcomeFailurePut requestModel)
    {
      return await PutOutcome(id, requestModel);
    }

    [HttpPut]
    [Route("outcome/{id:int}/success")]
     public async Task<ActionResult<ViewModels.AssessmentOutcomePut>> PutOutcomeSuccess(
      int id,
      [FromBody] RequestModels.AssessmentOutcomeSuccessPut requestModel)
    {
      return await PutOutcome(id, requestModel);
    }  

    private async Task<ActionResult<ViewModels.AssessmentPost>> Create(
      RequestModels.AssessmentPostPut requestModel)
    {
      try
      {
        Business.Models.AssessmentCreate businessModel = new Business.Models.AssessmentCreate();
        requestModel.MapToBusinessModel(businessModel);
        businessModel = await Service.CreateAsync(businessModel);
        ViewModels.AssessmentPost viewModel = new ViewModels.AssessmentPost(businessModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }  
    
    private async Task<ActionResult<ViewModels.AssessmentOutcomePut>> PutOutcome(
      int id,
      RequestModels.AssessmentOutcomePut requestModel)
    {
      try
      {
        Business.Models.AssessmentOutcome businessModel = requestModel.MapToBusinessModel(id);
        businessModel = await Service.UpdateOutcomeAsync(businessModel);
        ViewModels.AssessmentOutcomePut viewModel = 
          new ViewModels.AssessmentOutcomePut(businessModel);

        return Ok(viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }       

    private async Task<ActionResult<ViewModels.AssessmentPut>> Update(
      int id,
      RequestModels.AssessmentPostPut requestModel)
    {
      try
      {
        Business.Models.AssessmentUpdate businessModel = new Business.Models.AssessmentUpdate();
        businessModel.Id = id;
        requestModel.MapToBusinessModel(businessModel);
        businessModel = await Service.UpdateAsync(businessModel);
        ViewModels.AssessmentPut viewModel = new ViewModels.AssessmentPut(businessModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }  

  }
}