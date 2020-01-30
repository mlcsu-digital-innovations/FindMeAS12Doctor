using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Policy = "User")]

  public class ContactDetailTypeController : IdNameDescriptionBaseController
  {
    public ContactDetailTypeController(
      IContactDetailTypeService service,
      IUserClaimsService userClaimsService)
      : base(userClaimsService, service)
    {
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<ViewModels.ContactDetailType>>> GetList(
    )
    {
      return await GetListInternal(null, GetUserId());
    }

    [HttpGet]  
    [Route("{userId:int}")]
    public async Task<ActionResult<IEnumerable<ViewModels.ContactDetailType>>> GetList(
      int userId
    )
    {
      return await GetListInternal(null, userId);
    }

    [HttpGet]
    [Route("assessments/{assessmentId:int}")]
    public async Task<ActionResult<IEnumerable<ViewModels.ContactDetailType>>> GetListForAssessment(
      int assessmentId
    )
    {
      return await GetListInternal(assessmentId, GetUserId());
    }

    [HttpGet]
    [Route("assessments/{assessmentId:int}/{userId:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<IEnumerable<ViewModels.ContactDetailType>>> GetListForAssessment(
      int assessmentId,
      int userId
    )
    {
      return await GetListInternal(assessmentId, userId);
    }

    private async Task<ActionResult<IEnumerable<ViewModels.ContactDetailType>>> GetListInternal(
      int? assessmentId,
      int userId
    )
    {
      try
      {
        IEnumerable<Business.Models.ContactDetailType> businessModels;
        if (assessmentId == null)
        {
          businessModels = await Service.GetAsync(userId, true, true);
        }
        else
        {
          businessModels = await Service.GetAsync(assessmentId.Value, userId, true, true);
        }

        if (businessModels == null || !businessModels.Any())
        {
          return NoContent();
        }
        else
        {
          IEnumerable<ViewModels.ContactDetailType> viewModels =
            businessModels.Select(cdt => new ViewModels.ContactDetailType(cdt)).ToList();

          return Ok(viewModels);
        }
      }
      catch (Exception ex)
      {
        return ProcessException(ex);
      }
    }

    private IContactDetailTypeService Service
    {
      get { return _service as IContactDetailTypeService; }
    }
  }
}