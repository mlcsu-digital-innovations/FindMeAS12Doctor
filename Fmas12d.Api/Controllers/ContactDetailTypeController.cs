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
      [FromQuery] bool? includeOther
    )
    {
      return await GetListInternal(null, GetUserId(), includeOther ?? false);
    }

    [HttpGet]
    [Route("{userId:int}")]
    public async Task<ActionResult<IEnumerable<ViewModels.ContactDetailType>>> GetList(
      int userId,
      [FromQuery] bool? includeOther
    )
    {
      return await GetListInternal(null, userId, includeOther ?? false);
    }

    [HttpGet]
    [Route("assessments/{assessmentId:int}")]
    public async Task<ActionResult<IEnumerable<ViewModels.ContactDetailType>>> GetListForAssessment(
      int assessmentId,
      [FromQuery] bool? includeOther
    )
    {
      return await GetListInternal(assessmentId, GetUserId(), includeOther ?? false);
    }

    [HttpGet]
    [Route("assessments/{assessmentId:int}/{userId:int}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<IEnumerable<ViewModels.ContactDetailType>>> GetListForAssessment(
      int assessmentId,
      int userId,
      [FromQuery] bool? includeOther
    )
    {
      return await GetListInternal(assessmentId, userId, includeOther ?? false);
    }

    private async Task<ActionResult<IEnumerable<ViewModels.ContactDetailType>>> GetListInternal(
      int? assessmentId,
      int userId,
      bool includeOther
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

        List<ViewModels.ContactDetailType> viewModels = null;
        if (businessModels == null || !businessModels.Any())
        {
          viewModels = new List<ViewModels.ContactDetailType>();
        }
        else
        {
          viewModels = businessModels.Select(cdt => new ViewModels.ContactDetailType(cdt)).ToList();
        }

        if (includeOther)
        {
          viewModels.Add(new ViewModels.ContactDetailType()
          {
            ContactDetails = new List<ViewModels.ContactDetail>() {
              new ViewModels.ContactDetail() {
                Id = -1
              }
            },
            Description = "Other",
            Name = "Other"
          });
        }

        return Ok(viewModels);
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