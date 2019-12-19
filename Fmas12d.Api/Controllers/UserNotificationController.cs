using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fmas12d.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fmas12d.Api.Controllers
{
  [Route("user")]
  [ApiController]
  [Authorize(Policy="User")]
  
  public class UserNotificationController : ModelControllerDeletePatchBase
  {
    public UserNotificationController(
      IUserClaimsService userClaimsService,
      IUserNotificationService service)
      : base(userClaimsService, service)
    {
    }

    [HttpGet]
    [Route("{userId:int}/notifications")]
    public async Task<ActionResult<IEnumerable<ViewModels.UserAssessmentNotification>>> Get(
      int userId
    )
    {
      try
      {
        IEnumerable<Business.Models.UserAssessmentNotification> businessModels = 
          await Service.Get(userId, true, false);

        if (businessModels.Any())
        {
          IEnumerable<ViewModels.UserAssessmentNotification> viewModels =
            businessModels.Select(ViewModels.UserAssessmentNotification.ProjectFromModel).ToList();

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


    private IUserNotificationService Service
    {
      get { return _service as IUserNotificationService; }
    }
  }
}