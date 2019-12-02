using System;
using System.Threading.Tasks;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("api/assessment")]
  [ApiController]
  [Authorize(Policy="User")]
  
  public class AssessmentDoctorAcceptanceController : ModelControllerNoAutoMapper
  {
    public AssessmentDoctorAcceptanceController(
      IUserClaimsService userClaimsService,
      IAssessmentService service)
      : base(userClaimsService, service)
    {
    }

    [HttpPut]
    [Route("{assessmentId:int}/accepted")]
    public async Task<ActionResult> PutAccepted(
      int assessmentId
    )
    {
      RequestModels.AssessmentDoctorAcceptance requestModel = 
        new RequestModels.AssessmentDoctorAcceptance() {
          AssessmentId = assessmentId,
          HasAccepted = true,
          UserId = GetUserId()
      };
      return await UpdateAcceptance(requestModel);
    }   

    [HttpPut]
    [Authorize(Policy="Admin")]
    [Route("{assessmentId:int}/doctors/{userId:int}/accepted")]
    public async Task<ActionResult> PutAccepted(
      int assessmentId,
      int userId
    )
    {
      RequestModels.AssessmentDoctorAcceptance requestModel = 
        new RequestModels.AssessmentDoctorAcceptance() {
          AssessmentId = assessmentId,
          HasAccepted = true,
          UserId = userId
      };
      return await UpdateAcceptance(requestModel);
    }    

    [HttpPut]
    [Route("{assessmentId:int}/accepted/contactdetail")]
    public async Task<ActionResult> PutAcceptedContactDetail(
      int assessmentId,
      [FromBody] RequestModels.AssessmentDoctorAcceptancePutContactDetail requestModel
    )
    {
      requestModel.AssessmentId = assessmentId;
      requestModel.HasAccepted = true;
      requestModel.UserId = GetUserId();
      return await UpdateAcceptance(requestModel);
    }        

    [HttpPut]
    [Authorize(Policy="Admin")]
    [Route("{assessmentId:int}/doctors/{userId:int}/accepted/contactdetail")]
    public async Task<ActionResult> PutAcceptedContactDetail(
      int assessmentId,
      int userId,
      [FromBody] RequestModels.AssessmentDoctorAcceptancePutContactDetail requestModel
    )
    {
      requestModel.AssessmentId = assessmentId;
      requestModel.HasAccepted = true;
      requestModel.UserId = userId;
      return await UpdateAcceptance(requestModel);
    }

    [HttpPut]
    [Route("{assessmentId:int}/accepted/location")]
    public async Task<ActionResult> PutAcceptedLocation(
      int assessmentId,
      [FromBody] RequestModels.AssessmentDoctorAcceptancePutLocation requestModel
    )
    {
      requestModel.AssessmentId = assessmentId;
      requestModel.HasAccepted = true;
      requestModel.UserId = GetUserId();
      return await UpdateAcceptance(requestModel);
    }

    [HttpPut]
    [Authorize(Policy="Admin")]
    [Route("{assessmentId:int}/doctors/{userId:int}/accepted/location")]
    public async Task<ActionResult> PutAcceptedLocation(
      int assessmentId,
      int userId,
      [FromBody] RequestModels.AssessmentDoctorAcceptancePutLocation requestModel
    )
    {
      requestModel.AssessmentId = assessmentId;
      requestModel.HasAccepted = true;
      requestModel.UserId = userId;
      return await UpdateAcceptance(requestModel);
    }

    [HttpPut]
    [Route("{assessmentId:int}/accepted/postcode")]
    public async Task<ActionResult> PutAcceptedPostcode(
      int assessmentId,
      [FromBody] RequestModels.AssessmentDoctorAcceptancePutPostcode requestModel
    )
    {
      requestModel.AssessmentId = assessmentId;
      requestModel.HasAccepted = true;
      requestModel.UserId = GetUserId();
      return await UpdateAcceptance(requestModel);
    }      

    [HttpPut]
    [Authorize(Policy="Admin")]
    [Route("{assessmentId:int}/doctors/{userId:int}/accepted/postcode")]
    public async Task<ActionResult> PutAcceptedPostcode(
      int assessmentId,
      int userId,
      [FromBody] RequestModels.AssessmentDoctorAcceptancePutPostcode requestModel
    )
    {
      requestModel.AssessmentId = assessmentId;
      requestModel.HasAccepted = true;
      requestModel.UserId = userId;
      return await UpdateAcceptance(requestModel);
    }   

    [HttpPut]
    [Route("{assessmentId:int}/declined")]
    public async Task<ActionResult> PutDeclined(
      int assessmentId
    )
    {
      RequestModels.AssessmentDoctorAcceptance requestModel = 
        new RequestModels.AssessmentDoctorAcceptance() {
          AssessmentId = assessmentId,
          HasAccepted = false,
          UserId = GetUserId()
      };
      return await UpdateAcceptance(requestModel);
    }  

    [HttpPut]
    [Authorize(Policy="Admin")]
    [Route("{assessmentId:int}/doctors/{userId:int}/declined")]
    public async Task<ActionResult> PutDeclined(
      int assessmentId,
      int userId
    )
    {
      RequestModels.AssessmentDoctorAcceptance requestModel = 
        new RequestModels.AssessmentDoctorAcceptance() {
          AssessmentId = assessmentId,
          HasAccepted = false,
          UserId = userId
      };
      return await UpdateAcceptance(requestModel);
    }       

    private async Task<ActionResult> UpdateAcceptance(
      RequestModels.AssessmentDoctorAcceptance requestModel)
    {
      try
      {
        Business.Models.AssessmentDoctor businessModel = requestModel.MapToBusinessModel();
        await Service.UpdateAssessmentDoctorAcceptance(businessModel);

        return Ok();
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    private IAssessmentService Service
    {
      get { return _service as IAssessmentService; }
    }
  }
}