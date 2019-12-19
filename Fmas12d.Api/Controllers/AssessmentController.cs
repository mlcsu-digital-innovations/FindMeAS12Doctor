using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "User")]
  public class AssessmentController : ModelControllerDeletePatchBase
  {
    private IAssessmentService Service { get { return _service as IAssessmentService; } }

    public AssessmentController(
      IUserClaimsService userClaimsService,
      IAssessmentService service)
      : base(userClaimsService, service)
    {
    }

    [HttpGet]
    [Route("{id:int}/doctors/available")]
    public async Task<ActionResult<ViewModels.AssessmentAvailableDoctors>> GetDoctorsAvailable(
      int id)
    {
      try
      {
        Business.Models.Assessment businessModel =
          await Service.GetAvailableDoctorsAsync(id, true, true);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {
          ViewModels.AssessmentAvailableDoctors viewModel =
            new ViewModels.AssessmentAvailableDoctors(businessModel);

          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpGet]
    [Route("{id:int}/doctors/selected")]
    public async Task<ActionResult<ViewModels.AssessmentSelectedDoctors>> GetDoctorsSelected(
      int id)
    {
      try
      {
        Business.Models.Assessment businessModel =
          await Service.GetSelectedDoctorsAsync(id, true, true);

        if (businessModel == null)
        {
          return NoContent();
        }
        else
        {
          ViewModels.AssessmentSelectedDoctors viewModel =
            new ViewModels.AssessmentSelectedDoctors(businessModel);

          return Ok(viewModel);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<IEnumerable<ViewModels.AssessmentList>>> GetList([FromQuery]
      RequestModels.AssessmentListSearch requestModel)
    {
      return await GetListInternal(
        GetUserId(),
        requestModel.DoctorStatusId,
        requestModel.ReferralStatusId
      );
    }

    [HttpGet]
    [Route("list/{userId:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<IEnumerable<ViewModels.AssessmentList>>> GetList(
      int userId,
      [FromQuery] RequestModels.AssessmentListSearch requestModel)
    {
      return await GetListInternal(
        userId,
        requestModel.DoctorStatusId,
        requestModel.ReferralStatusId
      );
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<ViewModels.AssessmentView>> GetView(int id)
    {
      try
      {
        Business.Models.Assessment businessModel = await Service.GetByIdAsync(id, true, true);

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

    [HttpGet]
    [Route("{id:int}/user")]
    public async Task<ActionResult<ViewModels.AssessmentView>> GetViewForUser(int id)
    {
      try
      {
        Business.Models.Assessment businessModel =
          await Service.GetByIdForUserAsync(id, GetUserId(), true, true);

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
    [Route("{id:int}/doctors/allocated")]
    public async Task<ActionResult<ViewModels.AssessmentDoctorsPost>> PostDoctorsAllocated(
      int id,
      [FromBody] RequestModels.AssessmentDoctorsPost requestModel)
    {
      try
      {
        Business.Models.IAssessmentDoctorsUpdate businessModel =
          new Business.Models.AssessmentDoctorsUpdate
          {
            Id = id
          };
        requestModel.MapToBusinessModel(businessModel);
        businessModel = await Service.AddAllocatedDoctorsAsync(businessModel);
        ViewModels.AssessmentDoctorsPost viewModel =
          new ViewModels.AssessmentDoctorsPost(businessModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPost]
    [Route("{id:int}/doctors/allocated/direct")]
    public async Task<ActionResult> PostDoctorAllocatedDirect(
      int id,
      [FromBody] RequestModels.AssessmentDoctorsPostDirect requestModel
    )
    {
      try
      {
        await Service.AddAllocatedDoctorDirectAsync(id, requestModel.UserId.Value);
        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    [HttpPost]
    [Route("{id:int}/doctors/selected")]
    public async Task<ActionResult<ViewModels.AssessmentDoctorsPost>> PostDoctorsSelected(
      int id,
      [FromBody] RequestModels.AssessmentDoctorsPost requestModel)
    {
      try
      {
        Business.Models.IAssessmentDoctorsUpdate businessModel =
          new Business.Models.AssessmentDoctorsUpdate
          {
            Id = id
          };
        requestModel.MapToBusinessModel(businessModel);
        businessModel = await Service.AddSelectedDoctorsAsync(businessModel);
        ViewModels.AssessmentDoctorsPost viewModel =
          new ViewModels.AssessmentDoctorsPost(businessModel);

        return Created(GetCreatedModelUri(viewModel.Id), viewModel);
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
    [Route("{id:int}/doctors/remove")]
    public async Task<ActionResult> PutDoctorRemove(
      int id,
      [FromBody] RequestModels.AssessmentDoctorsRemovePut requestModel)
    {
      try
      {
        Business.Models.IAssessmentDoctorsRemove businessModel =
          new Business.Models.AssessmentDoctorsRemove
        {
          Id = id
        };
        requestModel.MapToBusinessModel(businessModel);
        await Service.RemoveDoctorsAsync(businessModel);

        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
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
    [Route("{id:int}/outcome/failure")]
    public async Task<ActionResult<ViewModels.AssessmentOutcomePut>> PutOutcomeFailure(
      int id,
      [FromBody] RequestModels.AssessmentOutcomeFailurePut requestModel)
    {
      return await PutOutcome(id, requestModel);
    }

    [HttpPut]
    [Route("{id:int}/outcome/success")]
    public async Task<ActionResult<ViewModels.AssessmentOutcomePut>> PutOutcomeSuccess(
      int id,
      [FromBody] RequestModels.AssessmentOutcomeSuccessPut requestModel)
    {
      return await PutOutcome(id, requestModel);
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
    [Route("{id:int}/schedule")]
    public async Task<ActionResult> PutSchedule(
      int id,
      [FromBody] RequestModels.AssessmentPutSchedule requestModel
    )
    {
      try
      {
        await Service.Schedule(id, requestModel.ScheduledTime.Value);
        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
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

    private async Task<ActionResult<IEnumerable<ViewModels.AssessmentList>>> GetListInternal(
      int userId,
      int? doctorStatusId,
      int? referralStatusId
    )
    {
      try
      {
        IEnumerable<Business.Models.Assessment> businessModels =
        await Service.GetListByUserIdAsync(
            userId,
            doctorStatusId,
            referralStatusId,
            true,
            true);

        if (businessModels == null || !businessModels.Any())
        {
          return NoContent();
        }
        else
        {
          IEnumerable<ViewModels.AssessmentList> viewModels =
            businessModels.Select(ViewModels.AssessmentList.ProjectFromModel).ToList();

          return Ok(viewModels);
        }
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
        Business.Models.AssessmentUpdate businessModel = new Business.Models.AssessmentUpdate
        {
          Id = id
        };
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